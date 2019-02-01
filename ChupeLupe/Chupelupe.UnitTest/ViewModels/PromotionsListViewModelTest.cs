using AutoFixture;
using Chupelupe.UnitTest.Helpers;
using ChupeLupe.Models;
using ChupeLupe.Services;
using ChupeLupe.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace Chupelupe.UnitTest.ViewModels
{
    [TestFixture]
    public class PromotionsListViewModelTest
    {
        Fixture Fixture  { get; set; }
        DependencyServiceStub DependencyService { get; set; }
        Mock<IWebServicesApi> ServerSideDataMock { get; set; }
        Mock<INavigation> NavigationMock { get; set; }

        [SetUp]
        public void SetUp()
        {
            MockForms.Init();
            Fixture = new Fixture();
            DependencyService = new DependencyServiceStub();

            ServerSideDataMock = new Mock<IWebServicesApi>();
            DependencyService.Register<IWebServicesApi>(ServerSideDataMock.Object);

            NavigationMock = new Mock<INavigation>();
            DependencyService.Register<INavigation>(NavigationMock.Object);
        }
        [Test]
        public void GetPromotionsCommandIsSuccessful()
        {
            var vm = new PromotionListViewModel(NavigationMock.Object, DependencyService);
            List<Promotion> list = new List<Promotion>
            {
                new Promotion
                {
                    Id = Fixture.Create<int>(),
                    Title = Fixture.Create<String>()
                }
            };
            ServerSideDataMock.Setup(m => m.GetPromotions()).ReturnsAsync(list);
            vm.GetPromotionCommand.Execute(null);

            ServerSideDataMock.Verify(m => m.GetPromotions(), Times.Once());
            Assert.IsNotNull(vm.PromotionList.Count);
            Assert.IsFalse(vm.IsBusy );
        }

        
    }
}

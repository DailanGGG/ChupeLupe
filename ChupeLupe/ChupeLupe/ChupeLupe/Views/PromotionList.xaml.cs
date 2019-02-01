using ChupeLupe.Helpers;
using ChupeLupe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChupeLupe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PromotionList : ContentPage
    {
        PromotionListViewModel _vm;
        public PromotionList()
        {
            InitializeComponent();
            _vm = new PromotionListViewModel(Navigation, new DependencyServiceWrapper());
            BindingContext = _vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _vm.GetPromotionCommand.Execute(null);
        }
    }
}
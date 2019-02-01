using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChupeLupe.Helpers;
using ChupeLupe.Models;
using ChupeLupe.ViewModels.Helpers;
using Xamarin.Forms;
namespace ChupeLupe.ViewModels
{
    public class PromotionListViewModel: BaseViewModel
    {
        #region properties & commands
        public Command GetPromotionCommand { get; set; }
        public ObservableCollection<Promotion> PromotionList { get; set; }
        #endregion

        bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                SetValue(ref _isBusy, value);
                Device.BeginInvokeOnMainThread(GetPromotionCommand.ChangeCanExecute);
            }
        }
        public PromotionListViewModel(INavigation navigation, IDependencyService dependencyservice): base(navigation, dependencyservice)
        {

            PromotionList = new ObservableCollection<Promotion>();
            GetPromotionCommand = new Command(async (obj) =>
            await ExecuteGetPromotionsCommand(obj), obj => !IsBusy);
        }
        async Task ExecuteGetPromotionsCommand(object obj)
        {
            try
            {

                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;
                

                var localPromotionsList = new List<Promotion>();
                localPromotionsList = await WebApi.GetPromotions();
                
                if (!localPromotionsList.Any())
                {
                    IsBusy = false;
                    return;
                }
                PromotionList = new ObservableCollection<Promotion>(localPromotionsList);
                OnPropertyChanged(nameof(PromotionList));
                
                IsBusy = false;

            }
            catch
            {

            }
        }
    }
}

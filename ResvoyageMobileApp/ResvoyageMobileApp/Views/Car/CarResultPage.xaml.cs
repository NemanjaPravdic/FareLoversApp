using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Services.Car;
using ResvoyageMobileApp.ViewModels.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResvoyageMobileApp.Views.Car
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarResultPage : ContentPage
    {
        private CarResultViewModel _vm;
        private CarSearchService _carSearchService;
        public CarResultPage(CarRequestViewModel request)
        {
            InitializeComponent();
            _carSearchService = new CarSearchService();
            _vm = new CarResultViewModel(request, null);
            BindingContext = null;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            waitScreen.IsVisible = true;
            VisibilityResults.IsVisible = false;
            Shell.SetNavBarIsVisible(this, false);
            if (BindingContext == null)
            {
                try
                {
                    var response = await _carSearchService.GetCarResponseAsync(_vm.Request);
                    if (response.Error == null && response.Cars != null)
                    {
                        BindingContext = new CarResultViewModel(_vm.Request, response);
                    }
                    else if (response.Error != null && response.Error.ErrorMessage != null)
                    {
                        waitScreen.IsVisible = false;
                        Shell.SetNavBarIsVisible(this, true);
                        await DisplayAlert(AppResources.APP_ERROR, response.Error.ErrorMessage, AppResources.APP_OK);
                        await Shell.Current.Navigation.PopAsync(true);
                    }
                }
                catch (Exception e)
                {
                    waitScreen.IsVisible = false;
                    Shell.SetNavBarIsVisible(this, true);
                    await DisplayAlert(AppResources.APP_ERROR, e.Message, AppResources.APP_OK);
                    await Shell.Current.Navigation.PopAsync(true);
                }

            }
            waitScreen.IsVisible = false;
            VisibilityResults.IsVisible = true;
            Shell.SetNavBarIsVisible(this, true);

        }
    }
}
using ResvoyageMobileApp.Models.Car;
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
    public partial class CarDetailsPage : ContentPage
    {
        private CarDetailsService _carDetailsService;
        private CarDetailsViewModel _vm;
        public CarDetailsPage(Guid selectedCarId, CarRequestViewModel request, Guid sessionId, CarInformation carInfo)
        {
            InitializeComponent();
            _vm = new CarDetailsViewModel(selectedCarId, request, sessionId, carInfo);
            _carDetailsService = new CarDetailsService();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //waitScreen.IsVisible = true;
            VisibilityResults.IsVisible = false;
            Shell.SetNavBarIsVisible(this, false);
            if (BindingContext as CarDetailsService == null)
            {
                try
                {
                    var response = await _carDetailsService.GetCarDetailsResponseAsync(_vm.SessionId, _vm.CarId);
                    if (response.Error == null && response.ErrorResult == null)
                    {
                        _vm.CarInformation.VehicleInfo.CarRules = response;
                        BindingContext = _vm;
                        //waitScreen.IsVisible = false;
                    }
                    else if (response.Error != null && response.Error.Message != null)
                    {
                        //waitScreen.IsVisible = false;
                        Shell.SetNavBarIsVisible(this, true);
                        await DisplayAlert(AppResources.APP_ERROR, response.Error.Message, AppResources.APP_OK);
                        await Shell.Current.Navigation.PopAsync(true);
                    }
                    else if (response.ErrorResult != null && response.ErrorResult.ErrorMessage != null)
                    {
                        //waitScreen.IsVisible = false;
                        Shell.SetNavBarIsVisible(this, true);
                        await DisplayAlert(AppResources.APP_ERROR, response.ErrorResult.ErrorMessage, AppResources.APP_OK);
                        await Shell.Current.Navigation.PopAsync(true);
                    }
                }
                catch (Exception e)
                {
                    //waitScreen.IsVisible = false;
                    Shell.SetNavBarIsVisible(this, true);
                    await DisplayAlert(AppResources.APP_ERROR, e.Message, AppResources.APP_OK);
                    await Shell.Current.Navigation.PopAsync(true);
                }

            }
            //waitScreen.IsVisible = false;
            VisibilityResults.IsVisible = true;
            Shell.SetNavBarIsVisible(this, true);
        }
    }
}
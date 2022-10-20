using ResvoyageMobileApp.Models.Hotel;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Services.Hotel;
using ResvoyageMobileApp.ViewModels.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ResvoyageMobileApp.Views.Hotel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HotelDetailsPage : ContentPage
    {
        private HotelDetailsViewModel _vm;
        private HotelDetailsService _hotelDetailsService;
        public HotelDetailsPage(Guid selectedHotelId, HotelRequestViewModel request, Guid sessionId)
        {
            InitializeComponent();
            _hotelDetailsService = new HotelDetailsService();
            _vm = new HotelDetailsViewModel(selectedHotelId, sessionId, request, null);

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //waitScreen.IsVisible = true;
            VisibilityResults.IsVisible = false;
            Shell.SetNavBarIsVisible(this, false);
            if (BindingContext as HotelDetailsViewModel == null)
            {
                try
                {
                    var response = await _hotelDetailsService.GetHotelDetailsResponseAsync(_vm.SessionId,_vm.HotelId);
                    if (response.Error == null && response.Hotels != null && response.Hotels.FirstOrDefault() != null && string.IsNullOrEmpty(response.ErrorMessage))
                    {
                        BindingContext = new HotelDetailsViewModel(_vm.HotelId, _vm.SessionId, _vm.Request, response.Hotels.FirstOrDefault());
                    }
                    else if (!string.IsNullOrEmpty(response.ErrorMessage))
                    {
                        //waitScreen.IsVisible = false;
                        Shell.SetNavBarIsVisible(this, true);
                        await DisplayAlert(AppResources.APP_ERROR, response.ErrorMessage, AppResources.APP_OK);
                        await Shell.Current.Navigation.PopAsync(true);
                    }
                    else if (response.Error != null && response.Error.ErrorMessage != null)
                    {
                        //waitScreen.IsVisible = false;
                        Shell.SetNavBarIsVisible(this, true);
                        await DisplayAlert(AppResources.APP_ERROR, response.Error.ErrorMessage, AppResources.APP_OK);
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
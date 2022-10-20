using Newtonsoft.Json;
using ResvoyageMobileApp.Models.User;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Services.Bookings;
using ResvoyageMobileApp.ViewModels.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResvoyageMobileApp.Views.Bookings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyBookingsPage : ContentPage
    {
        private BookingService _bookingService;
        public MyBookingsPage()
        {
            InitializeComponent();
            _bookingService = new BookingService();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext as BookingsViewModel == null)
            { 
                Shell.SetNavBarIsVisible(this, false);
                VisibilityResults.IsVisible = false;

                try
                {
                    var userJson = Application.Current.Properties["UserInfo"].ToString();
                    var user = JsonConvert.DeserializeObject<UserDetails>(userJson);

                    var response = await _bookingService.GetBookingInfoAsync(user.Id);
                    if (response.Error == null && response.Bookings != null)
                    {
                        BindingContext = new BookingsViewModel(response.Bookings);
                        waitScreen.IsVisible = false;
                        Shell.SetNavBarIsVisible(this, true);
                        VisibilityResults.IsVisible = true;
                    }
                    else if (response.Error != null && response.Error.ErrorMessage != null)
                    {
                        waitScreen.IsVisible = false;
                        VisibilityResults.IsVisible = true;
                        Shell.SetNavBarIsVisible(this, true);
                        await DisplayAlert(AppResources.APP_ERROR, response.Error.ErrorMessage, AppResources.APP_OK);

                        await Shell.Current.Navigation.PopAsync(true);
                    }
                }
                catch (Exception e)
                {
                    waitScreen.IsVisible = false;
                    VisibilityResults.IsVisible = true;
                    Shell.SetNavBarIsVisible(this, true);
                    await DisplayAlert(AppResources.APP_ERROR, e.Message, AppResources.APP_OK);

                    await Shell.Current.Navigation.PopAsync(true);
                }

            }

        }
    }
}
using Newtonsoft.Json;
using ResvoyageMobileApp.Models.Booking;
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
    public partial class BookingDetailsPage : ContentPage
    {
        private BookingInfo _booking;
        private BookingDetailsService _bookingDetailsService;
        public BookingDetailsPage(BookingInfo booking)
        {
            InitializeComponent();
            _booking = booking;
            _bookingDetailsService = new BookingDetailsService();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext as BookingDetailsViewModel == null)
            {
                Shell.SetNavBarIsVisible(this, false);
                VisibilityResults.IsVisible = false;

                try
                {
                    var response = await _bookingDetailsService.GetBookingDetailsAsync(_booking.Id);
                    if (response != null && response.Error == null)
                    {
                        BindingContext = new BookingDetailsViewModel(response, _booking);
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
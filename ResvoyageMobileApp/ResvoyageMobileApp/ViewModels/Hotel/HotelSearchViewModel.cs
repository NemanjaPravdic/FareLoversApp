using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Views.Hotel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Hotel
{
    public class HotelSearchViewModel : BaseViewModel
    {
        public HotelSearchViewModel()
        {
            _request = new HotelRequestViewModel();
        }
        private HotelRequestViewModel _request;

        public HotelRequestViewModel Request
        {
            get { return _request; }
            set { SetValue(ref _request, value); }
        }

        public ICommand ShowHotelDestinationAutocomplete => new Command(DisplayHotelDestinationAutocomplete);
        public ICommand ShowCalendarPage => new Command(DisplayCalendar); 
        public ICommand ShowRoomInfo => new Command(DisplayRoomInfo); 
        public ICommand SearchHotels => new Command(Search); 

        private void DisplayHotelDestinationAutocomplete()
        {
            var page = new HotelDestinationAutocompletePage(Request);
            _navigation.Navigation.PushAsync(page, true);
        }
        private void DisplayCalendar()
        {
            var page = new HotelCalendarPage(_request);
            _navigation.Navigation.PushAsync(page, true);
        }
        private void DisplayRoomInfo()
        {
            var page = new RoomInfoPage(_request);
            _navigation.Navigation.PushAsync(page, true);
        }
        public void Search() 
        {
            if (ValidateRequest())
            {
                var page = new HotelResultPage(_request);
                _navigation.Navigation.PushAsync(page);
            }
        }

        private bool ValidateRequest()
        {
			var errorMessage = string.Empty;
			var errorCount = 0;

			if (string.IsNullOrEmpty(_request.CityName))
			{
				errorMessage += AppResources.HS_CHECK_IN_LOCATION;
				errorCount++;
			}
			if (string.IsNullOrEmpty(_request.CheckInDate))
			{
				errorMessage += errorCount > 0 ? ", " + AppResources.HS_CHECK_IN_DATE : AppResources.HS_CHECK_IN_DATE;
				errorCount++;
			}
			if (string.IsNullOrEmpty(_request.CheckOutDate))
			{
				errorMessage += errorCount > 0 ? ", " + AppResources.HS_CHECK_OUT_DATE : AppResources.HS_CHECK_OUT_DATE;
				errorCount++;
			}
			
			if (errorCount > 0)
			{
				errorMessage += errorCount == 1 ? AppResources.SF_VALIDATION_ERROR_SINGLE : AppResources.SF_VALIDATION_ERROR;
				Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, errorMessage, AppResources.APP_OK);
				return false;
			}
			else
			{
				return true;
			}
		}

    }
}

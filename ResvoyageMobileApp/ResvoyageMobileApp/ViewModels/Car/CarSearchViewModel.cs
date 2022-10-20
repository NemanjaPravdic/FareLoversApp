using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Views.Car;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Car
{
    public class CarSearchViewModel :BaseViewModel
    {
        public CarSearchViewModel()
        {
            _request = new CarRequestViewModel();
        }
        private CarRequestViewModel _request;

        public CarRequestViewModel Request
        {
            get { return _request; }
            set { SetValue(ref _request, value); }
        }

        public ICommand ShowCalendarPage => new Command<string>(DisplayCalendar);
        public ICommand ShowDestinationAutocomplete => new Command<string>(DisplayDestinationAutocomplete);
        public ICommand SwitchDestinations => new Command(switchDestinations);
        public ICommand SearchCars => new Command(Search);

        private void switchDestinations()
        {
            var tmpCity = _request.PickupCity;
            var tmpIata = _request.PickupCityIata;
            _request.PickupCity = _request.DropOffCity;
            _request.PickupCityIata = _request.DropOffCityIata;
            _request.DropOffCity = tmpCity;
            _request.DropOffCityIata = tmpIata;
        }

        private void DisplayCalendar(string type)
        {
            var page = new CarCalendarPage(_request, type);
            _navigation.Navigation.PushAsync(page, true);
        }
        private void DisplayDestinationAutocomplete(string type)
        {
            var page = new DestinationPage(_request, type);
            _navigation.Navigation.PushAsync(page, true);
        }
        public void Search()
        {
            if (ValidateRequest())
            {
                var page = new CarResultPage(_request);
                _navigation.Navigation.PushAsync(page);
            }
        }

        private bool ValidateRequest()
        {
            var errorMessage = string.Empty;
            var errorCount = 0;

            if (string.IsNullOrEmpty(_request.PickupCity))
            {
                errorMessage += AppResources.CS_PICK_UP_LOCATION;
                errorCount++;
            }
            if (string.IsNullOrEmpty(_request.DropOffCity))
            {
                errorMessage += errorCount > 0 ? ", " + AppResources.CS_DROP_OFF_LOCATION : AppResources.CS_DROP_OFF_LOCATION;
                errorCount++;
            }
            if (string.IsNullOrEmpty(_request.PickupDate))
            {
                errorMessage += errorCount > 0 ? ", " + AppResources.CS_PICK_UP_DATE : AppResources.CS_PICK_UP_DATE;
                errorCount++;
            }
            if (string.IsNullOrEmpty(_request.DropOffDate))
            {
                errorMessage += errorCount > 0 ? ", " + AppResources.CS_DROP_OFF_DATE : AppResources.HS_CHECK_OUT_DATE;
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

        public List<string> Time
        {
            get
            {
                var response = new List<string>();

                for (int i = 0; i <= 23; i++)
                {
                    response.Add(i < 10 ? string.Format("0{0}:00", i) : string.Format("{0}:00", i));
                }

                return response;
            }
        }
    }
}

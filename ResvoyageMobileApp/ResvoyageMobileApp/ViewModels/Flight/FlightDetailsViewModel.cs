using ResvoyageMobileApp.Models.Flight;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Services.Flight;
using ResvoyageMobileApp.Views.Flight;
using ResvoyageMobileApp.Views.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Flight
{
    public class FlightDetailsViewModel : BaseViewModel
    {
        private AddToCartService _addToCartService;
        private FareRulesService _fareRulesService;
        public FlightDetailsViewModel(PreparedFlightResults selectedFlight, FlightRequestViewModel request)
        {
            _request = request;
            _flightDetails = PrepareFlightDetails(selectedFlight);
            _addToCartService = new AddToCartService();
            _fareRulesService = new FareRulesService();
        }
                

        private PreparedFlightDetails _flightDetails;

        public PreparedFlightDetails FlightDetails
        {
            get { return _flightDetails; }
            set { SetValue(ref _flightDetails, value); }
        }
        private FlightRequestViewModel _request;

        public FlightRequestViewModel Request
        {
            get { return _request; }
            set { SetValue(ref _request, value); }
        }
        private bool _showWaitScreen;

        public bool ShowWaitScreen
        {
            get { return _showWaitScreen; }
            set { SetValue(ref _showWaitScreen, value); }
        }
        public ICommand AddToCart => new Command(AddFlightToShoppingCart); 
        public ICommand GetFareRulesCommand => new Command(GetFareRules);
        private async void GetFareRules()
        {
            try
            {
                ShowWaitScreen = true;
                var response = await _fareRulesService.GetFareRules(_flightDetails.SessionId, _flightDetails.Id);
                if (response != null && response.FareRuleResponseInfo != null && response.FareRuleResponseInfo.Length > 0)
                {
                    ShowWaitScreen = false;
                    var page = new FareRulesPage(response);
                    var navigation = Application.Current.MainPage as Shell;
                    await navigation.Navigation.PushAsync(page, true);
                }
                else if (response.Error != null && !string.IsNullOrEmpty(response.Error.ErrorMessage))
                {
                    ShowWaitScreen = false;
                    await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, response.Error.ErrorMessage, AppResources.APP_OK);
                }
            }
            catch (Exception e)
            {
                ShowWaitScreen = false;
                await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, e.Message, AppResources.APP_OK);
            }
        }
        private async void AddFlightToShoppingCart()
        {
            try
            {
                ShowWaitScreen = true;
                var response = await _addToCartService.AddToCartAsync(_flightDetails);
                if (response != null && response.ShoppingCart != null && response.ShoppingCart.Air != null && response.ShoppingCart.Air.AirItineraryPricingInfo != null)
                {
                    var page = new ShoppingCartPage(response, "AIR");
                    var navigation = Application.Current.MainPage as Shell;
                    await navigation.Navigation.PushAsync(page, true);
                    ShowWaitScreen = false;
                }
                else if (response.Error != null && response.Error.ErrorMessage != null)
                {
                    ShowWaitScreen = false;
                    await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, response.Error.ErrorMessage, AppResources.APP_OK);
                }
                else
                {
                    ShowWaitScreen = false;
                    await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.ER_SOME_ERROR_OCCURED, AppResources.APP_OK);
                }
            }
            catch (Exception e)
            {
                ShowWaitScreen = false;
                await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, e.Message, AppResources.APP_OK);
            }
        }

        private PreparedFlightDetails PrepareFlightDetails(PreparedFlightResults selectedFlight)
        {
            var details = new PreparedFlightDetails {
                SessionId = selectedFlight.SessionId,
                Id = selectedFlight.Id,
                Total = selectedFlight.Total,
                Currency = selectedFlight.Currency
            };
            details.ListDestinationOptions = new List<DestinationOptions>();
            int i = 0;
            foreach (var destinationOption in selectedFlight.FlightInfo?.AirItinerary?.OriginDestinationOptions)
            {
                var organizedDestinationOption = new DestinationOptions();
                organizedDestinationOption.TotalDuration = destinationOption.JourneyTotalDuration;

                if (_request.SearchType == Models.SearchType.OneWay || _request.SearchType == Models.SearchType.RoundTrip && i == 0)
                {
                    organizedDestinationOption.From = _request.From1City;
                    organizedDestinationOption.To = _request.To1City;
                }

                if (_request.SearchType == Models.SearchType.RoundTrip && i == 1)
                {
                    organizedDestinationOption.From = _request.To1City;
                    organizedDestinationOption.To = _request.From1City;
                }
                if (_request.SearchType == Models.SearchType.MultiCity)
                {
                    if (i == 0)
                    {
                        organizedDestinationOption.From = _request.From1City;
                        organizedDestinationOption.To = _request.To1City;
                    }
                    else if (i == 1)
                    {
                        organizedDestinationOption.From = _request.From2City;
                        organizedDestinationOption.To = _request.To2City;
                    }
                    else if (i == 2)
                    {
                        organizedDestinationOption.From = _request.From3City;
                        organizedDestinationOption.To = _request.To3City;
                    }
                }

                organizedDestinationOption.ListFlightSegments = new List<FlightSegmentInfo>();
                for (int y = 0; y < destinationOption.FlightSegments.Count; y++)
                {
                    var organizedFlightSegment = new FlightSegmentInfo
                    {
                        Aircraft = destinationOption.FlightSegments[y].Aircraft,
                        ArrivalAirport = destinationOption.FlightSegments[y].ArrivalAirport,
                        ArrivalAirportName = destinationOption.FlightSegments[y].ArrivalAirportName,
                        ArrivalCountryCode = destinationOption.FlightSegments[y].ArrivalCountryCode,
                        ArrivalDate = destinationOption.FlightSegments[y].ArrivalDate,
                        BaggageFeeUrl = destinationOption.FlightSegments[y].BaggageFeeUrl,
                        BookingClass = destinationOption.FlightSegments[y].BookingClass,
                        Cabin = destinationOption.FlightSegments[y].Cabin,
                        DepartureAirport = destinationOption.FlightSegments[y].DepartureAirport,
                        DepartureAirportName = destinationOption.FlightSegments[y].DepartureAirportName,
                        DepartureCountryCode = destinationOption.FlightSegments[y].DepartureCountryCode,
                        DepartureDate = destinationOption.FlightSegments[y].DepartureDate,
                        Duration = destinationOption.FlightSegments[y].Duration,
                        FlightNumber = destinationOption.FlightSegments[y].FlightNumber,
                        FreeBaggages = destinationOption.FlightSegments[y].FreeBaggages,
                        MarketingAirlineCode = destinationOption.FlightSegments[y].MarketingAirlineCode,
                        MarketingAirlineName = destinationOption.FlightSegments[y].MarketingAirlineName,
                        OperatingAirlineCode = destinationOption.FlightSegments[y].OperatingAirlineCode,    
                        OperatingAirlineName = destinationOption.FlightSegments[y].OperatingAirlineName,
                        RouteNumber = destinationOption.FlightSegments[y].RouteNumber,
                        SeatsLeft = destinationOption.FlightSegments[y].SeatsLeft
                    };

                    if (organizedFlightSegment.MarketingAirlineName != organizedFlightSegment.OperatingAirlineName)
                    {
                        organizedFlightSegment.OperatedBy = string.Format("{0} {1}", AppResources.FD_OPERATED_BY, organizedFlightSegment.OperatingAirlineName);
                    }

                    organizedFlightSegment.AirlineImage = string.Format("https://farelovers.com/wp-content/themes/thomalex/images/airline-logos/{0}.png", organizedFlightSegment.MarketingAirlineCode);
                    if (y != destinationOption.FlightSegments.Count - 1)
                    {
                        var layover = destinationOption.FlightSegments[y + 1].DepartureDate - destinationOption.FlightSegments[y].ArrivalDate;
                        organizedFlightSegment.Layover = layover.Hours > 0 ? string.Format("{0}h {1}min", Math.Floor(layover.TotalHours), layover.Minutes) : string.Format("{0}min", layover.Minutes);
                    }
                    organizedDestinationOption.ListFlightSegments.Add(organizedFlightSegment);
                }

                details.ListDestinationOptions.Add(organizedDestinationOption);
                i++;
            }
            return details;
        }

    }
}

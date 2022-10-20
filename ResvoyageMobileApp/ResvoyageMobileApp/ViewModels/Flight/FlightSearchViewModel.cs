using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Views;
using ResvoyageMobileApp.Views.Flight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Flight
{
    public class FlightSearchViewModel :BaseViewModel
    {
		public FlightSearchViewModel(List<AirportInfo> NearbyAirportsList = null, List<Place> popularPlaces = null)
		{
			_request = new FlightRequestViewModel();

			if(popularPlaces != null)
				_places = new ObservableCollection<Place>(popularPlaces);

			if (NearbyAirportsList != null && NearbyAirportsList.Count > 0)
			{
				_nearbyAirports = NearbyAirportsList;
				var closestAirport = _nearbyAirports.FirstOrDefault();
				if (closestAirport != null)
				{
					_request.From1City = closestAirport.City;
					_request.From1Iata = closestAirport.Code;
				}
			}
		}
		private ObservableCollection<Place> _places;

		public ObservableCollection<Place> Places
		{
			get { return _places; }
			set {
				SetValue(ref _places, value);
			}
		}
		private List<AirportInfo> _nearbyAirports;

		public List<AirportInfo> NearbyAirports
		{
			get { return _nearbyAirports; }
			set
			{
				SetValue(ref _nearbyAirports, value);
			}
		}

		private FlightRequestViewModel _request;

		public FlightRequestViewModel Request
		{
			get { return _request; }
			set { SetValue(ref _request, value); }
		}
		private string _searchType;

		public string SearchType
		{
			get { return _searchType; }
			set { SetValue(ref _searchType, value); }
		}
		private bool _showWaitScreen;

		public bool ShowWaitScreen
		{
			get { return _showWaitScreen; }
			set { SetValue(ref _showWaitScreen, value); }
		}
		private bool _showSecondSegment;

		public bool ShowSecondSegment
		{
			get { return _showSecondSegment; }
			set { SetValue(ref _showSecondSegment, value); }
		}
		private bool _showThirdSegment;

		public bool ShowThirdSegment
		{
			get { return _showThirdSegment; }
			set { SetValue(ref _showThirdSegment, value); }
		}
		private int DisplyedSegments = 1;
		public ICommand ShowOtherInfoPage => new Command(DisplayOtherInfo);
		public ICommand ShowCalendarPage => new Command<string>(DisplayCalendar);
		public ICommand ShowCalendarPageMultiCity => new Command<string>(DisplayCalendarMultiCity);
		public ICommand ShowDestinationAutocomplete => new Command<string>(DisplayDestinationAutocomplete); 
		public ICommand SwitchDestinations => new Command<string>(switchDestinations); 
		public ICommand SearchFlights => new Command<string>(Search);
		public ICommand AddSegment => new Command(AddFlightSegment);
		public ICommand RemoveSegment => new Command(RemoveFlightSegment);
		public ICommand ShowDepartureAutocompleteMultiCity => new Command<string>(DisplayDepartureAutocompleteMultiCity);
		public ICommand ShowArrivalAutocompleteMultiCity => new Command<string>(DisplayArrivalAutocompleteMultiCity); 
		public ICommand ShowPlaceInfo => new Command<Place>(DisplayPlaceInfo);

		private ObservableCollection<Place> GetPlaces() {
			return new ObservableCollection<Place> {
				new Place{ Id = 0, AvailableFlights=29,City="Paris",Country="France",Image ="paris.png", IATACode = "CDG", Currency="EUR", TimeZone="GMT+02:00", About="Paris, city and capital of France, situated in the north-central part of the country. People were living on the site of the present-day city, located along the Seine River some 233 miles (375 km) upstream from the river’s mouth on the English Channel (La Manche), by about 7600 BCE. The modern city has spread from the island (the Île de la Cité) and far beyond both banks of the Seine." },
				new Place{ Id = 1, AvailableFlights=45,City="Venecie",Country="Italy",Image ="venecie.png", IATACode = "VCE", Currency="EUR", TimeZone = "GMT+02:00", About="Venice is a city in Italy. It is the capital of the Veneto region, which is in the north-east of the country. The population of the 'Comune di Venezia', which is Venice, its lagoon and its mainland is 271,367. Area is 412 km². The population of Venice itself keeps on shrinking at an alarming rate and is now under 55000 locals." },
				new Place{ Id = 2, AvailableFlights=24,City="Madrid",Country="Spain",Image ="madrid.png", IATACode = "MAD", Currency="EUR", TimeZone = "GMT+02:00", About = "Madrid is the capital city of Spain, located right in the centre of the Iberian Peninsula. Its geographical location grants good communications of the city with other Spanish regions. As capital of the country, it is the seat to the Spanish government institutions and the city of residence of Spanish Royal family."},
				new Place{ Id = 3, AvailableFlights=42,City="Beijing",Country="China",Image ="beijing.png", IATACode = "PEK", Currency = "CNY", TimeZone = "GMT-08:00", About="Beijing, Wade-Giles romanization Pei-ching, conventional Peking, city, province-level shi (municipality), and capital of the People’s Republic of China. Few cities in the world have served for so long as the political headquarters and cultural centre of an area as immense as China. The city has been an integral part of China’s history over the past eight centuries, and nearly every major building of any age in Beijing has at least some national historical significance. The importance of Beijing thus makes it impossible to understand China without a knowledge of this city." },
				new Place{ Id = 4, AvailableFlights=33,City="San Francisco",Country="USA",Image ="san_francisco.png", IATACode = "SFO", Currency = "USD", TimeZone = "GMT-07:00", About="San Francisco, city and port, coextensive with San Francisco county, northern California, U.S., located on a peninsula between the Pacific Ocean and San Francisco Bay. It is a cultural and financial centre of the western United States and one of the country’s most cosmopolitan cities. Area 46 square miles (120 square km). Pop. (2000) 776,733; San Francisco–San Mateo–Redwood City Metro Division, 1,731,183; San Francisco–Oakland–Fremont Metro Area, 4,123,740; (2010) 805,235; San Francisco–San Mateo–Redwood City Metro Division, 1,776,095; San Francisco–Oakland–Fremont Metro Area, 4,335,391." },
				new Place{ Id = 5, AvailableFlights=12,City="Seychelles",Country="Africa",Image ="seychelles.png", IATACode = "SEZ", Currency = "SCR", TimeZone = "GMT+04:00", About="Seychelles, island republic in the western Indian Ocean, comprising about 115 islands, with lush tropical vegetation, beautiful beaches, and a wide variety of marine life. Situated between latitudes 4° and 11° S and longitudes 46° and 56° E, the major islands of Seychelles are located about 1,000 miles (1,600 km) east of Kenya and about 700 miles (1,100 km) northeast of Madagascar. The capital, Victoria, is situated on the island of Mahé." },
			};
		}
		private void AddFlightSegment()
		{
			if (DisplyedSegments == 1)
			{
				ShowSecondSegment = true;
				DisplyedSegments++;
			}
			else if (DisplyedSegments == 2)
			{
				ShowThirdSegment = true;
				DisplyedSegments++;
			}
		}
		private void RemoveFlightSegment()
		{
			if (DisplyedSegments == 2)
			{
				ShowSecondSegment = false;
				DisplyedSegments--;
			}
			else if (DisplyedSegments == 3)
			{
				ShowThirdSegment = false;
				DisplyedSegments--;
			}
		}
		private void DisplayOtherInfo()
		{
			var page = new OtherInfoFlightPage(_request);
			var navigation = Application.Current.MainPage as Shell;
			navigation.Navigation.PushAsync(page, true);
		}
		private void DisplayCalendar(string type)
		{
			var page = new CalendarPage(_request, type);
			var navigation = Application.Current.MainPage as Shell;
			navigation.Navigation.PushAsync(page, true);
		}
		private void DisplayCalendarMultiCity(string multiCitySegment)
		{
			int segment;
			if (Int32.TryParse(multiCitySegment, out segment))
			{
				var page = new CalendarPage(_request, "MultiCity", segment);
				var navigation = Application.Current.MainPage as Shell;
				navigation.Navigation.PushAsync(page, true);
			}
			else
			{
				Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.ER_SOME_ERROR_OCCURED, AppResources.APP_OK);
			}
		}
		private void DisplayDestinationAutocomplete(string type)
		{
			var page = new DestinationsAutocompletePage(_request, type, nearbyAirports:_nearbyAirports);
			var navigation = Application.Current.MainPage as Shell;
			navigation.Navigation.PushAsync(page, true);
		}
		private void DisplayDepartureAutocompleteMultiCity(string multiCitySegment)
		{
			int segment;
			if (Int32.TryParse(multiCitySegment, out segment))
			{
				var page = new DestinationsAutocompletePage(_request, "departure", segment, _nearbyAirports);
				var navigation = Application.Current.MainPage as Shell;
				navigation.Navigation.PushAsync(page, true);
			}
			else
			{
				Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.ER_SOME_ERROR_OCCURED, AppResources.APP_OK);
			}
		}
		private void DisplayArrivalAutocompleteMultiCity(string multiCitySegment)
		{
			int segment;
			if (Int32.TryParse(multiCitySegment, out segment))
			{
				var page = new DestinationsAutocompletePage(_request, "arrival", segment, _nearbyAirports);
				var navigation = Application.Current.MainPage as Shell;
				navigation.Navigation.PushAsync(page, true);
			}
			else
			{
				Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.ER_SOME_ERROR_OCCURED, AppResources.APP_OK);
			}
		}
		private void Search(string type)
		{
			if (type == "OneWay")
				_request.SearchType = Models.SearchType.OneWay;
			else if(type == "RoundTrip")
				_request.SearchType = Models.SearchType.RoundTrip;
			else if (type == "MultiCity")
			{
				_request.SearchType = Models.SearchType.MultiCity;
				_request.MultiCityActieSegments = DisplyedSegments;
			}
			if (ValidateRequest())
			{
				_showWaitScreen = true;
				var page = new FlightResultPage(_request);
				var navigation = Application.Current.MainPage as Shell;
				navigation.Navigation.PushAsync(page);
			}			
		}
		public void DisplayPlaceInfo(Place place)
		{
			var page = new PopularPlaceDetailsPage(_places, place,_request, _searchType);
			_navigation.Navigation.PushAsync(page, true);
		}
		private void switchDestinations(string multiCitySegment)
		{
			if (_searchType == "OneWay" || _searchType == "RoundTrip")
			{
				var tmpIata = _request.From1Iata;
				var tmpCity = _request.From1City;

				_request.From1Iata = _request.To1Iata;
				_request.From1City = _request.To1City;
				_request.To1Iata = tmpIata;
				_request.To1City = tmpCity;
			}
			else if (_searchType == "MultiCity")
			{
				var tmpIata = string.Empty;
				var tmpCity = string.Empty;
				switch (multiCitySegment)
				{
					case "1":
						tmpIata = _request.From1Iata;
						tmpCity = _request.From1City;

						_request.From1Iata = _request.To1Iata;
						_request.From1City = _request.To1City;
						_request.To1Iata = tmpIata;
						_request.To1City = tmpCity;
						break;
					case "2":
						tmpIata = _request.From2Iata;
						tmpCity = _request.From2City;

						_request.From2Iata = _request.To2Iata;
						_request.From2City = _request.To2City;
						_request.To2Iata = tmpIata;
						_request.To2City = tmpCity;
						break;
					case "3":
						tmpIata = _request.From3Iata;
						tmpCity = _request.From3City;

						_request.From3Iata = _request.To3Iata;
						_request.From3City = _request.To3City;
						_request.To3Iata = tmpIata;
						_request.To3City = tmpCity;
						break;
				}	
			}
		}

		private bool ValidateRequest()
		{
			var errorMessage = string.Empty;
			var errorCount = 0;

			if (string.IsNullOrEmpty(_request.From1Iata)) 
			{
				errorMessage += errorCount > 0 ? AppResources.SF_FROM  : AppResources.SF_FROM;
				errorCount++;
			}
			if (string.IsNullOrEmpty(_request.To1Iata))
			{
				errorMessage += errorCount > 0 ? ", " + AppResources.SF_DESTINATION : AppResources.SF_DESTINATION;
				errorCount++;
			}
			if (string.IsNullOrEmpty(_request.DepartureDate1))
			{
				errorMessage += errorCount > 0 ? ", " + AppResources.SF_DEPARTURE : AppResources.SF_DEPARTURE;
				errorCount++;
			}
			if (string.IsNullOrEmpty(_request.DepartureDate2) && _request.SearchType == Models.SearchType.RoundTrip)
			{
				errorMessage += errorCount > 0 ? ", " + AppResources.SF_RETURN : AppResources.SF_RETURN;
				errorCount++;
			}
			if (_request.SearchType == Models.SearchType.MultiCity)
			{
				if (_request.MultiCityActieSegments > 1)
				{
					if (string.IsNullOrEmpty(_request.DepartureDate2))
					{
						errorMessage += errorCount > 0 ? ", " + AppResources.SF_DEPARTURE_SECOND_SEGMENT : AppResources.SF_DEPARTURE_SECOND_SEGMENT;
						errorCount++;
					}
					if (string.IsNullOrEmpty(_request.From2Iata))
					{
						errorMessage += errorCount > 0 ? AppResources.SF_FROM_SECOND_SEGMENT : AppResources.SF_FROM_SECOND_SEGMENT;
						errorCount++;
					}
					if (string.IsNullOrEmpty(_request.To2Iata))
					{
						errorMessage += errorCount > 0 ? ", " + AppResources.SF_DESTINATION : AppResources.SF_DESTINATION;
						errorCount++;
					}
				}
				if (_request.MultiCityActieSegments > 2)
				{
					if (string.IsNullOrEmpty(_request.DepartureDate3))
					{
						errorMessage += errorCount > 0 ? ", " + AppResources.SF_DEPARTURE_THIRD_SEGMENT : AppResources.SF_DEPARTURE_THIRD_SEGMENT;
						errorCount++;
					}
					if (string.IsNullOrEmpty(_request.From3Iata))
					{
						errorMessage += errorCount > 0 ? AppResources.SF_FROM_THIRD_SEGMENT : AppResources.SF_FROM_THIRD_SEGMENT;
						errorCount++;
					}
					if (string.IsNullOrEmpty(_request.To3Iata))
					{
						errorMessage += errorCount > 0 ? ", " + AppResources.SF_DESTINATION : AppResources.SF_DESTINATION;
						errorCount++;
					}
				}
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

using Newtonsoft.Json;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Services.Flight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ResvoyageMobileApp.ViewModels.Flight
{
    public class DestinationAutocompleteViewModel : BaseViewModel
    {
		private DestinationAutocompleteService desinationService;
		public DestinationAutocompleteViewModel(FlightRequestViewModel flightRequestView, string requestedType, int? segment = null, List<AirportInfo> nearbyAirpots = null)
		{
			_request = flightRequestView;
			_type = requestedType;
			_selectedDestination = new AirportInfo();
			_results = new ObservableCollection<AirportInfo>();
			desinationService = new DestinationAutocompleteService();
			if (segment != null)
			{
				_multiCitySegment = segment;
			}
			if (_type == "departure")
			{
				var jsonObject = Application.Current.Properties.ContainsKey("AirFromSearch") ? Application.Current.Properties["AirFromSearch"]?.ToString() : null;
				List<AirportInfo> recentSearches = !string.IsNullOrEmpty(jsonObject) ? JsonConvert.DeserializeObject<List<AirportInfo>>(jsonObject).OrderByDescending(x => x.SelectedDate).ToList() : new List<AirportInfo>();
				_recentSearches = new ObservableCollection<AirportInfo>(recentSearches);

				if (segment == null || segment == 1)
				{
					if (nearbyAirpots != null)
						_nearbyAirports = nearbyAirpots.Take(2).ToList();
				}
			}
			if (_type == "arrival")
			{
				var jsonObject = Application.Current.Properties.ContainsKey("AirToSearch") ? Application.Current.Properties["AirToSearch"]?.ToString() : null;
				List<AirportInfo> recentSearches = !string.IsNullOrEmpty(jsonObject) ? JsonConvert.DeserializeObject<List<AirportInfo>>(jsonObject).OrderByDescending(x => x.SelectedDate).ToList() : new List<AirportInfo>();
				_recentSearches = new ObservableCollection<AirportInfo>(recentSearches);

			}
		}
		private ObservableCollection<AirportInfo> _results;

		public ObservableCollection<AirportInfo> Results
		{
			get { return _results; }
			set { SetValue(ref _results, value); }
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
		private ObservableCollection<AirportInfo> _recentSearches;

		public ObservableCollection<AirportInfo> RecentSearches
		{
			get { return _recentSearches; }
			set { SetValue(ref _recentSearches, value); }
		}
		private FlightRequestViewModel _request;

		public FlightRequestViewModel Request
		{
			get { return _request; }
			set { SetValue(ref _request, value); }
		}
		private string _type;

		public string Type
		{
			get { return _type; }
			set { SetValue(ref _type, value); }
		}
		public string PlaceholderText
		{
			get
			{
				return _type == "departure" ? AppResources.SF_FROM : AppResources.SF_DESTINATION;
			}
		}
		private int? _multiCitySegment;

		public int? MultiCitySegment
		{
			get { return _multiCitySegment; }
			set { SetValue(ref _multiCitySegment, value); }
		}
		private string _search;

		public string Search
		{
			get { return _search; }
			set {
				if (_search != value)
					getDestinations(value);
				SetValue(ref _search, value); }
		}
		public ICommand SelecetAirportCommand => new Command<AirportInfo>(SelecetAirport);

		private AirportInfo _selectedDestination;

		public AirportInfo SelectedDestination
		{
			get { return _selectedDestination; }
			set {
				SetValue(ref _selectedDestination, value);
				SelecetAirport(value);
			}
		}

		private void SelecetAirport(AirportInfo value)
		{
			if (_type == "departure" && (_multiCitySegment == null || _multiCitySegment == 1) && value != null && value.Code != null)
			{
				_request.From1Iata = value.Code;
				_request.From1City = value.City;
			}

			if (_type == "arrival" && (_multiCitySegment == null || _multiCitySegment == 1) && value != null && value.Code != null)
			{
				_request.To1Iata = value.Code;
				_request.To1City = value.City;
			}

			if (_type == "departure" && _multiCitySegment == 2 && value != null && value.Code != null)
			{
				_request.From2Iata = value.Code;
				_request.From2City = value.City;
			}

			if (_type == "arrival" && _multiCitySegment == 2 && value != null && value.Code != null)
			{
				_request.To2Iata = value.Code;
				_request.To2City = value.City;
			}

			if (_type == "departure" && _multiCitySegment == 3 && value != null && value.Code != null)
			{
				_request.From3Iata = value.Code;
				_request.From3City = value.City;
			}

			if (_type == "arrival" && _multiCitySegment == 3 && value != null && value.Code != null)
			{
				_request.To3Iata = value.Code;
				_request.To3City = value.City;
			}

			if (_type == "departure" && value != null && value.Code != null)
			{
				var jsonObject = Application.Current.Properties.ContainsKey("AirFromSearch") ? Application.Current.Properties["AirFromSearch"]?.ToString() : null;
				var recentSearches = !string.IsNullOrEmpty(jsonObject) ? JsonConvert.DeserializeObject<List<AirportInfo>>(jsonObject) : new List<AirportInfo>();
				if (!recentSearches.Any(x => x.Name == value.Name && x.Code == value.Code))
				{
					if (recentSearches.Count == 4)
						recentSearches.Remove(recentSearches.FirstOrDefault());

					value.SelectedDate = DateTime.Now;
					recentSearches.Add(value);
				}
				else
				{
					var place = recentSearches.FirstOrDefault(x => x.Name == value.Name && x.Code == value.Code);
					if (place != null)
					{
						place.SelectedDate = DateTime.Now;
					}
				}
				jsonObject = JsonConvert.SerializeObject(recentSearches);
				Application.Current.Properties["AirFromSearch"] = jsonObject;
				Application.Current.SavePropertiesAsync();

			}
			if (_type == "arrival" && value != null && value.Code != null)
			{
				var jsonObject = Application.Current.Properties.ContainsKey("AirToSearch") ? Application.Current.Properties["AirToSearch"]?.ToString() : null;
				var recentSearches = !string.IsNullOrEmpty(jsonObject) ? JsonConvert.DeserializeObject<List<AirportInfo>>(jsonObject) : new List<AirportInfo>();
				if (!recentSearches.Any(x => x.Name == value.Name && x.Code == value.Code))
				{
					if (recentSearches.Count == 4)
						recentSearches.Remove(recentSearches.FirstOrDefault());

					value.SelectedDate = DateTime.Now;
					recentSearches.Add(value);
				}
				else
				{
					var place = recentSearches.FirstOrDefault(x => x.Name == value.Name && x.Code == value.Code);
					if (place != null)
					{
						place.SelectedDate = DateTime.Now;
					}
				}
				jsonObject = JsonConvert.SerializeObject(recentSearches);
				Application.Current.Properties["AirToSearch"] = jsonObject;
				Application.Current.SavePropertiesAsync();

			}

			var navigation = Application.Current.MainPage as Shell;
			navigation.Navigation.PopAsync(true);

		}
		private async void getDestinations(string search)
		{
			try
			{
				var response = await desinationService.GetDestinationAutocompleteResults(search);
				if (response != null && response.Airports != null && response.Airports.Count > 0)
				{
					Results = new ObservableCollection<AirportInfo>(response.Airports as List<AirportInfo>);
				}
				else
				{
					Results = new ObservableCollection<AirportInfo>();
				}
				
			}
			catch (Exception e)
			{
				await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR,e.Message, AppResources.APP_OK);
			}
		}
	}
}

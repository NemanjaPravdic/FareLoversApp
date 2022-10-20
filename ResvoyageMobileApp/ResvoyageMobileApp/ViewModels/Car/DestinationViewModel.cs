using Newtonsoft.Json;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Services.Flight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Car
{
    public class DestinationViewModel : BaseViewModel
    {
		private DestinationAutocompleteService desinationService;

		public DestinationViewModel(CarRequestViewModel requestViewModel, string type)
		{
			_request = requestViewModel;
			_type = type;
			_results = new ObservableCollection<AirportInfo>();
			_selectedDestination = new AirportInfo();
			desinationService = new DestinationAutocompleteService();
			var jsonObject = Application.Current.Properties.ContainsKey("CarLocationSearch") ? Application.Current.Properties["CarLocationSearch"]?.ToString() : null;
			List<AirportInfo> recentSearches = !string.IsNullOrEmpty(jsonObject) ? JsonConvert.DeserializeObject<List<AirportInfo>>(jsonObject).OrderByDescending(x => x.SelectedDate).ToList() : new List<AirportInfo>();
			_recentSearches = new ObservableCollection<AirportInfo>(recentSearches);
		}

		private ObservableCollection<AirportInfo> _results;

		public ObservableCollection<AirportInfo> Results
		{
			get { return _results; }
			set { SetValue(ref _results, value); }
		}
		private ObservableCollection<AirportInfo> _recentSearches;

		public ObservableCollection<AirportInfo> RecentSearches
		{
			get { return _recentSearches; }
			set { SetValue(ref _recentSearches, value); }
		}
		private CarRequestViewModel _request;

		public CarRequestViewModel Request
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
				return _type == "PickUp" ? AppResources.CS_PICK_UP_LOCATION : AppResources.CS_DROP_OFF_LOCATION;
			}
		}
		private string _search;

		public string Search
		{
			get { return _search; }
			set
			{
				if (_search != value)
					getDestinations(value);
				SetValue(ref _search, value);
			}
		}
		private AirportInfo _selectedDestination;

		public AirportInfo SelectedDestination
		{
			get { return _selectedDestination; }
			set
			{
				SetValue(ref _selectedDestination, value);

				if (_type == "DropOff" || string.IsNullOrEmpty(_request.DropOffCity) || _request.DropOffCity == _request.PickupCity && value != null && value.Code != null)
				{
					_request.DropOffCityIata = value.Code;
					_request.DropOffCity = value.City;
				}
				if (_type == "PickUp" && value != null && value.Code != null)
				{
					_request.PickupCityIata = value.Code;
					_request.PickupCity = value.City;
				}

				if (value != null && value.Code != null)
				{
					var jsonObject = Application.Current.Properties.ContainsKey("CarLocationSearch") ? Application.Current.Properties["CarLocationSearch"]?.ToString() : null;
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
					Application.Current.Properties["CarLocationSearch"] = jsonObject;
					Application.Current.SavePropertiesAsync();

				}

				_navigation.Navigation.PopAsync(true);

			}
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
				await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, e.Message, AppResources.APP_OK);
			}
		}
	}
}

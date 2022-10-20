using Newtonsoft.Json;
using ResvoyageMobileApp.Models.Hotel;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Services.Hotel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Hotel
{
    public class HotelDestinationAutocompleteViewModel : BaseViewModel
    {
        private HotelDestinationService destinationService;
        public HotelDestinationAutocompleteViewModel(HotelRequestViewModel request)
        {
            _request = request;
            _results = new ObservableCollection<HotelCityData>();
            destinationService = new HotelDestinationService();
            var jsonObject = Application.Current.Properties.ContainsKey("HotelLocationSearch") ? Application.Current.Properties["HotelLocationSearch"]?.ToString() : null;
            List<HotelCityData> recentSearches = !string.IsNullOrEmpty(jsonObject) ? JsonConvert.DeserializeObject<List<HotelCityData>>(jsonObject).OrderByDescending(x => x.SelectedDate).ToList() : new List<HotelCityData>();
            _recentSearches = new ObservableCollection<HotelCityData>(recentSearches);
        }
        private HotelRequestViewModel _request;

        public HotelRequestViewModel Request
        {
            get { return _request; }
            set { SetValue(ref _request, value); }
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
        private ObservableCollection<HotelCityData> _results;

        public ObservableCollection<HotelCityData> Results
        {
            get { return _results; }
            set { SetValue(ref _results, value); }
        }
        private ObservableCollection<HotelCityData> _recentSearches;

        public ObservableCollection<HotelCityData> RecentSearches
        {
            get { return _recentSearches; }
            set { SetValue(ref _recentSearches, value); }
        }
        private HotelCityData _selectedDestination;

        public HotelCityData SelectedDestination
        {
            get { return _selectedDestination; }
            set
            {
                SetValue(ref _selectedDestination, value);
                if (value != null)
                {
                    _request.CityName = value.Name;
                    _request.PlaceId = value.PlaceId;
                    _request.HotelCityCode = value.Code;

                    var jsonObject = Application.Current.Properties.ContainsKey("HotelLocationSearch") ? Application.Current.Properties["HotelLocationSearch"]?.ToString() : null;
                    var recentSearches = !string.IsNullOrEmpty(jsonObject) ? JsonConvert.DeserializeObject<List<HotelCityData>>(jsonObject) : new List<HotelCityData>();
                    if (!recentSearches.Any(x => x.Name == value.Name && x.Code == value.Code))
                    {
                        if (recentSearches.Count == 5)
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
                    Application.Current.Properties["HotelLocationSearch"] = jsonObject;
                    Application.Current.SavePropertiesAsync();

                    var navigation = Application.Current.MainPage as Shell;
                    navigation.Navigation.PopAsync(true);
                }
            }
        }

        private async void getDestinations(string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var response = await destinationService.GetDestinationAutocompleteResults(value);
                    if (response != null && response.Resutls != null && response.Resutls.Count > 0)
                    {
                        var validDestinations = new List<HotelCityData>();

                        foreach (var destination in response.Resutls)
                        {
                            if (!string.IsNullOrEmpty(destination.Name))
                                validDestinations.Add(destination);
                        }

                        if (validDestinations?.Count > 0)
                            Results = new ObservableCollection<HotelCityData>(validDestinations as List<HotelCityData>);
                        else
                            Results = null;
                    }
                    else
                    {
                        Results = null;
                    }
                    
                }
                else
                {
                    Results = new ObservableCollection<HotelCityData>();
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, e.Message, AppResources.APP_OK);
            }
        }
    }
}

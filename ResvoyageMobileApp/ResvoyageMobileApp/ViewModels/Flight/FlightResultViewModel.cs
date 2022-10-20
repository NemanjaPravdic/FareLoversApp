using Newtonsoft.Json;
using ResvoyageMobileApp.Models.Flight;
using ResvoyageMobileApp.Models.Other;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Services;
using ResvoyageMobileApp.Views.Flight;
using Rg.Plugins.Popup.Extensions;
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
    public class FlightResultViewModel :BaseViewModel
    {
		private CurrencyConversionService _currencyConversionService;
		public FlightResultViewModel(ObservableCollection<PreparedFlightResults> preparedFlightResults, FlightRequestViewModel flightResultViewModel)
		{
			_currencyConversionService = new CurrencyConversionService();
			_currency = SelectedCurrency;
			_results = preparedFlightResults;
			_request = flightResultViewModel;
			_filterdResults = preparedFlightResults;
			_filters = new FiltersViewModel(preparedFlightResults);

			if (_results != null && _results.Count > 0 && _results.FirstOrDefault().Currency != _currency.CurrencyCode)
			{ 
				ExchangePrices();
			}

		}
		private ObservableCollection<PreparedFlightResults> _results;

		public ObservableCollection<PreparedFlightResults>  Results
		{
			get { return _results; }
			set { SetValue(ref _results, value); }
		}
		private ObservableCollection<PreparedFlightResults> _filterdResults;

		public ObservableCollection<PreparedFlightResults> FilterdResults
		{
			get { return _filterdResults; }
			set { SetValue(ref _filterdResults, value); }
		}
		private FlightRequestViewModel _request;

		public FlightRequestViewModel Request
		{
			get { return _request; }
			set { SetValue(ref _request, value); }
		}
		private PreparedFlightResults _selectedFlight;

		public PreparedFlightResults SelectedFlight
		{
			get { return _selectedFlight; }
			set { 
				SetValue(ref _selectedFlight, value);
				
				if (value != null)
				{
					var page = new FlightDetailsPage(value, _request);
					var navigation = Application.Current.MainPage as Shell;
					navigation.Navigation.PushAsync(page, true);
					SelectedFlight = null;

				}

			}
		}
		private CurrencyInfo _currency;
		public CurrencyInfo Currency
		{
			get { return _currency; }
			set
			{
				SetValue(ref _currency, value);
				if (value.CurrencyCode != SelectedCurrency.CurrencyCode)
					ExchangePrices();
				var currencyJson = JsonConvert.SerializeObject(value);
				Application.Current.Properties["Currency"] = currencyJson;

				
			}
		}
		private FiltersViewModel _filters;

		public FiltersViewModel Filters
		{
			get { return _filters; }
			set { SetValue(ref _filters, value); }
		}
		public ICommand ShowFilters => new Command(DisplayFilters);
		public ICommand ChangeSorting => new Command<SortFlightViewModel>(ChangeFlightSorting);
		public ICommand PickerFocusCommand => new Command<Object>(PickerFocus);


		private void ChangeFlightSorting(SortFlightViewModel obj)
		{
			Filters.Sort.ForEach(x => x.IsSelected = false);
			obj.IsSelected = true;

			if (obj.Title == AppResources.FF_CHEAPEST)
			{
				FilterdResults = new ObservableCollection<PreparedFlightResults>(FilterdResults.OrderBy(x => x.Total));
			}
			else if (obj.Title == AppResources.FF_QUICKEST)
			{
				FilterdResults = new ObservableCollection<PreparedFlightResults>(FilterdResults.OrderBy(x => x.TotalDuration));
			}
			else if (obj.Title == AppResources.FF_EARLIEST)
			{
				FilterdResults = new ObservableCollection<PreparedFlightResults>(FilterdResults.OrderBy(x => x.FirstDateFrom));
			}
		}

		private void DisplayFilters()
		{
			var page = new FlightFiltersPage(this);
			var navigation = Application.Current.MainPage as Shell;
			navigation.Navigation.PushAsync(page,true);
		}

		public string RequestTextInfo
		{
			get
			{
				var passengers = _request.Adults + _request.Children + _request.Infants;
				return string.Format("{0} {1}, {2} {3}", _request.Cabin.ToString(), AppResources.FR_CABIN, passengers, passengers == 1 ? AppResources.FR_PASSENGER : AppResources.FR_PASSENGERS);
			}
		}
		private void PickerFocus(Object obj)
		{
			var picker = (Picker)obj;
			picker.Focus();
			Shell.Current.FlyoutIsPresented = false;
		}
		private async void ExchangePrices()
		{
			if (Results != null && Results.Count > 0)
			{
				var fromCurrency = Results.FirstOrDefault().Currency;
				var rate = await _currencyConversionService.CurrencyConversion(fromCurrency, Currency.CurrencyCode);

				var tmp = Results;
				foreach (var result in tmp)
				{
					result.Currency = Currency.CurrencyCode;
					result.Total = result.Total * rate;
				}
				Results = new ObservableCollection<PreparedFlightResults>(tmp);

				if (FilterdResults != null && FilterdResults.Count > 0)
				{
					tmp = FilterdResults;
					foreach (var result in tmp)
					{
						if (result.Currency != Currency.CurrencyCode)
						{
							result.Currency = Currency.CurrencyCode;
							result.Total = result.Total * rate;
						}						
					}
					FilterdResults = new ObservableCollection<PreparedFlightResults>(tmp);
				}
			}				
			
		}

	}
}

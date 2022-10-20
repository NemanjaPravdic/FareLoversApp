using Newtonsoft.Json;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Models.Car;
using ResvoyageMobileApp.Models.Other;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Services;
using ResvoyageMobileApp.Views.Car;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Car
{
    public class CarResultViewModel : BaseViewModel
    {
		private CurrencyConversionService _currencyConversionService;
		public CarResultViewModel(CarRequestViewModel request, CarSearchResponse response)
        {
			_currencyConversionService = new CurrencyConversionService();
			_currency = SelectedCurrency;
			_request = request;
			if (response != null)
			{
				_sessionId = response.SessionId;
				_filters = new FiltersViewModel(response.Cars);
				_results = new ObservableCollection<CarInformation>(response.Cars);
				_filterdResults = new ObservableCollection<CarInformation>(response.Cars);
				if (response.Cars != null && response.Cars.Count > 0)
				{
					_minPrice = Math.Floor(response.Cars.Min(x => x.VehicleInfo.RateTotalAmount));
					_maxPrice = Math.Ceiling(response.Cars.Max(x => x.VehicleInfo.RateTotalAmount));
					_currencySymbol = response.Cars[0].VehicleInfo.CurrencySymbol;

					if (_results.FirstOrDefault().VehicleInfo.CurrencyCode != _currency.CurrencyCode)
						ExchangePrices();
				}
			}
		}
		private Guid _sessionId;

		public Guid SessionId
		{
			get { return _sessionId; }
			set { SetValue(ref _sessionId, value); }
		}

		private ObservableCollection<CarInformation> _results;

		public ObservableCollection<CarInformation> Results
		{
			get { return _results; }
			set { SetValue(ref _results, value); }
		}
		private ObservableCollection<CarInformation> _filterdResults;

		public ObservableCollection<CarInformation> FilterdResults
		{
			get { return _filterdResults; }
			set { SetValue(ref _filterdResults, value); }
		}
		private CarInformation _selectedCar;

		public CarInformation SelectedCar
		{
			get { return _selectedCar; }
			set
			{
				SetValue(ref _selectedCar, value);

				if (value != null)
				{
					var page = new CarDetailsPage(value.Id, _request, _sessionId, value);
					_navigation.Navigation.PushAsync(page, true);
					SelectedCar = null;
				}

			}
		}
		private CarRequestViewModel _request;

		public CarRequestViewModel Request
		{
			get { return _request; }
			set { SetValue(ref _request, value); }
		}
		public FiltersViewModel _filters;
		public FiltersViewModel Filters
		{
			get { return _filters; }
			set { SetValue(ref _filters, value); }
		}
		private decimal _minPrice;

		public decimal MinPrice
		{
			get { return _minPrice; }
			set { SetValue(ref _minPrice, value); }
		}
		private decimal _maxPrice;

		public decimal MaxPrice
		{
			get { return _maxPrice; }
			set { SetValue(ref _maxPrice, value); }
		}
		private string _currencySymbol;

		public string CurrencySymbol
		{
			get { return _currencySymbol; }
			set { SetValue(ref _currencySymbol, value); }
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
		public ICommand ChangeSorting => new Command<CheckboxViewModel>(ChangeHotelSorting);
		public ICommand ShowFilters => new Command(DisplayFilters);
		public ICommand PickerFocusCommand => new Command<Object>(PickerFocus);
		private void ChangeHotelSorting(CheckboxViewModel obj)
		{
			Filters.Sort.ForEach(x => x.IsSelected = false);
			obj.IsSelected = true;

			if (obj.Title == AppResources.FF_CHEAPEST)
			{
				FilterdResults = new ObservableCollection<CarInformation>(FilterdResults.OrderBy(x => x.VehicleInfo.RateTotalAmount));
			}
			else if (obj.Title == AppResources.CR_CAR_NAME)
			{
				FilterdResults = new ObservableCollection<CarInformation>(FilterdResults.OrderBy(x => x.VehicleInfo.VehModel));
			}
			else if (obj.Title == AppResources.CR_CATEGORY)
			{
				FilterdResults = new ObservableCollection<CarInformation>(FilterdResults.OrderBy(x => x.VehicleInfo.VehClass));
			}
		}
		private void DisplayFilters()
		{
			var page = new CarFiltersPage(this);
			_navigation.Navigation.PushAsync(page, true);
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
				var fromCurrency = Results.FirstOrDefault().VehicleInfo.CurrencyCode;
				var rate = await _currencyConversionService.CurrencyConversion(fromCurrency, Currency.CurrencyCode);

				var symbol = Util.GetCurrencySymbol(Currency.CurrencyCode);

				if (Currency.CurrencyCode == "UZS" || Currency.CurrencyCode == symbol)
					CurrencySymbol = Currency.CurrencyCode + " ";
				else
					CurrencySymbol = string.Format("{0} {1}", Currency.CurrencyCode, symbol);
				var tmp = new List<CarInformation>(Results);
				var tmpFilterd = new List<CarInformation>(FilterdResults);
				foreach (var result in tmp)
				{
					result.VehicleInfo.CurrencyCode = Currency.CurrencyCode;
					result.VehicleInfo.BasePrice = result.VehicleInfo.BasePrice * rate;
					result.VehicleInfo.RateTotalAmount = result.VehicleInfo.RateTotalAmount * rate;
					result.VehicleInfo.Total = result.VehicleInfo.Total * rate;
					result.VehicleInfo.Daily = result.VehicleInfo.Daily * rate;
				}
				Results = new ObservableCollection<CarInformation>(tmp);
				MinPrice = Math.Floor(Results.Min(x => x.VehicleInfo.RateTotalAmount));
				MaxPrice = Math.Ceiling(Results.Max(x => x.VehicleInfo.RateTotalAmount));

				Filters.MinPrice = Math.Floor(Filters.MinPrice * rate);
				Filters.MaxPrice = Math.Ceiling(Filters.MaxPrice * rate);

				if (tmpFilterd != null && tmpFilterd.Count > 0)
				{
					foreach (var result in tmpFilterd)
					{
						if (result.VehicleInfo.CurrencyCode != Currency.CurrencyCode)
						{
							result.VehicleInfo.CurrencyCode = Currency.CurrencyCode;
							result.VehicleInfo.BasePrice = result.VehicleInfo.BasePrice * rate;
							result.VehicleInfo.RateTotalAmount = result.VehicleInfo.RateTotalAmount * rate;
							result.VehicleInfo.Total = result.VehicleInfo.Total * rate;
							result.VehicleInfo.Daily = result.VehicleInfo.Daily * rate;
						}
					}
					FilterdResults = new ObservableCollection<CarInformation>(tmpFilterd);
				}

			}
		}
	}
}

using ResvoyageMobileApp.Models.Flight;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Views.Flight;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Flight
{
    public class FlightFiltersViewModel : BaseViewModel
    {
		public FlightFiltersViewModel(FlightResultViewModel resultsModel)
		{
			_results = resultsModel;
			_choosenFilters = new FiltersViewModel()
			{
				Airlines = new List<CheckboxViewModel>(),
				ChoosenAirlines = new List<CheckboxViewModel>(),
				Stops = new List<FlightStopsViewModel>(),
				InboundArrivalFrom = _results.Filters.InboundArrivalFrom,
				InboundArrivalTo = _results.Filters.InboundArrivalTo,
				InboundDepartureFrom = _results.Filters.InboundDepartureFrom,
				InboundDepartureTo = _results.Filters.InboundDepartureTo,
				OutboundArrivalFrom = _results.Filters.OutboundArrivalFrom,
				OutboundArrivalTo = _results.Filters.OutboundArrivalTo,
				OutboundDepartureFrom = _results.Filters.OutboundDepartureFrom,
				OutboundDepartureTo = _results.Filters.OutboundDepartureTo,
				SelectedAirlines = _results.Filters.SelectedAirlines,
				Sort = _results.Filters.Sort
			};
			_results.Filters.Airlines.ForEach(x => _choosenFilters.Airlines.Add(new CheckboxViewModel { IsSelected = x.IsSelected, Title = x.Title}));
			_results.Filters.Stops.ForEach(x => _choosenFilters.Stops.Add(new FlightStopsViewModel { IsSelected = x.IsSelected, Title = x.Title, Value = x.Value}));
		}
		private FlightResultViewModel _results;

		public FlightResultViewModel Results
		{
			get { return _results; }
			set { SetValue(ref _results, value); }
		}
		private FiltersViewModel _choosenFilters;

		public FiltersViewModel ChoosenFilters
		{
			get { return _choosenFilters; }
			set { SetValue(ref _choosenFilters, value); }
		}
		public ICommand ApplyFilters => new Command(ApplyFlightFilters); 
		public ICommand ResetFilters => new Command(ResetFlightFilters); 
		public ICommand ShowAirlines => new Command(DisplayAirlines);
		public ICommand ApplyCheckStops => new Command<FlightStopsViewModel>(CheckStop);

		private void CheckStop(FlightStopsViewModel obj)
		{
			obj.IsSelected = !obj.IsSelected;
		}

		private void ApplyFlightFilters() 
		{
			_results.Filters = ChoosenFilters;
			if (_results.Results != null && _results.Results.Count > 0)
			{
				var filterdResults = new List<PreparedFlightResults>(_results.Results);
				if (_results.Filters.Stops.Any(x => x.IsSelected))
				{
					var stops = new List<int>();
					foreach (var stop in _results.Filters.Stops)
					{
						if (stop.IsSelected)
							stops.Add(stop.Value);
					}
					if (stops.Contains(2))
					{
						filterdResults = filterdResults.FindAll(x => x.ListSegments.Any(y => stops.Contains(y.Stops) || y.Stops > 2));
					}
					else
					{ 
						filterdResults = filterdResults.FindAll(x => x.ListSegments.Any(y => stops.Contains(y.Stops)));
					}					
				}
				if (_results.Filters.Airlines.Any(x => x.IsSelected))
				{
					var airlines = new List<string>();
					foreach (var airline in _results.Filters.Airlines)
					{
						if (airline.IsSelected)
							airlines.Add(airline.Title);
					}
					filterdResults = filterdResults.FindAll(x => x.ListSegments.Any(y => airlines.Contains(y.AirlineName)));
				}
				if (_results.Filters.OutboundDepartureFrom != 0)
				{
					filterdResults = filterdResults.FindAll(x => x.ListSegments != null && x.ListSegments[0] != null && x.ListSegments[0].DateFrom.Value != null && x.ListSegments[0].DateFrom.Value.Hour > _results.Filters.OutboundDepartureFrom);
				}
				if (_results.Filters.OutboundDepartureTo != 24)
				{
					filterdResults = filterdResults.FindAll(x => x.ListSegments != null && x.ListSegments[0] != null && x.ListSegments[0].DateFrom.Value != null && x.ListSegments[0].DateFrom.Value.Hour < _results.Filters.OutboundDepartureTo);
				}
				if (_results.Filters.OutboundArrivalFrom != 0)
				{
					filterdResults = filterdResults.FindAll(x => x.ListSegments != null && x.ListSegments[0] != null && x.ListSegments[0].DateFrom.Value != null && x.ListSegments[0].DateTo.Value.Hour > _results.Filters.OutboundArrivalFrom);
				}
				if (_results.Filters.OutboundArrivalTo != 24)
				{
					filterdResults = filterdResults.FindAll(x => x.ListSegments != null && x.ListSegments[0] != null && x.ListSegments[0].DateFrom.Value != null && x.ListSegments[0].DateTo.Value.Hour < _results.Filters.OutboundArrivalTo);
				}

				if (_results.Request.SearchType == Models.SearchType.RoundTrip)
				{
					if (_results.Filters.InboundDepartureFrom != 0)
					{
						filterdResults = filterdResults.FindAll(x => x.ListSegments != null && x.ListSegments[1] != null && x.ListSegments[1].DateFrom.Value != null && x.ListSegments[1].DateFrom.Value.Hour > _results.Filters.InboundDepartureFrom);
					}
					if (_results.Filters.InboundDepartureTo != 24)
					{
						filterdResults = filterdResults.FindAll(x => x.ListSegments != null && x.ListSegments[1] != null && x.ListSegments[1].DateFrom.Value != null && x.ListSegments[1].DateFrom.Value.Hour < _results.Filters.InboundDepartureTo);
					}
					if (_results.Filters.InboundArrivalFrom != 0)
					{
						filterdResults = filterdResults.FindAll(x => x.ListSegments != null && x.ListSegments[1] != null && x.ListSegments[1].DateFrom.Value != null && x.ListSegments[1].DateTo.Value.Hour > _results.Filters.InboundArrivalFrom);
					}
					if (_results.Filters.InboundArrivalTo != 24)
					{
						filterdResults = filterdResults.FindAll(x => x.ListSegments != null && x.ListSegments[1] != null && x.ListSegments[1].DateFrom.Value != null && x.ListSegments[1].DateTo.Value.Hour < _results.Filters.InboundArrivalTo);
					}
				}
				
				_results.FilterdResults = new ObservableCollection<PreparedFlightResults>(filterdResults);
				SortResults();
			}
			var navigation = Application.Current.MainPage as Shell;
			navigation.Navigation.PopAsync(true);
		}
		private void ResetFlightFilters()
		{
			_results.FilterdResults = _results.Results;
			_results.Filters = new FiltersViewModel(_results.Results);
			var navigation = Application.Current.MainPage as Shell;
			navigation.Navigation.PopAsync(true);
		}
		private void DisplayAirlines() 
		{
			_choosenFilters.ChoosenAirlines = new List<CheckboxViewModel>();
			_choosenFilters.Airlines.ForEach(x => _choosenFilters.ChoosenAirlines.Add(new CheckboxViewModel { IsSelected = x.IsSelected, Title = x.Title }));
			var page = new FilterAirlinesPage(ChoosenFilters);
			var navigation = Application.Current.MainPage as Shell;
			navigation.Navigation.PushAsync(page, true);
		}
		private void SortResults() 
		{
			var selectedSorting = _results.Filters.Sort.Where(x => x.IsSelected).FirstOrDefault();
			if (selectedSorting != null)
			{
				if (selectedSorting.Title == AppResources.FF_CHEAPEST)
				{
					_results.FilterdResults = new ObservableCollection<PreparedFlightResults>(_results.FilterdResults.OrderBy(x => x.Total));
				}
				else if (selectedSorting.Title == AppResources.FF_QUICKEST)
				{
					_results.FilterdResults = new ObservableCollection<PreparedFlightResults>(_results.FilterdResults.OrderBy(x => x.TotalDuration));
				}
				else if (selectedSorting.Title == AppResources.FF_EARLIEST)
				{
					_results.FilterdResults = new ObservableCollection<PreparedFlightResults>(_results.FilterdResults.OrderBy(x => x.FirstDateFrom));
				}
			}
		}
	}
}

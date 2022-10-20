using ResvoyageMobileApp.Models.Flight;
using ResvoyageMobileApp.Resources;
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
    public class FiltersViewModel : BaseViewModel
    {
        public FiltersViewModel()
        {

        }
        public FiltersViewModel(ObservableCollection<PreparedFlightResults> results)
        {
            _stops = GetStops();
            _sort = GetSorts();

            if (results != null)
            {
                _airlines = GetAirlines(results);
                _choosenAirlines = new List<CheckboxViewModel>();
                _airlines.ForEach(x => _choosenAirlines.Add(new CheckboxViewModel { Title = x.Title, IsSelected = x.IsSelected }));
            }
            _outboundDepartureFrom = 0;
            _outboundDepartureTo = 24;
            _outboundArrivalFrom = 0;
            _outboundArrivalTo = 24;

            _inboundDepartureFrom = 0;
            _inboundDepartureTo = 24;
            _inboundArrivalFrom = 0;
            _inboundArrivalTo = 24;
        }
        private List<FlightStopsViewModel> _stops;

        public List<FlightStopsViewModel> Stops
        {
            get { return _stops; }
            set { SetValue(ref _stops, value); }
        }
        private List<SortFlightViewModel> _sort;

        public List<SortFlightViewModel> Sort
        {
            get { return _sort; }
            set { SetValue(ref _sort, value); }
        }
        private List<CheckboxViewModel> _airlines;

        public List<CheckboxViewModel> Airlines
        {
            get { return _airlines; }
            set { SetValue(ref _airlines, value); }
        }
        private List<CheckboxViewModel> _choosenAirlines;

        public List<CheckboxViewModel> ChoosenAirlines
        {
            get { return _choosenAirlines; }
            set { SetValue(ref _choosenAirlines, value); }
        }
        private string _selectedAirlines;

        public string SelectedAirlines
        {
            get { return _selectedAirlines; }
            set { SetValue(ref _selectedAirlines, value); }
        }
        private int _outboundDepartureFrom;

        public int OutboundDepartureFrom
        {
            get { return _outboundDepartureFrom; }
            set { SetValue(ref _outboundDepartureFrom, value); }
        }
        private int _outboundDepartureTo;

        public int OutboundDepartureTo
        {
            get { return _outboundDepartureTo; }
            set { SetValue(ref _outboundDepartureTo, value); }
        }
        private int _outboundArrivalFrom;

        public int OutboundArrivalFrom
        {
            get { return _outboundArrivalFrom; }
            set { SetValue(ref _outboundArrivalFrom, value); }
        }
        private int _outboundArrivalTo;

        public int OutboundArrivalTo
        {
            get { return _outboundArrivalTo; }
            set { SetValue(ref _outboundArrivalTo, value); }
        }
        private int _inboundDepartureFrom;

        public int InboundDepartureFrom
        {
            get { return _inboundDepartureFrom; }
            set { SetValue(ref _inboundDepartureFrom, value); }
        }
        private int _inboundDepartureTo;

        public int InboundDepartureTo
        {
            get { return _inboundDepartureTo; }
            set { SetValue(ref _inboundDepartureTo, value); }
        }
        private int _inboundArrivalFrom;

        public int InboundArrivalFrom
        {
            get { return _inboundArrivalFrom; }
            set { SetValue(ref _inboundArrivalFrom, value); }
        }
        private int _inboundArrivalTo;

        public int InboundArrivalTo
        {
            get { return _inboundArrivalTo; }
            set { SetValue(ref _inboundArrivalTo, value); }
        }
        public ICommand ApplyAirlines => new Command(ApplyAirlinesFilter);
        public ICommand ResetAirlines => new Command(ResetAirlinesFilter);
        public ICommand ApplyCheckAirlines => new Command<CheckboxViewModel>(CheckAirline);

        private void CheckAirline(CheckboxViewModel obj)
        {
            obj.IsSelected = !obj.IsSelected;
        }

        private void ApplyAirlinesFilter() 
        {
            Airlines = ChoosenAirlines;
            if(Airlines != null && Airlines.Count > 0)
            {
                SelectedAirlines = string.Join(", ", Airlines.Where(x => x.IsSelected).Select(x => x.Title));
            }
            var navigation = Application.Current.MainPage as Shell;
            navigation.Navigation.PopAsync(true);
        }
        private void ResetAirlinesFilter()
        {
            if (Airlines != null && Airlines.Count > 0)
            {
                SelectedAirlines = null;
                Airlines.ForEach(x => x.IsSelected = false);
                ChoosenAirlines.ForEach(x => x.IsSelected = false);
            }
            var navigation = Application.Current.MainPage as Shell;
            navigation.Navigation.PopAsync(true);
        }
        private List<FlightStopsViewModel> GetStops()
        {
            return new List<FlightStopsViewModel> {
                new FlightStopsViewModel{ Title = AppResources.FF_NONSTOP, Value = 0},
                new FlightStopsViewModel{ Title = AppResources.FF_ONESTOP, Value = 1},
                new FlightStopsViewModel{ Title = AppResources.FF_TWOSTOPS, Value = 2}
            };
        }
        private List<SortFlightViewModel> GetSorts()
        {
            return new List<SortFlightViewModel> {
                new SortFlightViewModel{ Title = AppResources.FF_CHEAPEST, IsSelected = true},
                new SortFlightViewModel{ Title = AppResources.FF_QUICKEST},
                new SortFlightViewModel{ Title = AppResources.FF_EARLIEST}
            };
        }
        private List<CheckboxViewModel> GetAirlines(ObservableCollection<PreparedFlightResults> results)
        {
            var airlines = new List<CheckboxViewModel>();

            foreach (var filght in results)
            {
                foreach (var segment in filght.ListSegments)
                {
                    if (!airlines.Any(x => x.Title == segment.AirlineName))
                        airlines.Add(new CheckboxViewModel() { Title = segment.AirlineName });
                }
            }

            return airlines;
        }
    }
}

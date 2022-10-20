using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Views.Flight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Flight
{
    public class PopularPlacesViewModel : BaseViewModel
    {
        public PopularPlacesViewModel(ObservableCollection<Place> places, Place place, FlightRequestViewModel request, string searchType)
        {
            _places = places;
            _place = place;
            _request = new FlightRequestViewModel()
            {
                IsFromPopularPlaces = true,
                From1City = request.From1City,
                From1Iata = request.From1Iata
            };
            _searchType = searchType;
        }

        private ObservableCollection<Place> _places;

        public ObservableCollection<Place> Places
        {
            get { return _places; }
            set { SetValue(ref _places, value); }
        }
        private Place _place;

        public Place Place
        {
            get { return _place; }
            set { SetValue(ref _place, value); }
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
        public ICommand SelectPlace => new Command<Place>(SetPlace);
        public ICommand GoBack => new Command(Back);

        private void SetPlace(Place obj)
        {
            Request.To1City = obj.City;
            Request.To1Iata = obj.IATACode;

            var page = new CalendarPage(Request, _searchType);
            _navigation.Navigation.PushAsync(page, true);
        }

        private void Back()
        {
            _navigation.Navigation.PopAsync(true);
        }
    }
}

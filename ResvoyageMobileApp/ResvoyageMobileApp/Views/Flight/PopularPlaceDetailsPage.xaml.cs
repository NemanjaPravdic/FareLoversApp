using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.ViewModels.Flight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResvoyageMobileApp.Views.Flight
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopularPlaceDetailsPage : ContentPage
    {
        public PopularPlaceDetailsPage(ObservableCollection<Place> places, Place place, FlightRequestViewModel request, string searchType)
        {
            InitializeComponent();
            BindingContext = new PopularPlacesViewModel(places, place, request, searchType);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }
    }
}
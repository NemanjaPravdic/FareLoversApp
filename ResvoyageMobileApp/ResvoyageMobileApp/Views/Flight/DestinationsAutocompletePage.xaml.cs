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
    public partial class DestinationsAutocompletePage : ContentPage
    {
        public DestinationsAutocompletePage(FlightRequestViewModel request, string type, int? segment=null, List<AirportInfo> nearbyAirports = null)
        {
            InitializeComponent();
            BindingContext = new DestinationAutocompleteViewModel(request, type, segment, nearbyAirports);
            
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(600);
            Destination.Focus();
        }

    }
}
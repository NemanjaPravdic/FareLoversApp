using ResvoyageMobileApp.Models.Flight;
using ResvoyageMobileApp.ViewModels.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResvoyageMobileApp.Views.Flight
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlightDetailsPage : ContentPage
    {
        public FlightDetailsPage(PreparedFlightResults selectedFlight, FlightRequestViewModel request)
        {
            InitializeComponent();
            BindingContext = new FlightDetailsViewModel(selectedFlight, request);
        }
    }
}
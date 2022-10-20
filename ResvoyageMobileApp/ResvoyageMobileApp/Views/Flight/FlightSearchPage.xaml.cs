using ResvoyageMobileApp.Interfaces;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Services.Flight;
using ResvoyageMobileApp.ViewModels.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResvoyageMobileApp.Views.Flight
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlightSearchPage : TabbedPage
    {
        private FlightSearchViewModel _vm = new FlightSearchViewModel();
        private PlaceService _placeService;
        private PopularPlacesService _popularPlacesService;
        public FlightSearchPage()
        {
            InitializeComponent();
            _placeService = new PlaceService();
            _popularPlacesService = new PopularPlacesService();
            OneWay.BindingContext = null;
            RoundTrip.BindingContext = null;
            MultiCity.BindingContext = null;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (OneWay.BindingContext == null || RoundTrip.BindingContext == null || MultiCity.BindingContext == null)
            {
                var nearbyAirports = await _placeService.GetNearbyAirports();
                var popularPlaces = await _popularPlacesService.GetPopularPlaces();
                OneWay.BindingContext = new FlightSearchViewModel(nearbyAirports, popularPlaces) { SearchType = "OneWay" };
                RoundTrip.BindingContext = new FlightSearchViewModel(nearbyAirports, popularPlaces) { SearchType = "RoundTrip" };
                MultiCity.BindingContext = new FlightSearchViewModel(nearbyAirports, popularPlaces) { SearchType = "MultiCity" };

                
                bool ratedApp = Application.Current.Properties.ContainsKey("RatedApp");

                if (!ratedApp)
                {
                    bool answer = await DisplayAlert(AppResources.APP_RATE_APP, AppResources.APP_RATE_TEXT, AppResources.APP_OK, AppResources.APP_LATER);
                    if (answer)
                    { 
                        RateApp();
                        Application.Current.Properties["RatedApp"] = true;
                        await Application.Current.SavePropertiesAsync();
                    }
                }
            }
        }


        private void RateApp()
        {
            IAppRating appRating = DependencyService.Get<IAppRating>();

            if (appRating != null)
                appRating.RateApp();

        }
    }
}
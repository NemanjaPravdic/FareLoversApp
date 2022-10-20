using ResvoyageMobileApp.ViewModels.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResvoyageMobileApp.Views.Hotel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HotelDestinationAutocompletePage : ContentPage
    {
        public HotelDestinationAutocompletePage(HotelRequestViewModel request)
        {
            InitializeComponent();
            BindingContext = new HotelDestinationAutocompleteViewModel(request);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(600);
            Destination.Focus();
        }
    }
}
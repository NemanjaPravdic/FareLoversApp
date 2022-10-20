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
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage(FlightRequestViewModel request, string type, int? multiCitySegment = null)
        {
            InitializeComponent();
            BindingContext = new CalendarViewModel(request, type, multiCitySegment);
        }
    }
}
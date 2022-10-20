using ResvoyageMobileApp.ViewModels.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResvoyageMobileApp.Views.Car
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarCalendarPage : ContentPage
    {
        public CarCalendarPage(CarRequestViewModel request, string type)
        {
            InitializeComponent();
            BindingContext = new CarCalendarViewModel(request, type);
        }
    }
}
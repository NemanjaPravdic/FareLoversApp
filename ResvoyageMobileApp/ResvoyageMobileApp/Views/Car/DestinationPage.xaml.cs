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
    public partial class DestinationPage : ContentPage
    {
        public DestinationPage(CarRequestViewModel request, string type)
        {
            InitializeComponent();
            BindingContext = new DestinationViewModel(request, type);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(600);
            Destination.Focus();
        }
    }
}
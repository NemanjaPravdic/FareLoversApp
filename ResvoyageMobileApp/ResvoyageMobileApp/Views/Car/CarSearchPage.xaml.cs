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
    public partial class CarSearchPage : ContentPage
    {
        public CarSearchPage()
        {
            InitializeComponent();
            BindingContext = new CarSearchViewModel();
        }
    }
}
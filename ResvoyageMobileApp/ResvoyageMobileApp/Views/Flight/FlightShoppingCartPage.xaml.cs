using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.ViewModels.ShoppingCart;
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
    public partial class FlightShoppingCartPage : ContentPage
    {
        public FlightShoppingCartPage(ShoppingCartModel shoppingCart)
        {
            InitializeComponent();
            BindingContext = new ShoppingCartViewModel(shoppingCart,"AIR");
        }
    }
}
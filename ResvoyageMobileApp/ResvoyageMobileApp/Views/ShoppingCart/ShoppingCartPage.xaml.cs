using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.ViewModels.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResvoyageMobileApp.Views.ShoppingCart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingCartPage : ContentPage
    {
        public ShoppingCartPage(ShoppingCartModel shoppingCart, string type)
        {
            InitializeComponent();
            BindingContext = new ShoppingCartViewModel(shoppingCart, type);
        }
    }
}
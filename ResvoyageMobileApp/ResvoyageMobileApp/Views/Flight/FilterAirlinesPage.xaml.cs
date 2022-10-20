using ResvoyageMobileApp.ViewModels.Flight;
using Rg.Plugins.Popup.Pages;
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
    public partial class FilterAirlinesPage : ContentPage
    {
        public FilterAirlinesPage(FiltersViewModel filters)
        {
            InitializeComponent();
            BindingContext = filters;
        }
    }
}
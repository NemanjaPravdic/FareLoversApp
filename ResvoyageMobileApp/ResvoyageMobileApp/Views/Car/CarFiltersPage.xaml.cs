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
    public partial class CarFiltersPage : ContentPage
    {
        public CarFiltersPage(CarResultViewModel result)
        {
            InitializeComponent();
            BindingContext = new CarFilterViewModel(result);

            RangeSliderWithEffect.Effects.Add(Effect.Resolve("EffectsSlider.RangeSliderEffect"));
        }
    }
}
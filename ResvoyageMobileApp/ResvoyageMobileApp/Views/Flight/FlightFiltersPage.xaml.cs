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
    public partial class FlightFiltersPage : ContentPage
    {
        public FlightFiltersPage(FlightResultViewModel results)
        {
            InitializeComponent();
            BindingContext = new FlightFiltersViewModel(results);
            
            RangeSliderWithEffect1.Effects.Add(Effect.Resolve("EffectsSlider.RangeSliderEffect"));
            RangeSliderWithEffect2.Effects.Add(Effect.Resolve("EffectsSlider.RangeSliderEffect"));
            RangeSliderWithEffect3.Effects.Add(Effect.Resolve("EffectsSlider.RangeSliderEffect"));
            RangeSliderWithEffect4.Effects.Add(Effect.Resolve("EffectsSlider.RangeSliderEffect"));
            
        }
    }
}
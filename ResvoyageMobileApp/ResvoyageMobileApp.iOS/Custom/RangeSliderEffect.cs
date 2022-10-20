using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using ResvoyageMobileApp.iOS.Custom;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("EffectsSlider")]
[assembly: ExportEffect(typeof(RangeSliderEffect), "RangeSliderEffect")]
namespace ResvoyageMobileApp.iOS.Custom
{
    public class RangeSliderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var ctrl = (Xamarin.RangeSlider.RangeSliderControl)Control;
            ctrl.TintColor = Xamarin.Forms.Color.FromHex("BEA7FF").ToUIColor();
        }

        protected override void OnDetached()
        {
        }
    }
}
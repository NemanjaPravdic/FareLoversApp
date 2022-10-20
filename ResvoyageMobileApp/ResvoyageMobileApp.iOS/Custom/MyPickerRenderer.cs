using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using ResvoyageMobileApp.iOS.Custom;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Picker), typeof(MyPickerRenderer))]
namespace ResvoyageMobileApp.iOS.Custom
{
    public class MyPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
           base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
                Element.HeightRequest = 40;
            }
        }
    }
}
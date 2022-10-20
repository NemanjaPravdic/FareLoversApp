using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using ResvoyageMobileApp.iOS.Custom;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(MyEntryRenderer))]
namespace ResvoyageMobileApp.iOS.Custom
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                // do whatever you want to the UITextField here!
                Control.BorderStyle = UITextBorderStyle.None;
                Control.TextColor = Color.Black.ToUIColor();
                Element.HeightRequest = 40;
            }
        }
    }
}
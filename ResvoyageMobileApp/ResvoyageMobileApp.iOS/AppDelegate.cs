using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using ResvoyageMobileApp.Models.User;
using Syncfusion.SfCalendar.XForms.iOS;
using Syncfusion.SfRangeSlider.XForms.iOS;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.RangeSlider.Forms;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Xamarin.RangeSlider.Forms.RangeSlider), typeof(Xamarin.RangeSlider.Forms.RangeSliderRenderer))]
namespace ResvoyageMobileApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            SfCalendarRenderer.Init();
            Xamarin.FormsMaps.Init();
            global::Xamarin.Forms.Forms.Init();
            global::Xamarin.Auth.Presenters.XamarinIOS.AuthenticationConfiguration.Init();
            var t1 = typeof(Xamarin.RangeSlider.RangeSliderControl);
            var t2 = typeof(RangeSliderRenderer);
            LoadApplication(new App());
            Syncfusion.SfRating.XForms.iOS.SfRatingRenderer.Init();
            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            // Convert NSUrl to Uri
            var uri = new Uri(url.AbsoluteString);

            // Load redirectUrl page
            AuthenticationState.Authenticator.OnPageLoading(uri);

            return true;
        }
        public override void OnActivated(UIApplication uiApplication)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                // If VS has updated to the latest version , you can use StatusBarManager , else use the first line code
                // UIView statusBar = new UIView(UIApplication.SharedApplication.StatusBarFrame);
                UIView statusBar = new UIView(UIApplication.SharedApplication.KeyWindow.WindowScene.StatusBarManager.StatusBarFrame);
                statusBar.BackgroundColor = Xamarin.Forms.Color.FromHex("BEA7FF").ToUIColor();
                statusBar.TintColor = UIColor.White;
                UIApplication.SharedApplication.KeyWindow.AddSubview(statusBar);
            }
            else
            {
                UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
                if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
                {
                    statusBar.BackgroundColor = Xamarin.Forms.Color.FromHex("BEA7FF").ToUIColor();
                    statusBar.TintColor = UIColor.White;
                    UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.BlackOpaque;
                }
            }
            base.OnActivated(uiApplication);
        }
    }
}

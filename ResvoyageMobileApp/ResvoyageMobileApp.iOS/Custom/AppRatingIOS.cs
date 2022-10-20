using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using ResvoyageMobileApp.Interfaces;
using ResvoyageMobileApp.iOS.Custom;
using StoreKit;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppRatingIOS))]
namespace ResvoyageMobileApp.iOS.Custom
{
    public class AppRatingIOS : IAppRating
    {
        public void RateApp()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 3))
                SKStoreReviewController.RequestReview();
            else
            {
                var storeUrl = "itms-apps://itunes.apple.com/app/YourAppId";
                var url = storeUrl + "?action=write-review";

                try
                {
                    UIApplication.SharedApplication.OpenUrl(new NSUrl(url));
                }
                catch (Exception ex)
                {
                    // Here you could show an alert to the user telling that App Store was unable to launch

                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
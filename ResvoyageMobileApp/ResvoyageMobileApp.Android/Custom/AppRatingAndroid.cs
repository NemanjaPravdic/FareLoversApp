using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ResvoyageMobileApp.Droid.Custom;
using ResvoyageMobileApp.Interfaces;
using Xamarin.Forms;

[assembly:Dependency(typeof (AppRatingAndroid))]
namespace ResvoyageMobileApp.Droid.Custom
{
    public class AppRatingAndroid : IAppRating
    {
        public void RateApp()
        {
            var activity = Android.App.Application.Context;

            var playStoreUrl = "https://play.google.com/store/apps/details?id=com.Farelovers.FareloversMobileApp"; //Add here the url of your application on the store

            var browserIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(playStoreUrl));
            browserIntent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ResetTaskIfNeeded);

            activity.StartActivity(browserIntent);
        }
    }
}
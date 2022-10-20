using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ResvoyageMobileApp.Droid.Custom;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.RangeSlider;

[assembly: ResolutionGroupName("EffectsSlider")]
[assembly: ExportEffect(typeof(RangeSliderEffect), "RangeSliderEffect")]
namespace ResvoyageMobileApp.Droid.Custom
{
    public class RangeSliderEffect : Xamarin.Forms.Platform.Android.PlatformEffect
    {
        protected override void OnAttached()
        {
            var ctrl = (RangeSliderControl)Control;

            Context context = Xamarin.Forms.Forms.Context;
            Bitmap icon = GetResizedBitmap(BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.dot), 80, 80);
            ctrl.SetCustomThumbImage(icon);
            ctrl.SetCustomThumbPressedImage(icon);
        }
        public Bitmap GetResizedBitmap(Bitmap bm, int newWidth, int newHeight)
        {
            int width = bm.Width;
            int height = bm.Height;
            float scaleWidth = ((float)newWidth) / width;
            float scaleHeight = ((float)newHeight) / height;
            // CREATE A MATRIX FOR THE MANIPULATION
            Matrix matrix = new Matrix();
            // RESIZE THE BIT MAP
            matrix.PostScale(scaleWidth, scaleHeight);

            // "RECREATE" THE NEW BITMAP
            Bitmap resizedBitmap = Bitmap.CreateBitmap(
                bm, 0, 0, width, height, matrix, false);
            bm.Recycle();
            return resizedBitmap;
        }
        protected override void OnDetached()
        {
            
        }
    }
}
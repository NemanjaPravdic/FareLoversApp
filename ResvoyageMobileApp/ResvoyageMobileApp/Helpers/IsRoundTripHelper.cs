using ResvoyageMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResvoyageMobileApp.Helpers
{
    public class IsRoundTripHelper : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SearchType searchType;
            if (Enum.TryParse(value.ToString(), true, out searchType))
                if (searchType == SearchType.RoundTrip)
                    return true;
                else
                    return false;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
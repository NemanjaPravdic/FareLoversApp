using ResvoyageMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ResvoyageMobileApp.Helpers
{
    public class MapItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate XamarinTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((Location)item).Address.Contains("San Francisco") ? XamarinTemplate : DefaultTemplate;
        }
    }
}

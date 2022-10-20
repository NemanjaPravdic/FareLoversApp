using Newtonsoft.Json;
using ResvoyageMobileApp.Models.Other;
using ResvoyageMobileApp.Models.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected Shell _navigation = Application.Current.MainPage as Shell;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName]string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return;

            backingField = value;

            OnPropertyChanged(propertyName);
        }
        protected CurrencyInfo SelectedCurrency
        {
            get
            {
                if (Application.Current.Properties.ContainsKey("Currency") && Application.Current.Properties["Currency"].ToString() != "null")
                {
                    var currencyJson = Application.Current.Properties["Currency"].ToString();
                    var currency = JsonConvert.DeserializeObject<CurrencyInfo>(currencyJson);
                    return currency;
                }
                else
                {
                    if (Application.Current.Properties.ContainsKey("UserLocation"))
                    {
                        var placeInfo = JsonConvert.DeserializeObject<UserLocation>(Application.Current.Properties["UserLocation"]?.ToString());
                        if (placeInfo.CurrencyCode != null && placeInfo.CurrencyName != null)
                            return new CurrencyInfo() { CurrencyCode = placeInfo.CurrencyCode , Text = placeInfo.CurrencyName};
                        else
                            return new CurrencyInfo { CurrencyCode = "CAD", Text = "Canadian dollar" };
                    }
                    else
                        return new CurrencyInfo { CurrencyCode = "CAD", Text = "Canadian dollar" };
                }
            }
        }

        public ObservableCollection<CurrencyInfo> Currencies
        {
            get
            {
                var list = new ObservableCollection<CurrencyInfo>
                {
                    new CurrencyInfo { CurrencyCode = "EUR", Text = "Euro"},
                    new CurrencyInfo { CurrencyCode = "USD", Text = "United States dollar"},
                    new CurrencyInfo { CurrencyCode = "CAD", Text = "Canadian dollar"},
                    new CurrencyInfo { CurrencyCode = "JPY", Text = "Japanese yen"},
                    new CurrencyInfo { CurrencyCode = "GBP", Text = "British pound"},
                    new CurrencyInfo { CurrencyCode = "CHF", Text = "Swiss franc"},
                    new CurrencyInfo { CurrencyCode = "RUB", Text = "Russian ruble"},
                    new CurrencyInfo { CurrencyCode = "UZS", Text = "Uzbekistan som"},
                    new CurrencyInfo { CurrencyCode = "NGN", Text = "Nigerian naira"}

                };
                if (Application.Current.Properties.ContainsKey("UserLocation"))
                {
                    var placeInfo = JsonConvert.DeserializeObject<UserLocation>(Application.Current.Properties["UserLocation"]?.ToString());
                    if (placeInfo.CurrencyCode != null && placeInfo.CurrencyName != null)
                    {
                        var currencyByIp = new CurrencyInfo() { CurrencyCode = placeInfo.CurrencyCode, Text = placeInfo.CurrencyName };

                        if(!list.Any(x => x.CurrencyCode == currencyByIp.CurrencyCode))
                            list.Add(currencyByIp);
                    }
                }

                return list;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.ViewModels.ShoppingCart
{
    public class AddressViewModel : BaseViewModel
    {
		private string _countryCode;

		public string CountryCode
		{
			get { return _countryCode; }
			set { SetValue(ref _countryCode, value); }
		}
		private string _countryName;

		public string CountryName
		{
			get { return _countryName; }
			set { SetValue(ref _countryName, value); }
		}
		private string _state;

		public string State
		{
			get { return _state; }
			set { SetValue(ref _state, value); }
		}
		private string _regionName;

		public string RegionName
		{
			get { return _regionName; }
			set { SetValue(ref _regionName, value); }
		}
		private string _zip;

		public string ZIP
		{
			get { return _zip; }
			set { SetValue(ref _zip, value); }
		}
		private string _cityName;

		public string CityName
		{
			get { return _cityName; }
			set { SetValue(ref _cityName, value); }
		}
		private string _streetAddress;

		public string StreetAddress
		{
			get { return _streetAddress; }
			set { SetValue(ref _streetAddress, value); }
		}
	}
}

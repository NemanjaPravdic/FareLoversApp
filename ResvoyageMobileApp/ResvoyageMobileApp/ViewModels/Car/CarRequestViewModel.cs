using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.ViewModels.Car
{
    public class CarRequestViewModel : BaseViewModel
    {
		public CarRequestViewModel()
		{
			_dropoffTime = "12:00";
			_pickupTime = "12:00";
		}
		private string _pickupCity;

		public string PickupCity
		{
			get { return _pickupCity; }
			set { SetValue(ref _pickupCity, value); }
		}
		private string _dropOffCity;

		public string DropOffCity
		{
			get { return _dropOffCity; }
			set { SetValue(ref _dropOffCity, value); }
		}
		private string _pickupCityIata;

		public string PickupCityIata
		{
			get { return _pickupCityIata; }
			set { SetValue(ref _pickupCityIata, value); }
		}
		private string _dropOffCityIata;

		public string DropOffCityIata
		{
			get { return _dropOffCityIata; }
			set { SetValue(ref _dropOffCityIata, value); }
		}
		private string _pickupTime;

		public string PickupTime
		{
			get { return _pickupTime; }
			set { SetValue(ref _pickupTime, value); }
		}
		private string _dropoffTime;

		public string DropoffTime
		{
			get { return _dropoffTime; }
			set { SetValue(ref _dropoffTime, value); }
		}
		private string _pickupDate;

		public string PickupDate
		{
			get { return _pickupDate; }
			set { SetValue(ref _pickupDate, value); }
		}
		private string _pickupDateString;

		public string PickupDateString
		{
			get { return _pickupDateString; }
			set { SetValue(ref _pickupDateString, value); }
		}
		private string _pickupDateDayString;

		public string PickupDateDayString
		{
			get { return _pickupDateDayString; }
			set { SetValue(ref _pickupDateDayString, value); }
		}
		private string _dropOffDate;

		public string DropOffDate
		{
			get { return _dropOffDate; }
			set { SetValue(ref _dropOffDate, value); }
		}
		private string _dropOffDateString;

		public string DropOffDateString
		{
			get { return _dropOffDateString; }
			set { SetValue(ref _dropOffDateString, value); }
		}
		private string _dropOffDateDayString;

		public string DropOffDateDayString
		{
			get { return _dropOffDateDayString; }
			set { SetValue(ref _dropOffDateDayString, value); }
		}
		public string RequestText
		{
			get
			{
				return string.Format("{0}, {1} - {2}, {3}", PickupCity, PickupDateString, DropOffCity, DropOffDateString);
			}
		}
	}
}

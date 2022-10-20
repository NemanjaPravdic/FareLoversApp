using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.ViewModels.Flight
{
    public class FlightStopsViewModel : BaseViewModel
    {
		private bool _isSelected;

		public bool IsSelected
		{
			get { return _isSelected; }
			set { SetValue(ref _isSelected, value); }
		}
		private string _title;

		public string Title
		{
			get { return _title; }
			set { SetValue(ref _title, value); }
		}
		private int _value;

		public int Value
		{
			get { return _value; }
			set { SetValue(ref _value, value); }
		}
	}
}

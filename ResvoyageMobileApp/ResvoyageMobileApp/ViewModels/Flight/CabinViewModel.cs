using ResvoyageMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.ViewModels.Flight
{
    public class CabinViewModel : BaseViewModel
    {
		private string _name;

		public string Name
		{
			get { return _name; }
			set { SetValue(ref _name, value); }
		}
		private string _type;

		public string Type
		{
			get { return _type; }
			set { SetValue(ref _type, value); }
		}

		private bool _isSelected;

		public bool IsSelected
		{
			get { return _isSelected; }
			set { SetValue(ref _isSelected, value); }
		}

	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.ShoppingCart
{
    public class PassengerGendreViewModel : BaseViewModel
    {
		private string _name;

		public string Name
		{
			get { return _name; }
			set { SetValue(ref _name, value); }
		}

		private bool _isSelected;

		public bool IsSelected
		{
			get { return _isSelected; }
			set { SetValue(ref _isSelected, value); }
		}

	}
}

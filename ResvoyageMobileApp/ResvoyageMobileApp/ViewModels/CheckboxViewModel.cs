using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.ViewModels
{
    public class CheckboxViewModel :BaseViewModel
    {
		private string _title;

		public string Title
		{
			get { return _title; }
			set { SetValue(ref _title, value); }
		}

		private bool _isSelected;

		public bool IsSelected
		{
			get { return _isSelected; }
			set { SetValue(ref _isSelected, value); }
		}
	}
}

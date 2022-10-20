using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.ViewModels.Hotel
{
    public class ChildAgeViewModel : BaseViewModel
    {
		private int _id;

		public int Id
		{
			get { return _id; }
			set { SetValue(ref _id, value); }
		}
		private int _age;

		public int Age
		{
			get { return _age; }
			set { SetValue(ref _age, value); }
		}
	}
}

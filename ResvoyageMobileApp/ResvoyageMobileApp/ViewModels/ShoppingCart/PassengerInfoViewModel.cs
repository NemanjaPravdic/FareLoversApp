using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.ShoppingCart
{
    public class PassengerInfoViewModel : BaseViewModel
    {
		private int _id;

		public int Id
		{
			get { return _id; }
			set { SetValue(ref _id, value); }
		}
		private int _adultId;

		public int AdultId
		{
			get { return _adultId; }
			set { SetValue(ref _adultId, value); }
		}
		private int _childId;

		public int ChildId
		{
			get { return _childId; }
			set { SetValue(ref _childId, value); }
		}
		private int _infantId;

		public int InfantId
		{
			get { return _infantId; }
			set { SetValue(ref _infantId, value); }
		}
		private string _title;

		public string Title
		{
			get { return _title; }
			set { SetValue(ref _title, value); }
		}
		private string _firstName;

		public string FirstName
		{
			get { return _firstName; }
			set { SetValue(ref _firstName, value); }
		}
		private string _middleName;

		public string MiddleName
		{
			get { return _middleName; }
			set { SetValue(ref _middleName, value); }
		}
		private string _lastName;

		public string LastName
		{
			get { return _lastName; }
			set { SetValue(ref _lastName, value); }
		}
		private string _email;

		public string Email
		{
			get { return _email; }
			set { SetValue(ref _email, value); }
		}
		private string _phone;

		public string Phone
		{
			get { return _phone; }
			set { SetValue(ref _phone, value); }
		}
		private string _dateOfBirthString;

		public string DateOfBirthString
		{
			get { return _dateOfBirthString; }
			set { SetValue(ref _dateOfBirthString, value); }
		}
		private string _day;

		public string Day
		{
			get { return _day; }
			set { SetValue(ref _day, value); }
		}
		private string _month;

		public string Month
		{
			get { return _month; }
			set { SetValue(ref _month, value); }
		}
		private string _year;

		public string Year
		{
			get { return _year; }
			set { SetValue(ref _year, value); }
		}
		private string _gender;

		public string Gender
		{
			get { return _gender; }
			set { SetValue(ref _gender, value); }
		}
		private bool _isChild;

		public bool IsChild
		{
			get { return _isChild; }
			set { SetValue(ref _isChild, value); }
		}
		private string _typeCode;

		public string TypeCode
		{
			get { return _typeCode; }
			set { SetValue(ref _typeCode, value); }
		}
		public List<string> Years
		{
			get
			{
				var list = new List<string>();
				var year = DateTime.Now.Year;
				var minYear = IsChild ? year - 17 : 1910;
				for (int i = year; i >= minYear; i--)
				{
					list.Add(i.ToString());
				}
				return list;
			}
		}
		public List<string> Months
		{
			get
			{
				var list = new List<string>();
				for (int i = 1; i <= 12; i++)
				{
					list.Add(i.ToString());
				}
				return list;
			}
		}
		public List<string> Days
		{
			get
			{
				var list = new List<string>();
				for (int i = 1; i <= 31; i++)
				{
					list.Add(i.ToString());
				}
				return list;
			}
		}
		public List<string> PassengerTitle
		{
			get
			{
				return new List<string> { "Mr", "Ms", "Mrs", "Miss", "Mx" };
			}
		}
		public string TravelerInfo
		{
			get {
				if (TypeCode == "ADT")
					return string.Format("{0} {1}", Resources.AppResources.SF_ADULT, AdultId);
				else if (TypeCode == "CHD")
					return string.Format("{0} {1}", Resources.AppResources.SF_CHILD, ChildId);
				else if (TypeCode == "INF")
					return string.Format("{0} {1}", Resources.AppResources.SF_INFANT, InfantId);
				else
					return null;
			}
		}

		public bool IsMale
		{
			get
			{
				return Gender == "Male";
			}
		}
		public bool IsFemale
		{
			get
			{
				return Gender == "Female";
			}
		}
		public ICommand ChangedGender => new Command<string>(changedGender);

		public int? EmployeeId { get; set; }

		private void changedGender(string selectedGender)
		{
			Gender = selectedGender;
		}
	}
}

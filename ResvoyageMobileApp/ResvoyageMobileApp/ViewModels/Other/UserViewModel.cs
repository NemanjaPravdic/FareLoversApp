using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Other
{
    public class UserViewModel : BaseViewModel
    {
		private int _id;

		public int Id
		{
			get { return _id; }
			set { SetValue(ref _id, value); }
		}
		private string _username;

		public string Username
		{
			get { return _username; }
			set { SetValue(ref _username, value); }
		}
		private string _password;

		public string Password
		{
			get { return _password; }
			set { SetValue(ref _password, value); }
		}
		private string _repeatPassword;

		public string RepeatPassword
		{
			get { return _repeatPassword; }
			set { SetValue(ref _repeatPassword, value); }
		}
		private string _oldPassword;

		public string OldPassword
		{
			get { return _oldPassword; }
			set { SetValue(ref _oldPassword, value); }
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
		private bool _isMale;

		public bool IsMale
		{
			get { return _isMale; }
			set { SetValue(ref _isMale, value); }
		}
		private bool _isFemale;

		public bool IsFemale
		{
			get { return _isFemale; }
			set { SetValue(ref _isFemale, value); }
		}
		public List<string> Years
		{
			get
			{
				var list = new List<string>();
				var year = DateTime.Now.Year;
				var minYear = 1910;
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

		public string DateOfBirth
		{
			get
			{
				if (!string.IsNullOrEmpty(Day) && !string.IsNullOrEmpty(Month) && !string.IsNullOrEmpty(Year))
					return string.Format("{0}-{1}-{2}", Year, Month, Day);
				else
					return null;
			}
		}
		public ICommand ChangeGenderCommand => new Command<string>(ChangeGender);

		private void ChangeGender(string gender)
		{
			Gender = gender;
			IsFemale = gender == "Female";
			IsMale = gender == "Male";
		}
	}
}

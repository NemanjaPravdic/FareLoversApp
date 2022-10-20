using ResvoyageMobileApp.Views.Flight;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using SelectionChangedEventArgs = Syncfusion.SfCalendar.XForms.SelectionChangedEventArgs;

namespace ResvoyageMobileApp.ViewModels.Flight
{
    public class CalendarViewModel : BaseViewModel
    {
		public CalendarViewModel(FlightRequestViewModel flightRequestView, string type, int? multiCitySegment = null)
		{
			_request = flightRequestView;
			_searchType = type;

			var startDate = _request.DepartureDate1;
			var endDate = _request.DepartureDate2;
			DateTime dateStart;
			DateTime dateEnd;

			if (type == "RoundTrip")
			{
				if (DateTime.TryParse(startDate, out dateStart))
				{
					if(_selectedRange == null)
						_selectedRange = new SelectionRange();

					_selectedRange.StartDate = dateStart;
				}

				if (DateTime.TryParse(endDate, out dateEnd))
				{
					if (_selectedRange == null)
						_selectedRange = new SelectionRange();

					_selectedRange.EndDate = dateEnd;
				}
				else if(_selectedRange != null)
				{
					_selectedRange.EndDate = _selectedRange.StartDate;
				}

			}
			if (type == "OneWay")
			{
				if (DateTime.TryParse(startDate, out dateStart))
				{
					if (_selectedRange == null)
						_selectedRange = new SelectionRange();

					_selectedRange.StartDate = dateStart;
					_selectedRange.EndDate = dateStart;
				}
			}

			if (type == "MultiCity")
			{
				if (multiCitySegment != null)
				{
					_isMultiCity = true;
					_selectedSegment = multiCitySegment;
					switch (multiCitySegment)
					{
						case 1:
							startDate = _request.DepartureDate1;
							_isFirstSegment = true;
							break;
						case 2:
							startDate = _request.DepartureDate2;
							_isSecondSegment = true;
							SetMinimumDate(_request.DepartureDate1);
							break;
						case 3:
							startDate = _request.DepartureDate3;
							_isThirdSegment = true;
							SetMinimumDate(_request.DepartureDate2);
							break;

					}
				}
				if (DateTime.TryParse(startDate, out dateStart))
				{
					if (_selectedRange == null)
						_selectedRange = new SelectionRange();

					_selectedRange.StartDate = dateStart;
					_selectedRange.EndDate = dateStart;
				}
			}

		}
		private FlightRequestViewModel _request;

		public FlightRequestViewModel Request
		{
			get { return _request; }
			set { SetValue(ref _request, value); }
		}
		private string _searchType;

		public string SearchType
		{
			get { return _searchType; }
			set { SetValue(ref _searchType, value); }
		}
		private int? _selectedSegment;

		public int? SelectedSegment
		{
			get { return _selectedSegment; }
			set { SetValue(ref _selectedSegment, value); }
		}
		private bool _isMultiCity;

		public bool IsMultiCity
		{
			get { return _isMultiCity; }
			set { SetValue(ref _isMultiCity, value); }
		}
		private bool _isFirstSegment;

		public bool IsFirstSegment
		{
			get { return _isFirstSegment; }
			set { SetValue(ref _isFirstSegment, value); }
		}
		private bool _isSecondSegment;

		public bool IsSecondSegment
		{
			get { return _isSecondSegment; }
			set { SetValue(ref _isSecondSegment, value); }
		}
		private bool _isThirdSegment;

		public bool IsThirdSegment
		{
			get { return _isThirdSegment; }
			set { SetValue(ref _isThirdSegment, value); }
		}
		private DateTime _minimumDate;

		public DateTime MinimumDate
		{
			get { return _minimumDate; }
			set { SetValue(ref _minimumDate, value); }
		}

		private SelectionRange _selectedRange;

		public SelectionRange SelectedRange
		{
			get { return _selectedRange; }
			set { SetValue(ref _selectedRange, value); }
		}
		[Obsolete]
		public ICommand CalendarCellTapped => new Command<SelectionChangedEventArgs>(SetDateValues);
		[Obsolete]
		private async void SetDateValues(SelectionChangedEventArgs value) 
		{
			if (_searchType == "RoundTrip")
			{
				if (SelectedRange.EndDate != SelectedRange.StartDate)
				{
					_request.DepartureDate1 = SelectedRange.StartDate.ToString("yyyy-MM-dd");
					_request.DepartureDate1String = SelectedRange.StartDate.ToString("dd MMM");
					_request.DepartureDate1DayString = SelectedRange.StartDate.ToString("ddd");
					_request.DepartureDate2 = SelectedRange.EndDate.ToString("yyyy-MM-dd");
					_request.DepartureDate2String = SelectedRange.EndDate.ToString("dd MMM");
					_request.DepartureDate2DayString = SelectedRange.EndDate.ToString("ddd");

					if (!_request.IsFromPopularPlaces)
					{
						await _navigation.Navigation.PopAsync(true);
					}
					else
					{
						var page = new OtherInfoFlightPage(_request);
						await _navigation.Navigation.PushAsync(page, true);
					}
				}
				else { 
					_request.DepartureDate1 = SelectedRange.StartDate.ToString("yyyy-MM-dd");
					_request.DepartureDate1String = SelectedRange.StartDate.ToString("dd MMM");
					_request.DepartureDate1DayString = SelectedRange.StartDate.ToString("ddd");
					_request.DepartureDate2 = null;
					_request.DepartureDate2String = null;
					_request.DepartureDate2DayString = null;
				}

			}
			else if (_searchType == "OneWay")
			{

				_request.DepartureDate1 = value.DateAdded[0].ToString("yyyy-MM-dd");
				_request.DepartureDate1String = value.DateAdded[0].ToString("dd MMM");
				_request.DepartureDate1DayString = value.DateAdded[0].ToString("ddd");
				if (!_request.IsFromPopularPlaces)
				{
					await _navigation.Navigation.PopAsync(true);
				}
				else
				{
					var page = new OtherInfoFlightPage(_request);
					await _navigation.Navigation.PushAsync(page, true);
				}
				
			}
			else if (_searchType == "MultiCity")
			{
				if(_selectedSegment != null)
				{
					if (SelectedRange.EndDate != new DateTime())
						SelectedRange.StartDate = SelectedRange.EndDate;

					switch (_selectedSegment)
					{
						case 1:
							_request.DepartureDate1 = value.DateAdded[0].ToString("yyyy-MM-dd");
							_request.DepartureDate1String = value.DateAdded[0].ToString("dd MMM");
							_request.DepartureDate1DayString = value.DateAdded[0].ToString("ddd");
							break;
						case 2:
							_request.DepartureDate2 = value.DateAdded[0].ToString("yyyy-MM-dd");
							_request.DepartureDate2String = value.DateAdded[0].ToString("dd MMM");
							_request.DepartureDate2DayString = value.DateAdded[0].ToString("ddd");
							break;
						case 3:
							_request.DepartureDate3 = value.DateAdded[0].ToString("yyyy-MM-dd");
							_request.DepartureDate3String = value.DateAdded[0].ToString("dd MMM");
							_request.DepartureDate3DayString = value.DateAdded[0].ToString("ddd");
							break;
					}
				}
				
				await _navigation.Navigation.PopAsync(true);
			}
		}
		private void SetMinimumDate(string date)
		{
			DateTime minDate = DateTime.Now;
			if (DateTime.TryParse(date, out minDate))
			{
				_minimumDate = minDate;
			}
		}

	}
}

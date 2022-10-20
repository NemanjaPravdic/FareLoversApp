using ResvoyageMobileApp.Resources;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using SelectionChangedEventArgs = Syncfusion.SfCalendar.XForms.SelectionChangedEventArgs;

namespace ResvoyageMobileApp.ViewModels.Car
{
    public class CarCalendarViewModel : BaseViewModel
    {
        public CarCalendarViewModel(CarRequestViewModel request, string type)
        {
            _request = request;
            _type = type;

            var pickup = _request.PickupDate;
            _selectedDate = pickup;
            DateTime pickupDate;
            if (type == "PickUp")
            {                
                if (DateTime.TryParse(pickup, out pickupDate))
                {
                    if (_selectedRange == null)
                        _selectedRange = new SelectionRange();

                    _selectedRange.StartDate = pickupDate;
                    _selectedRange.EndDate = pickupDate;
                }
            }
            if (type == "DropOff")
            {
                var dropoff = _request.DropOffDate;
                _selectedDate = dropoff;
                DateTime dropoffDate;
                if (DateTime.TryParse(dropoff, out dropoffDate))
                {
                    if (_selectedRange == null)
                        _selectedRange = new SelectionRange();

                    _selectedRange.StartDate = dropoffDate;
                    _selectedRange.EndDate = dropoffDate;
                }
                if (DateTime.TryParse(pickup, out pickupDate))
                {
                    _minimumDate = pickupDate;
                }
            }
        }
        private DateTime _minimumDate;

        public DateTime MinimumDate
        {
            get { return _minimumDate; }
            set { SetValue(ref _minimumDate, value); }
        }
        private string _selectedDate;

        public string SelectedDate
        {
            get { return _selectedDate; }
            set { SetValue(ref _selectedDate, value); }
        }
        private string _type;

        public string Type
        {
            get { return _type; }
            set { SetValue(ref _type, value); }
        }
        public string Text
        {
            get
            {
                return _type == "PickUp" ? AppResources.CS_PICK_UP_DATE : AppResources.CS_DROP_OFF_DATE;
            }
        }
        private CarRequestViewModel _request;

        public CarRequestViewModel Request
        {
            get { return _request; }
            set { SetValue(ref _request, value); }
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

        private async void SetDateValues(SelectionChangedEventArgs obj)
        {
            if (_type == "PickUp")
            {
                _request.PickupDate = obj.DateAdded[0].ToString("yyyy-MM-dd");
                _request.PickupDateString = obj.DateAdded[0].ToString("dd MMM");
                _request.PickupDateDayString = obj.DateAdded[0].ToString("ddd");
            }
            else if (_type == "DropOff")
            {
                
                _request.DropOffDate = obj.DateAdded[0].ToString("yyyy-MM-dd");
                _request.DropOffDateString = obj.DateAdded[0].ToString("dd MMM");
                _request.DropOffDateDayString = obj.DateAdded[0].ToString("ddd");
            }
            await _navigation.Navigation.PopAsync(true);

        }
    }
}

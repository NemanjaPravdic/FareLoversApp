using ResvoyageMobileApp.Models.Booking;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Views.Bookings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Bookings
{
    public class BookingsViewModel : BaseViewModel
    {
        public BookingsViewModel(List<BookingInfo> bookings)
        {
            _bookings = new ObservableCollection<BookingInfo>(bookings.OrderByDescending(x => x.DateRequested).ToList());
            _filterdBookings = new ObservableCollection<BookingInfo>(_bookings.Where(x => x.StartDate > DateTime.Now).ToList());
            _sort = GetSortOptions();
        }

        private ObservableCollection<BookingInfo> _bookings;

        public ObservableCollection<BookingInfo> Bookings
        {
            get { return _bookings; }
            set { SetValue(ref _bookings, value); }
        }
        private ObservableCollection<BookingInfo> _filterdBookings;

        public ObservableCollection<BookingInfo> FilterdBookings
        {
            get { return _filterdBookings; }
            set { SetValue(ref _filterdBookings, value); }
        }
        private List<CheckboxViewModel> _sort;

        public List<CheckboxViewModel> Sort
        {
            get { return _sort; }
            set { SetValue(ref _sort, value); }
        }
        public ICommand ShowBookingDetails => new Command<BookingInfo>(ShowBooking);
        public ICommand ChangeSorting => new Command<CheckboxViewModel>(ChangeBookingSorting);

        private void ShowBooking(BookingInfo obj)
        {
            _navigation.Navigation.PushAsync(new BookingDetailsPage(obj));
        }
        private void ChangeBookingSorting(CheckboxViewModel obj)
        {
            Sort.ForEach(x => x.IsSelected = false);
            obj.IsSelected = true;

            if (obj.Title == AppResources.MB_UPCOMING)
            {
                FilterdBookings = new ObservableCollection<BookingInfo>(Bookings.Where(x => x.StartDate > DateTime.Now));
            }
            else if (obj.Title == AppResources.MB_ARCHIVE)
            {
                FilterdBookings = new ObservableCollection<BookingInfo>(Bookings.Where(x => x.StartDate < DateTime.Now));
            }
        }
        private List<CheckboxViewModel> GetSortOptions()
        {
            return new List<CheckboxViewModel>
            {
                new CheckboxViewModel{ IsSelected = true, Title=AppResources.MB_UPCOMING },
                new CheckboxViewModel{ IsSelected = false, Title=AppResources.MB_ARCHIVE },
            };
        }
    }
}

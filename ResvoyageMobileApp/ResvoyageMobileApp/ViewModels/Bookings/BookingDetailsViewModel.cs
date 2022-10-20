using ResvoyageMobileApp.Models.Booking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ResvoyageMobileApp.ViewModels.Bookings
{
    public class BookingDetailsViewModel : BaseViewModel
    {
        public BookingDetailsViewModel(BookingDetails response, BookingInfo booking)
        {
            if (response != null && response.Details != null && response.Details.Count > 0)
            {
                _details = new ObservableCollection<BookingDetailsResponse>(response.Details);
                _booking = booking;
            }
        }

        private ObservableCollection<BookingDetailsResponse> _details;

        public ObservableCollection<BookingDetailsResponse> Details
        {
            get { return _details; }
            set { SetValue(ref _details, value); }
        }
        private BookingInfo _booking;

        public BookingInfo Booking
        {
            get { return _booking; }
            set { SetValue(ref _booking, value); }
        }

    }
}

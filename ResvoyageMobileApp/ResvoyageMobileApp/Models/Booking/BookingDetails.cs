using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.Booking
{
    public class BookingDetails
    {
        public ErrorResult Error { get; set; }
        public List<BookingDetailsResponse> Details { get; set; }
    }
}

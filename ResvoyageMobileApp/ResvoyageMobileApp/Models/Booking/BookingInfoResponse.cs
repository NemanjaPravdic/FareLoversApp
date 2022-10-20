using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.Booking
{
    public class BookingInfoResponse
    {
        public ErrorResult Error { get; set; }
        public List<BookingInfo> Bookings { get; set; }
        
    }
}

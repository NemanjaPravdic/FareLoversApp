using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models
{
    public class Place
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string City { get; set; }
        public string IATACode { get; set; }
        public string Country { get; set; }
        public int AvailableFlights { get; set; }
        public string TimeZone { get; set; }
        public string About { get; set; }
        public string Currency { get; set; }
    }
}

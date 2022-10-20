using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.Flight
{
    public class FlightRequest
    {
        public string From1 { get; set; }
        public string From2 { get; set; }
        public string From3 { get; set; }
        public string From4 { get; set; }

        public string To1 { get; set; }
        public string To2 { get; set; }
        public string To3 { get; set; }
        public string To4 { get; set; }

        public string DepartureDate1 { get; set; }
        public string DepartureDate2 { get; set; }
        public string DepartureDate3 { get; set; }
        public string DepartureDate4 { get; set; }

        public int Adults { get; set; }
        public int Children { get; set; }
        public int Infants { get; set; }
        public bool FlexibleDates { get; set; }
        public string SessionId { get; set; }
        public CabinType FlightClass { get; set; }
    }
}

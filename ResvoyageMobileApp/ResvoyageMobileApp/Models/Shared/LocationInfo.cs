using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.Shared
{
    public class LocationInfo
    {
        public string IATACode { get; set; }
        public string CountryCode { get; set; }
        public decimal LAT { get; set; }
        public decimal LON { get; set; }
        public bool IsInternational { get; set; }
        public bool IsAirport { get; set; }
        public string StateName { get; set; }
        public bool IsGroup { get; set; }
        public int Weight { get; set; }
        public DateTime? DepartureDate { get; set; }
        public string DepartureDateString { get; set; }
        public bool IsCity { get; set; }
        public string RegionCode { get; set; }
        public string TimeZone { get; set; }
        public int? Offset { get; set; }
        public int? DstOffset { get; set; }
        public string PlaceId { get; set; }
        public string PlaceName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.Hotel
{
    public class HotelCityData
    {
        public string Code { get; set; }
        public string CountryCode { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string PlaceId { get; set; }
        public DateTime SelectedDate { get; set; }
        public bool IsAirport 
        { 
            get
            {
                return Type == "Airport";
            }
        }
        public bool IsDestination
        {
            get
            {
                return Type == "Destination";
            }
        }
        public bool IsCity
        {
            get
            {
                return Type == "City";
            }
        }
        public bool IsHotel
        {
            get
            {
                return Type == "Hotel";
            }
        }
    }
}

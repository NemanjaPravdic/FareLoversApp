using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.Hotel
{
    public class HotelDetailsRequest
    {
        public string SessionId { get; set; }
        public string RoomId { get; set; }
        public string HotelCityCode { get; set; }
        public string HotelCode { get; set; }
        public string ChainCode { get; set; }
        public string HotelCodeContext { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public string[] RoomIds { get; set; }
        public RoomDetailsRequest Room { get; set; }
    }

    public class RoomDetailsRequest
    {
        public string RatePlanCode { get; set; }
        public string BookingCode { get; set; }
        public string GuaranteeType { get; set; }
        public string RoomTypeCode { get; set; }
        public decimal RoomRate { get; set; }
    }
}

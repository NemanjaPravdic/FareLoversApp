using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.Car
{
    public class CarSearchRequest
    {
        public Guid SessionId { get; set; }
        public string PickupCity { get; set; }
        public string DropOffCity { get; set; }
        public string PickupDate { get; set; }
        public string DropOffDate { get; set; }
        public int? PickupTime { get; set; }
        public int? DropoffTime { get; set; }
        public string CarType { get; set; }
        public string CarCategory { get; set; }
        public string CorporateDiscount { get; set; }
        public string PromotionCode { get; set; }
        public bool? AirCondition { get; set; }
        public bool? Automatic { get; set; }
        public bool? UnlimitedMiles { get; set; }
        public int? TripReasonId { get; set; }
        public string TripName { get; set; }
        public string Vendor { get; set; }
        public string CurrencyCode { get; set; }
    }
}

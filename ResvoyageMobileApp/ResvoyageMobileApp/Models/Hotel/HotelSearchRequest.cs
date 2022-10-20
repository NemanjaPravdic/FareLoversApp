using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.Hotel
{
    public class HotelSearchRequest
    {
        public string CurrencyCode { get; set; }
        public Guid SessionId { get; set; }
        public string HotelCityCode { get; set; }
        public string HotelCode { get; set; }
        public string HotelChainCode { get; set; }
        public string HotelName { get; set; }
        public string HotelContext { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int[] ChildrenAge { get; set; }
        public int RoomCount { get; set; }
        public string RoomType { get; set; }
        public string RatePlan { get; set; }
        public string PropertyCode { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string Area { get; set; }
        public string PointOfInterest { get; set; }
        public string CityName { get; set; }
        /// <summary>
        /// Number of adults in room no. 2
        /// </summary>
        public int Adults2 { get; set; }
        /// <summary>
        /// Number of childrens in room no. 2
        /// </summary>
        public int Children2 { get; set; }
        public int[] ChildrenAge2 { get; set; }
        /// <summary>
        /// Number of adults in room no. 3
        /// </summary>
        public int Adults3 { get; set; }
        /// <summary>
        /// Number of childrens in room no. 3
        /// </summary>
        public int Children3 { get; set; }
        public int[] ChildrenAge3 { get; set; }
        /// <summary>
        /// Number of adults in room no. 4
        /// </summary>
        public int Adults4 { get; set; }
        /// <summary>
        /// Number of childrens in room no. 4
        /// </summary>
        public int Children4 { get; set; }
        public int[] ChildrenAge4 { get; set; }
        public string PlaceId { get; set; }
        public int? RadiusSize { get; set; }
        public string RadiusUnit { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}

using ResvoyageMobileApp.Models.Hotel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.Booking
{
    public class BookingDetailsResponse
    {
        public string PNR { get; set; }
        public int EmployeeTripSummeryId { get; set; }
        public bool IsAir { get; set; }
        public bool IsCar { get; set; }
        public bool IsHotel { get; set; }
        public decimal? Deposit { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDateString
        {
            get
            {
                return StartDate.ToString("dd MMM yyyy");
            }
        }
        public string StartDateTimeString
        {
            get
            {
                return StartDate.ToString("HH:mm tt");
            }
        }
        public DateTime EndDate { get; set; }
        public string EndDateString
        {
            get
            {
                return EndDate.ToString("dd MMM yyyy");
            }
        }
        public string EndDateTimeString
        {
            get
            {
                return EndDate.ToString("HH:mm tt");
            }
        }
        public DateTime DateRequested { get; set; }
        public string DateRequestedString
        {
            get
            {
                return DateRequested.ToString("dd MMM yyyy");
            }
        }

        //vehicle info
        public string VehicleCategory { get; set; }
        public bool? Aircondition { get; set; }
        public string TransmissionType { get; set; }
        public string VehicleClass { get; set; }
        public string CarVendorName { get; set; }
        public string CarVendorType { get; set; }

        //air info
        public List<AirInfo> FlightInfo { get; set; }
        public string DestinationCode { get; set; }
        public string OriginCode { get; set; }

        public string Destination { get; set; }
        public string Origin { get; set; }
        public decimal TotalCost { get; set; }
        public decimal? LowestPrice { get; set; }
        public string ChainCode { get; set; }

        //hotel info
        public string HotelName { get; set; }
        public RoomAdditionalDetails RoomAdditionalDetails { get; set; }
    }
    public class RoomAdditionalDetails
    {
        public string[] cancellationPolicy { get; set; }
        public List<TaxToPay> TaxesToPay { get; set; }
        public KeyCollectionInfoObject KeyCollectionInfo { get; set; }
        public string CheckInTime { get; set; }
        public string ImportantInformation { get; set; }
    }
    public class AirInfo
    {
        public DateTime StartDate { get; set; }
        public string StartDateString
        {
            get
            {
                return StartDate.ToString("dd MMM yyyy");
            }
        }
        public string StartDateTimeString
        {
            get
            {
                return StartDate.ToString("HH:mm tt");
            }
        }
        public DateTime EndDate { get; set; }
        public string EndDateString
        {
            get
            {
                return EndDate.ToString("dd MMM yyyy");
            }
        }
        public string EndDateTimeString
        {
            get
            {
                return EndDate.ToString("HH:mm tt");
            }
        }
        public string FlightNumber { get; set; }
        public string AirlineName { get; set; }
        public string CabinPref { get; set; }
        public string DestinationCode { get; set; }
        public string OriginCode { get; set; }
        public string Destination { get; set; }
        public string Origin { get; set; }
    }
}

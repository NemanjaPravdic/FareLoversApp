using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.Booking
{
    public class BookingInfo
    {
        public int Id { get; set; }
        public string PNR { get; set; }
        public DateTime DateRequested { get; set; }
        public string DateRequestedString {
            get
            {
                return DateRequested.ToString("ddd dd MMM");
            }
        }
        public DateTime StartDate { get; set; }
        public string StartDateString
        {
            get
            {
                return StartDate.ToString("ddd dd MMM");
            }
        }
        public DateTime EndDate { get; set; }
        public string EndDateString
        {
            get
            {
                return EndDate.ToString("ddd dd MMM");
            }
        }
        public decimal TotalCost { get; set; }
        public decimal AirCost { get; set; }
        public decimal HotelCost { get; set; }
        public decimal CarCost { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol {
            get
            {
                var symbol = Util.GetCurrencySymbol(CurrencyCode);

                if (CurrencyCode == "UZS" || CurrencyCode == symbol)
                    return CurrencyCode + " ";
                else
                    return string.Format("{0} {1}", CurrencyCode, symbol);
            }
        }
        public bool ContainsAir { get; set; }
        public bool ContainsCar { get; set; }
        public bool ContainsHotel { get; set; }
        public string Destination { get; set; }
        public string Origin { get; set; }
        public string DestinationInfo
        {
            get
            {
                return string.IsNullOrEmpty(Destination) ? Origin : string.Format("{0} ➝ {1}", Origin, Destination);
            }
        }
    }
}

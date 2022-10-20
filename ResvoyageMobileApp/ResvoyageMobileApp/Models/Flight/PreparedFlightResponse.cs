using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ResvoyageMobileApp.Models.Flight
{
    public class PreparedFlightResults
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public decimal Total { get; set; }
        public string DisplayTotalPrice
        {
            get
            {
                if (Currency == "UZS")
                {
                    NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
                    var tmp = Math.Round(Total).ToString("n", CultureInfo.CurrentCulture);

                    if (tmp.Contains(nfi.NumberDecimalSeparator))
                        return tmp.Split(nfi.NumberDecimalSeparator.ToCharArray())[0];
                    else
                        return tmp;
                }
                else
                    return Total.ToString("n", CultureInfo.CurrentCulture);
            }

        }
        public string Currency { get; set; }
        public string CurrencySymbol
        {
            get
            {
                var symbol = Util.GetCurrencySymbol(Currency);

                if (Currency == "UZS" || Currency == symbol)
                    return Currency + " ";
                else
                    return string.Format("{0} {1}", Currency, symbol);
            }
        }
        public string StringPrice
        {
            get
            {
                return string.Format("{0}{1}", CurrencySymbol, DisplayTotalPrice);
            }
        }
        public List<FlightSegmentOrganized> ListSegments { get; set; }
        public string AirlineImage { get; set; }
        public string AirlaneName { get; set; }
        public PricedItinerary FlightInfo { get; set; }
        public TimeSpan TotalDuration { get; set; }
        public DateTime? FirstDateFrom { get; set; }
        public bool IsBestPrice { get; set; }
        public bool IsDifferentDate { get; set; }
        public string DepartureDate {
            get
            {
                var firstSegment = ListSegments.FirstOrDefault();
                if (firstSegment != null && firstSegment.DateFrom.HasValue)
                    return firstSegment.DateFrom.Value.ToString("dd MMM");
                else
                    return null;
            }
        }
        public string ArrivalDate 
        {
            get
            {
                var lastSegment = ListSegments.LastOrDefault();
                if (lastSegment != null && lastSegment.DateFrom.HasValue)
                    return lastSegment.DateFrom.Value.ToString("dd MMM");
                else
                    return null;
            }
        }
        public string NewDate
        {
            get
            {
                if (!string.IsNullOrEmpty(DepartureDate) && !string.IsNullOrEmpty(ArrivalDate))
                {
                    return DepartureDate == ArrivalDate ? ArrivalDate : string.Format("{0} - {1}", DepartureDate, ArrivalDate);
                }
                else
                    return null;
            }
        }

    }

    public class FlightSegmentOrganized
    {
        public int Index { get; set; }
        public string AirlineCode { get; set; }
        public string AirlineName { get; set; }
        public string AirlineOpCode { get; set; }
        public string AirlineOpName { get; set; }
        public string BookingClass { get; set; }
        public string Cabin { get; set; }
        public string DurationTotal { get; set; }
        public string FlightNumber { get; set; }
        public string PlaceFrom { get; set; }
        public string PlaceTo { get; set; }
        public string PlaceFromIATA { get; set; }
        public string PlaceToIATA { get; set; }
        public string IATAInfo { get; set; }
        public string RouteNumber { get; set; }
        public int SectrorSequence { get; set; }
        public string Transfer { get; set; }
        public int Stops { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string TimeFrom
        {
            get
            {
                return DateFrom?.ToString("HH:mm") ?? "";
            }
        }
        public string TimeTo
        {
            get
            {
                return DateTo?.ToString("HH:mm") ?? "";
            }
        }
        public string TimeInfo
        {
            get
            {
                return string.Format("{0} - {1}", TimeFrom, TimeTo);
            }
        }
        public string DateString
        {
            get
            {
                return DateFrom?.ToString("ddd, dd MMM") ?? "";
            }
        }
    }
}

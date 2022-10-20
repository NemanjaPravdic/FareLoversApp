using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ResvoyageMobileApp.Models.Flight
{
    public class PreparedFlightDetails
    {
        public Guid SessionId { get; set; }
        public Guid Id { get; set; }
        public decimal Total { get; set; }
        public string Currency { get; set; }
        public string CurrencySymbol {
            get
            {
                var symbol = Util.GetCurrencySymbol(Currency);

                if (Currency == "UZS" || Currency == symbol)
                    return Currency + " ";
                else
                    return string.Format("{0} {1}", Currency, symbol);
            }
        }
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
        public List<DestinationOptions> ListDestinationOptions { get; set; }
        public string BookText
        {
            get
            {
                return string.Format("Book flight: {0}{1}", CurrencySymbol, DisplayTotalPrice);
            }
        }

    }
    public class DestinationOptions
    {
        public string From { get; set; }
        public string To { get; set; }
        public TimeSpan TotalDuration { get; set; }
        public List<FlightSegmentInfo> ListFlightSegments { get; set; }
        public string FromToTitle {
            get {
                return From + " - " + To;
            }
        }
        public string TotalDurationString
        {
            get
            {
                return TotalDuration.Hours > 0 ? string.Format("{0}h {1}min", Math.Floor(TotalDuration.TotalHours), TotalDuration.Minutes) : string.Format("{0}min", TotalDuration.Minutes);
            }
        }

    }

    public class FlightSegmentInfo
    {
        public string RouteNumber { get; set; }
        public string FlightNumber { get; set; }
        public string MarketingAirlineCode { get; set; }
        public string MarketingAirlineName { get; set; }
        public string OperatingAirlineName { get; set; }
        public string OperatingAirlineCode { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string DepartureAirport { get; set; }
        public string DepartureCountryCode { get; set; }
        public string ArrivalAirport { get; set; }
        public string ArrivalCountryCode { get; set; }
        public string Aircraft { get; set; }
        public string BookingClass { get; set; }
        public string ArrivalAirportName { get; set; }
        public string DepartureAirportName { get; set; }
        public string BaggageFeeUrl { get; set; }
        public List<Baggage> FreeBaggages { get; set; }
        public string Cabin { get; set; }
        public int? SeatsLeft { get; set; }
        public string AirlineImage { get; set; }
        public string Layover { get; set; }
        public string OperatedBy { get; set; }
        public string LayoverText
        {
            get
            {
                return string.Format("{0} : {1}", Resources.AppResources.FD_LAYOVER, Layover);
            }
        }
        public string FlightNumberText
        {
            get
            {
                return string.Format("{0} : {1}", Resources.AppResources.FD_FLIGHT_NUMBER, FlightNumber);
            }
        }
        public string DurationString 
        {
            get 
            {
                return Duration.Hours > 0 ? string.Format("{0}h {1}min", Math.Floor(Duration.TotalHours), Duration.Minutes) : string.Format("{0}min", Duration.Minutes);
            }
        }
        public string DepartureTimeString
        {
            get
            {
                return DepartureDate.ToString("HH:mm");
            }
        }
        public string DepartureDateString
        {
            get
            {
                return DepartureDate.ToString("dd MMM, ddd");
            }
        }
        public string ArrivalTimeString
        {
            get
            {
                return ArrivalDate.ToString("HH:mm");
            }
        }
        public string ArrivalDateString
        {
            get
            {
                return ArrivalDate.ToString("dd MMM, ddd");
            }
        }

        public string CabinInfo
        {
            get 
            {
                return string.Format("{0} ({1})", Cabin, BookingClass);
            }
        }
        public string LayoverString
        {
            get
            {
                return string.Format("Layover: {0}", Layover);
            }
        }
        public string AirlineNameAndNumber
        {
            get
            {
                return string.Format("{0} {1}", MarketingAirlineName, FlightNumber);
            }
        }
        public string DepartureInfo
        {
            get
            {
                if (!string.IsNullOrEmpty(DepartureAirportName))
                {
                    var tmp = DepartureAirportName.Split(',');
                    if (tmp != null)
                        return tmp[0];
                    else
                        return null;
                }
                else
                    return null;
            }
        }
        public string ArrivalInfo
        {
            get
            {
                if (!string.IsNullOrEmpty(ArrivalAirportName))
                {
                    var tmp = ArrivalAirportName.Split(',');
                    if (tmp != null)
                        return tmp[0];
                    else
                        return null;
                }
                else
                    return null;
            }
        }
    }
}

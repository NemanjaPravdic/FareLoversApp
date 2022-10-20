using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.Flight
{
    public class FareRulesResponse
    {
        public FareRuleResponseInfo[] FareRuleResponseInfo { get; set; }
        public Error[] Errors { get; set; }
        public ErrorResult Error { get; set; }
    }

    public class Error
    {
        public string Language { get; set; }
        public string Type { get; set; }
        public string ShortText { get; set; }
    }

    public class FareRuleResponseInfo
    {
        public FareRuleInfo FareRuleInfo { get; set; }
        public FareRules FareRules { get; set; }
    }

    public class FareRules
    {
        public SubSection[] SubSection { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
    }

    public class SubSection
    {
        public Paragraph[] Paragraph { get; set; }
        public string SubTitle { get; set; }
        public string SubCode { get; set; }
        public int SubSectionNumber { get; set; }
        public bool SubSectionNumberSpecified { get; set; }
    }

    public class Paragraph
    {
        public Text Text { get; set; }
        public string Image { get; set; }
        public string URL { get; set; }
        public ListItem ListItem { get; set; }
        public string Name { get; set; }
        public int ParagraphNumber { get; set; }
        public bool ParagraphNumberSpecified { get; set; }
        public System.DateTime CreateDateTime { get; set; }
        public bool CreateDateTimeSpecified { get; set; }
        public string CreatorID { get; set; }
        public System.DateTime LastModifyDateTime { get; set; }
        public bool LastModifyDateTimeSpecified { get; set; }
        public string LastModifierID { get; set; }
        public string Language { get; set; }
    }

    public class ListItem
    {
        public bool Formatted { get; set; }
        public string Language { get; set; }
        public int ListItem1 { get; set; }
        public string Value { get; set; }
    }

    public class Text
    {
        public bool Formatted { get; set; }
        public string Language { get; set; }
        public string Value { get; set; }
    }

    public class FareRuleInfo
    {
        public System.DateTime DepartureDate { get; set; }
        public bool DepartureDateSpecified { get; set; }
        public string FareReference { get; set; }
        public RuleInfo RuleInfo { get; set; }
        public FilingAirline FilingAirline { get; set; }
        public MarketingAirline[] MarketingAirline { get; set; }
        public AirportRuleInfo DepartureAirport { get; set; }
        public AirportRuleInfo ArrivalAirport { get; set; }
        public bool NegotiatedFare { get; set; }
        public string NegotiatedFareCode { get; set; }
        public string PassengerType { get; set; }
    }

    public class AirportRuleInfo
    {
        public string LocationCode { get; set; }
        public string CodeContext { get; set; }
        public string Value { get; set; }
    }

    public class MarketingAirline
    {
        public string CompanyShortName { get; set; }
        public string TravelSector { get; set; }
        public string Code { get; set; }
        public string CodeContext { get; set; }
        public string Value { get; set; }
    }

    public class FilingAirline
    {
        public string CompanyShortName { get; set; }
        public string TravelSector { get; set; }
        public string Code { get; set; }
        public string CodeContext { get; set; }
        public string Value { get; set; }
    }

    public class RuleInfo
    {
        public ResTicketingRules ResTicketingRules { get; set; }
        public LengthOfStayRules LengthOfStayRules { get; set; }
        public ChargesRules ChargesRules { get; set; }
    }

    public class ChargesRules
    {
        public VoluntaryChanges VoluntaryChanges { get; set; }
    }

    public class VoluntaryChanges
    {
        public Penalty Penalty { get; set; }
        public bool VolChangeInd { get; set; }
        public bool VolChangeIndSpecified { get; set; }
    }

    public class Penalty
    {
        public string PenaltyType { get; set; }
        public string DepartureStatus { get; set; }
        public double Amount { get; set; }
        public bool AmountSpecified { get; set; }
        public string CurrencyCode { get; set; }
        public int DecimalPlaces { get; set; }
        public bool DecimalPlacesSpecified { get; set; }
        public double Percent { get; set; }
        public bool PercentSpecified { get; set; }
    }

    public class LengthOfStayRules
    {
        public MinimumStay MinimumStay { get; set; }
        public MaximumStay MaximumStay { get; set; }
        public bool StayRestrictionsInd { get; set; }
        public bool StayRestrictionsIndSpecified { get; set; }
    }

    public class MaximumStay
    {
        public System.DateTime ReturnTimeOfDay { get; set; }
        public bool ReturnTimeOfDaySpecified { get; set; }
        public MaximumStayReturnType ReturnType { get; set; }
        public bool ReturnTypeSpecified { get; set; }
        public int MaxStay { get; set; }
        public bool MaxStaySpecified { get; set; }
        public StayUnit StayUnit { get; set; }
        public bool StayUnitSpecified { get; set; }
        public System.DateTime MaxStayDate { get; set; }
        public bool MaxStayDateSpecified { get; set; }
    }

    public enum MaximumStayReturnType
    {

        /// <remarks/>
        C,

        /// <remarks/>
        S,
    }

    public class MinimumStay
    {
        public System.DateTime ReturnTimeOfDay { get; set; }
        public bool ReturnTimeOfDaySpecified { get; set; }
        public int MinStay { get; set; }
        public bool MinStaySpecified { get; set; }
        public StayUnit StayUnit { get; set; }
        public bool StayUnitSpecified { get; set; }
        public System.DateTime MinStayDate { get; set; }
        public bool MinStayDateSpecified { get; set; }
    }

    public enum StayUnit
    {
        /// <remarks/>
        Minutes,

        /// <remarks/>
        Hours,

        /// <remarks/>
        Days,

        /// <remarks/>
        Months,

        /// <remarks/>
        MON,

        /// <remarks/>
        TUES,

        /// <remarks/>
        WED,

        /// <remarks/>
        THU,

        /// <remarks/>
        FRI,

        /// <remarks/>
        SAT,

        /// <remarks/>
        SUN,
    }

    public class ResTicketingRules
    {
        public AdvResTicketing AdvResTicketing { get; set; }
    }

    public class AdvResTicketing
    {
        public AdvReservation AdvReservation { get; set; }
        public AdvTicketing AdvTicketing { get; set; }
        public bool AdvResInd { get; set; }
        public bool AdvResIndSpecified { get; set; }
        public bool AdvTicketingInd { get; set; }
        public bool AdvTicketingIndSpecified { get; set; }
    }

    public class AdvTicketing
    {
        public System.DateTime FromResTimeOfDay { get; set; }
        public bool FromResTimeOfDaySpecified { get; set; }
        public string FromResPeriod { get; set; }
        public AdvReservationLatestUnit FromResUnit { get; set; }
        public bool FromResUnitSpecified { get; set; }
        public System.DateTime FromDepartTimeOfDay { get; set; }
        public bool FromDepartTimeOfDaySpecified { get; set; }
        public string FromDepartPeriod { get; set; }
        public AdvReservationLatestUnit FromDepartUnit { get; set; }
        public bool FromDepartUnitSpecified { get; set; }
    }

    public class AdvReservation
    {
        public System.DateTime LatestTimeOfDay { get; set; }
        public bool LatestTimeOfDaySpecified { get; set; }
        public string LatestPeriod { get; set; }
        public AdvReservationLatestUnit LatestUnit { get; set; }
        public bool LatestUnitSpecified { get; set; }
    }

    public enum AdvReservationLatestUnit
    {
        /// <remarks/>
        Minutes,

        /// <remarks/>
        Hours,

        /// <remarks/>
        Days,

        /// <remarks/>
        Months,

        /// <remarks/>
        MON,

        /// <remarks/>
        TUES,

        /// <remarks/>
        WED,

        /// <remarks/>
        THU,

        /// <remarks/>
        FRI,

        /// <remarks/>
        SAT,

        /// <remarks/>
        SUN,
    }
}

using ResvoyageMobileApp.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ResvoyageMobileApp.Models.Hotel
{
    public class HotelSearchResponse
    {
        public Guid SessionId { get; set; }
        public List<HotelInformation> Hotels { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public ErrorResult Error { get; set; }
        public string ErrorMessage { get; set; }

    }

    public class HotelInformation
    {
        public Guid Id { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol
        {
            get
            {
                var symbol = Util.GetCurrencySymbol(CurrencyCode);

                if (CurrencyCode == "UZS" || CurrencyCode == symbol)
                    return CurrencyCode + " ";
                else
                    return string.Format("{0} {1}", CurrencyCode, symbol);
            }
        }
        public decimal DailyRatePerRoom { get; set; }
        public string DisplayDailyRatePerRoom {
            get
            {
                if (CurrencyCode == "UZS")
                {
                    NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
                    var tmp = Math.Round(DailyRatePerRoom).ToString("n", CultureInfo.CurrentCulture);

                    if (tmp.Contains(nfi.NumberDecimalSeparator))
                        return tmp.Split(nfi.NumberDecimalSeparator.ToCharArray())[0];
                    else
                        return tmp;
                }
                else
                    return DailyRatePerRoom.ToString("n", CultureInfo.CurrentCulture);
            }
        }
        public DateTime CheckInDate { get; set; }
        public string CheckInDateString
        {
            get
            {
                return CheckInDate.ToString("dd MMM");
            }
        }
        public DateTime CheckOutDate { get; set; }
        public string CheckOutDateString
        {
            get
            {
                return CheckOutDate.ToString("dd MMM");
            }
        }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        public string ChainName { get; set; }
        public string ChainCode { get; set; }
        public string HotelName { get; set; }
        public string HotelCode { get; set; }
        public string HotelCodeContext { get; set; }
        public AddressInfo HotelAddress { get; set; }
        public string HotelMainImage { get; set; }
        public List<string> HotelImages { get; set; }
        public List<string> HotelMainText { get; set; }
        public List<string> HotelDescription { get; set; }
        public string LocationDescription { get; set; }
        public string AttractionDescription { get; set; }
        public string RoomInfoDescription { get; set; }
        //public string Policy { get; set; }
        public List<string> Recreation { get; set; }
        public string OnSiteServices { get; set; }
        public string HotelPhone { get; set; }
        public string HotelFax { get; set; }
        public string HotelCityCode { get; set; }
        public Dictionary<string, string> HotelAmenities { get; set; }
        public Dictionary<string, string> RoomAmenities { get; set; }
        public List<HotelAmenityItem> HotelAmenitiesCollection { get; set; }
        public List<RoomAmenityItem> RoomAmenitiesCollection { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        //public bool IsAddedToCart { get; set; }
        public string PolicyType { get; set; }
        public string PaymentTerms { get; set; }
        //public bool IsHotelSpecialCodeApplicable { get; set; }
        //public int PreferenceLevel { get; set; }
        public List<HotelAward> HotelAwards { get; set; }

        public PaymentDetails PaymentDetails { get; set; }

        public List<RoomInfo> Rooms { get; set; }

        public KeyCollectionInfoObject KeyCollectionInfo { get; set; }
        public string HotelType { get; set; }

        public int GuestCount { get; set; }
        public string CityNameOverrided
        {
            get
            {
                if (HotelAddress != null)
                {
                    return HotelAddress.CityNameOverrided;
                }
                else
                    return null;
            }
        }
        public string StarRating 
        {
            get
            {
                if (HotelAwards != null && HotelAwards.Count > 0)
                {
                    return HotelAwards[0].Rating;
                }
                else
                    return null;
            }
        }
        public decimal? StarRatingDecimal
        {
            get
            {
                if (StarRating != null)
                {
                    decimal star;
                    if (Decimal.TryParse(StarRating, out star))
                        return star;
                    else
                        return null;
                }
                else
                    return null;
            }
        }
        public bool HasPool
        {
            get
            {
                if (HotelAmenitiesCollection != null)
                {
                    return HotelAmenitiesCollection.Any(x => x.Code == "HAC49" || x.Code == "HAC54" || x.Code == "HAC66" || x.Code == "HAC71" || x.Code == "HAC253");
                }
                else
                    return false;
            }
        }
        public bool HasWiFi
        {
            get
            {
                if (HotelAmenitiesCollection != null)
                {
                    return HotelAmenitiesCollection.Any(x => x.Code == "HAC179" || x.Code == "HAC261" || x.Code == "HAC286" || x.Code == "RMA123");
                }
                else
                    return false;
            }
        }
        public bool HasParking
        {
            get
            {
                if (HotelAmenitiesCollection != null)
                {
                    return HotelAmenitiesCollection.Any(x => x.Code == "HAC53" || x.Code == "HAC65" || x.Code == "HAC68");
                }
                else
                    return false;
            }
        }
        public bool HasAirConditioning
        {
            get
            {
                if (HotelAmenitiesCollection != null)
                {
                    return HotelAmenitiesCollection.Any(x => x.Code == "HAC5");
                }
                else
                    return false;
            }
        }
        public bool HasBar
        {
            get
            {
                if (HotelAmenitiesCollection != null)
                {
                    return HotelAmenitiesCollection.Any(x => x.Code == "HAC165");
                }
                else
                    return false;
            }
        }
        public bool HasSpa
        {
            get
            {
                if (HotelAmenitiesCollection != null)
                {
                    return HotelAmenitiesCollection.Any(x => x.Code == "HAC84");
                }
                else
                    return false;
            }
        }
        public bool HasRestaurant
        {
            get
            {
                if (HotelAmenitiesCollection != null)
                {
                    return HotelAmenitiesCollection.Any(x => x.Code == "HAC76");
                }
                else
                    return false;
            }
        }
        public bool HasRoomService
        {
            get
            {
                if (HotelAmenitiesCollection != null)
                {
                    return HotelAmenitiesCollection.Any(x => x.Code == "HAC77");
                }
                else
                    return false;
            }
        }
        public bool HasGym
        {
            get
            {
                if (HotelAmenitiesCollection != null)
                {
                    return HotelAmenitiesCollection.Any(x => x.Code == "HAC35");
                }
                else
                    return false;
            }
        }
        public bool HasHousekeeping
        {
            get
            {
                if (HotelAmenitiesCollection != null)
                {
                    return HotelAmenitiesCollection.Any(x => x.Code == "HAC50" || x.Code == "HAC51");
                }
                else
                    return false;
            }
        }
        public bool HasPetFriendly
        {
            get
            {
                if (HotelAmenitiesCollection != null)
                {
                    return HotelAmenitiesCollection.Any(x => x.Code == "HAC224");
                }
                else
                    return false;
            }
        }
    }
    public class HotelAmenityItem
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class RoomAmenityItem
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class HotelAward
    {
        public string Provider { get; set; }
        public string Rating { get; set; }
    }
    public class PaymentDetails
    {
        public string[] AcceptedCreditCards { get; set; }
        public string[] PaymentDescription { get; set; }
        public string[] CancellationPolicy { get; set; }
        public TotalBreakdown TotalBreakdown { get; set; }
    }
    public class TotalBreakdown
    {
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; }
        public decimal TaxAmount { get; set; }
        public bool TaxIncluded { get; set; }
        public string TaxDescription { get; set; }
    }
    public class KeyCollectionInfoObject
    {
        public string HowToCollect { get; set; }
        public string KeyLocation { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
    public class RoomInfo
    {
        public Guid Id { get; set; }
        public string BookingCode { get; set; }
        public bool IsAddedToCart { get; set; }
        public bool? PriceWithoutTax { get; set; }
        public string RatePlanCode { get; set; }
        public DateTime HoldTime { get; set; }
        public string Status { get; set; }
        public string GuaranteeType { get; set; }
        public bool? MarkupsCalculated { get; set; }
        public List<string> PromoTypes { get; set; }
        public decimal RoomRate { get; set; }
        public decimal BaseRoomRate { get; set; }
        public decimal RoomTax { get; set; }
        public decimal TotalTaxes { get; set; }
        public string Inclusion { get; set; }
        public string ResultIndex { get; set; }
        public List<TaxToPay> TaxesToPay { get; set; }
        public string RoomTypeCode { get; set; }
        public decimal DisplayRoomRate { get; set; }
        public decimal TBORoomRate { get; set; }
        public string[] RoomText { get; set; }
        public bool RateChange { get; set; }
        public string Category { get; set; }
        public string Meal { get; set; }
        public string NumberOfBeds { get; set; }
        public string NumberOfRooms { get; set; }
        public string BedType { get; set; }
        public DailyRate DailyRate { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        public List<RoomRates> RoomDailyrate { get; set; }
        public string[] cancellationPolicy { get; set; }
        [Obsolete]
        public bool IsMarkUpDisplay { get; set; }
        [Obsolete]
        public decimal MarkUp { get; set; }
        [Obsolete]
        public string MarkUpType { get; set; }
        public string PolicyType { get; set; }
        [Obsolete]
        public string MarkupName { get; set; }
        public string PolicyDisplayName { get; set; }
        public Markup PromotionalDiscount { get; set; }
        public List<HotelMarkup> Markups { get; set; }
        public CancelPenalty CancelPenalty { get; set; }
        public bool DisallowBooking { get; set; }
        public bool SendToOutOfPolicy { get; set; }
        public int MealPlanId { get; set; }
        public List<CancelPenaltyNormalized> CancellationPenalties { get; set; }
        public List<RoomAmenityItem> RoomAmenitiesCollection { get; set; }
        public string CurrencyCode { get; set; }
        public string RoomRateDisplay
        {
            get
            {
                if (CurrencyCode != null && CurrencyCode == "UZS")
                {
                    NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
                    var tmp = Math.Round(RoomRate).ToString("n", CultureInfo.CurrentCulture);

                    if (tmp.Contains(nfi.NumberDecimalSeparator))
                        return tmp.Split(nfi.NumberDecimalSeparator.ToCharArray())[0];
                    else
                        return tmp;
                }
                else
                    return RoomRate.ToString("n", CultureInfo.CurrentCulture);
            }
        }
        public string RoomInformation
        {
            get {
                return string.Format("{0} {1}", Category != null ? Category : "", NumberOfBeds != null && BedType != null ? NumberOfBeds + " " + BedType  : "");
            }
        }
        public string NumberOfBedsString
        {
            get
            {
                if (!string.IsNullOrEmpty(NumberOfBeds))
                    return string.Format("{0} {1}", NumberOfBeds, NumberOfBeds == "1" ? AppResources.RD_BED : AppResources.RD_BEDS);
                else
                    return null;
            }
        }
        public bool HasFreeCancellation
        {
            get
            {
                if (CancellationPenalties != null && CancellationPenalties.Count > 0)
                {
                    if (CancellationPenalties.Any(x => x.Amount == 0))
                        return true;
                    else
                        return false;
                }
                return false;
            }
        }
        public string LastDayToCancel
        {
            get
            {
                if (CancellationPenalties != null && CancellationPenalties.Count > 0 )
                {
                    var cancelObject = CancellationPenalties.FirstOrDefault(x => x.Amount == 0);
                    if (cancelObject != null)
                        return string.Format("{0} {1},{2}", AppResources.RD_BEFORE, cancelObject.DeadlineDate.ToString("ddd"), cancelObject.DeadlineDate.ToString("dd MMM"));
                    else
                        return null;
                }
                return null;
            }
        }
        public bool HasHairdryer
        {
            get
            {
                if (RoomAmenitiesCollection != null)
                {
                    return RoomAmenitiesCollection.Any(x => x.Code == "RMA50");
                }
                else
                    return false;
            }
        }
        public bool HasAirConditioning
        {
            get
            {
                if (RoomAmenitiesCollection != null)
                {
                    return RoomAmenitiesCollection.Any(x => x.Code == "RMA2");
                }
                else
                    return false;
            }
        }
        public bool HasWiFi
        {
            get
            {
                if (RoomAmenitiesCollection != null)
                {
                    return RoomAmenitiesCollection.Any(x => x.Code == "RMA51" || x.Code == "RMA123" || x.Code == "RMA226");
                }
                else
                    return false;
            }
        }
        public bool HasTV
        {
            get
            {
                if (RoomAmenitiesCollection != null)
                {
                    return RoomAmenitiesCollection.Any(x => x.Code == "RMA273" || x.Code == "RMA251");
                }
                else
                    return false;
            }
        }
        public bool HasBreakfast
        {
            get
            {
                if (RoomAmenitiesCollection != null)
                {
                    return RoomAmenitiesCollection.Any(x => x.Code == "RMA189" || x.Code == "RMA160" || x.Code == "RMA190");
                }
                else
                    return false;
            }
        }
        
    }
    public class TaxToPay
    {
        public decimal? Amount { get; set; }
        public string TaxType { get; set; }
        public bool? Inclusive { get; set; }
        public string Description { get; set; }
        public int TaxId { get; set; }
    }
    public class DailyRate
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class RoomRates
    {
        public decimal RoomDailyRate { get; set; }
    }
    public class HotelMarkup
    {
        public bool IsDailyServiceFee { get; set; }
        public bool IsMarkupDisplay { get; set; }
        public string MarkupType { get; set; }
        public decimal MarkupAmount { get; set; }
        public string MarkupName { get; set; }
        public decimal MarkupAmountFromConf { get; set; }
        public int PromotionType { get; set; }
        public bool UseDiscountPerPassenger { get; set; }

    }
    public class CancelPenalty
    {
        public bool CancelPenaltyNotSpecified { get; set; }

        public bool IsNonRefundable { get; set; }
        public CancelDeadLine DeadLine { get; set; }
        public AmountPercent AmountPercent { get; set; }
        public decimal CancellationFee { get; set; }
    }
    public class CancelDeadLine
    {
        public DeadlineOffsetTimeUnit OffsetTimeUnit { get; set; }
        public bool OffsetTimeUnitSpecified { get; set; }
        public int OffsetUnitMultiplier { get; set; }
        public DateTime AbsoluteDeadline { get; set; }
        public bool AbsoluteDeadlineSpecified { get; set; }
    }
    public enum DeadlineOffsetTimeUnit
    {

        Year,

        Month,

        Week,

        Day,

        Hour,

        Second,

        FullDuration,
    }
    public class AmountPercent
    {
        public double Amount { get; set; }
        public string CurrencyCode { get; set; }

        public int DecimalPlaces { get; set; }

    }
    public class Markup
    {
        public decimal Margin { get; set; }
        public bool? ApplyMarkupForEachLeg { get; set; }
        public string MarginType { get; set; }
        public bool Display { get; set; }
        public string FeeDestination { get; set; }
        public MarkupTypes MarkupType { get; set; }
        public string MarkupName { get; set; }
        public decimal MarkupAmount { get; set; }
        public string PromotionDiscountCode { get; set; }
        public bool UseDiscountPerPassenger { get; set; }
        [Obsolete]
        public bool? ExcludeInfants { get; set; }
        public List<string> paxTypes { get; set; }
        public int Id { get; set; }
    }
    public enum MarkupTypes
    {
        Discount = 1,
        Markup = 2,
        ServiceFee = 3,
        ContractDiscountMarkup = 4,
        PromotionalDiscount = 5
    }
    public class CancelPenaltyNormalized
    {
        public double Amount { get; set; }
        public bool IsNonRefundable { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime DeadlineDate { get; set; }
        public List<string> Description { get; set; }
        public string CurrencySymbol
        {
            get
            {
                var symbol = Util.GetCurrencySymbol(CurrencyCode);

                if (CurrencyCode == "UZS" || CurrencyCode == symbol)
                    return CurrencyCode + " ";
                else
                    return string.Format("{0} {1}", CurrencyCode, symbol);
            }
        }
        public string GetRefundableMessage
        {
            get
            {
                if (Description == null || Description.Count == 0)
                {
                    if (DeadlineDate != new DateTime())
                    {
                        if (IsNonRefundable)
                        {
                            if (Amount > 0)
                                return string.Format("Cancellation fee {0} not refundable.", CurrencySymbol + Amount);
                            else
                                return "Rate is not refundable.";
                        }
                        else 
                            return string.Format("Cancellation fee {0} if cancelled after {1} at {2}", CurrencySymbol + Amount, DeadlineDate.ToShortDateString(), DeadlineDate.ToString("hh:mm tt"));
                    }
                    else
                    {
                        if (Amount > 0 && IsNonRefundable)
                            return string.Format("Cancellation fee {0} not refundable.", CurrencySymbol + Amount);
                        else
                            return "No Cancelation Policy.";
                    }
                }
                else
                {
                    var response = string.Empty;
                    Description.ForEach(x => response += x + ";");
                    return response;
                }
                
            }
        }
            
    }
}

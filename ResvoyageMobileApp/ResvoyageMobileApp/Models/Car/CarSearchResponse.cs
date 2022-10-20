using ResvoyageMobileApp.Models.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace ResvoyageMobileApp.Models.Car
{
    public class CarSearchResponse
    {
        public Guid SessionId { get; set; }
        public List<CarInformation> Cars { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public LocationInfo PickupCity { get; set; }
        public LocationInfo DropOffCity { get; set; }

        public ErrorResult Error { get; set; }
    }

    public class CarInformation
    {
        public Guid Id { get; set; }
        public string CarCode { get; set; }
        public string CarName { get; set; }
        public string CarContext { get; set; }
        public VehInfo VehicleInfo { get; set; }
        public string PolicyType { get; set; }
        public DateTime PickupDate { get; set; }
        public string PickupDateString 
        {
            get
            {
                return PickupDate.ToString("dd MMM");
            }
        }
        public DateTime DropOffDate { get; set; }
        public string DropOffDateString
        {
            get
            {
                return DropOffDate.ToString("dd MMM");
            }
        }
        public LocationInfo PickupLocation { get; set; }
        public LocationInfo ReturnLocation { get; set; }
        public string PickupCity { get; set; }
        public string DropOffCity { get; set; }
        public string CarVendorLogo { get { return $"https://engines.resvoyage.com/content/v02-car-vendor-logos/{CarCode}.png"; } }
    }

    public class VehInfo
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public bool AirConditioning { get; set; }
        public string TransmissionType { get; set; }
        public string CarType { get; set; }
        public string LoyaltyCard { get; set; }
        public string VehCategory { get; set; }
        public string VehModel { get; set; }
        public string DisplayVehModel { 
            get
            {
                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;
                return string.IsNullOrEmpty(VehModel) ? "Any" : textInfo.ToTitleCase(VehModel.ToLower());
            }
        }
        public string VehClass { get; set; }
        public int Seats { get; set; }
        public string SeatsCount { 
            get
            {
                return Seats > 0 ? Seats.ToString() : null;
            } 
        }
        public string PictureURL { get; set; }
        public string VehClassCode
        {
            get
            {
                if (!string.IsNullOrEmpty(CarType))
                {
                    return CarType[0].ToString();
                }
                return string.Empty;
            }
        }
        public bool UnlimitedRate { get; set; }
        public string DistanceUnit { get; set; }
        public string PeriodUnit { get; set; }
        public VehicleCharge[] VehCharge { get; set; }
        public string RateCategory { get; set; }
        public string RateQualifier { get; set; }
        public string RatePeriod { get; set; }
        public string VendorRateID { get; set; }
        public decimal RateTotalAmount { get; set; }
        public decimal BasePrice { get; set; }
        public string DisplayTotalPrice
        {
            get
            {
                if (CurrencyCode == "UZS")
                {
                    NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
                    var tmp = Math.Round(RateTotalAmount).ToString("n", CultureInfo.CurrentCulture);

                    if (tmp.Contains(nfi.NumberDecimalSeparator))
                        return tmp.Split(nfi.NumberDecimalSeparator.ToCharArray())[0];
                    else
                        return tmp;
                }
                else
                    return RateTotalAmount.ToString("n", CultureInfo.CurrentCulture);
            }
        }
        public decimal Deposit { get; set; }
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
        public string VendorCode { get; set; }
        public string VendorValue { get; set; }
        public string VendorName { get; set; }
        public bool IsAddedToCart { get; set; }
        public CarProvider Provider { get; set; }
        public int OnRequestCarId { get; set; }
        public int OnRequestVendorId { get; set; }
        public bool IsDailyServiceFee { get; set; }
        public bool IsMarkUpDisplay { get; set; }
        public decimal MarkUp { get; set; }
        public string MarkUpType { get; set; }
        public decimal Total { get; set; }
        public decimal Daily { get; set; }
        public string CorporateDiscoutCode { get; set; }
        public string ConfirmationNumber { get; set; }
        public string MarkupName { get; set; }
        public string PolicyDisplayName { get; set; }
        public bool? MarkupsCalculated { get; set; }
        public string CarLogo { get { return $"https://engines.resvoyage.com/content/car-logos/{VendorCode}/{VendorCode}_{VehClassCode}.png"; } }

        public List<CarMarkup> Markups { get; set; }
        public CarRules CarRules { get; set; }

        public LocationInfo PickuUpCity { get; set; }
        public LocationInfo DropOffCity { get; set; }

        public bool IsSpecialCategory()
        {
            if (!string.IsNullOrEmpty(CarType) && CarType.Length > 1 && CarType[1].Equals('X'))
            {
                return true;
            }

            return false;
        }

        public bool IsOnRequest()
        {
            return Provider == CarProvider.OnRequest;
        }
        public bool IsEnjoyCuba()
        {
            return Provider == CarProvider.EnjoyCubaCars;
        }
    }

    public class CarRules
    {
        public string VehicleModel { get; set; }
        public bool UnlimitedMilage { get; set; }
        public BaseRateDescription BaseRate { get; set; }
        public BaseRateDescription TotalRate { get; set; }
        public List<CarLocationInfo> LocationInfos { get; set; }
        public CarLocationInfo PickUpLocation { get; set; }
        public CarLocationInfo DropOffLocation { get; set; }
        public string[] Comments { get; set; }
        public List<RateDescription> Fees { get; set; }
        public List<RateDescription> Extras { get; set; }
        public List<RateDescription> AdditionalEquipment { get; set; }
        public List<RateDescription> Insurance { get; set; }
        public Error Error { get; set; }
        public ErrorResult ErrorResult { get; set; }
        public string TaxesPrice
        {
            get
            {
                if (BaseRate != null && TotalRate != null)
                {
                    if (TotalRate.CurrencyCode == "UZS")
                    {
                        NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
                        var tmp = Math.Round(TotalRate.Rate - BaseRate.Rate).ToString("n", CultureInfo.CurrentCulture);

                        if (tmp.Contains(nfi.NumberDecimalSeparator))
                            return tmp.Split(nfi.NumberDecimalSeparator.ToCharArray())[0];
                        else
                            return tmp;
                    }
                    else
                        return (TotalRate.Rate - BaseRate.Rate).ToString("n", CultureInfo.CurrentCulture);
                }
                else
                    return null;
            }
        }
    }

    public class VehicleCharge
    {
        public decimal Amount { get; set; }
        public decimal Difference { get; set; }
        public string CurrencyCode { get; set; }
        public int DecimalPlaces { get; set; }
        public string Description { get; set; }
        public bool TaxInclusive { get; set; }
        public bool GuaranteedInd { get; set; }
        public bool IncludedInRate { get; set; }
        public string Purpose { get; set; }
        public decimal ServiceFee { get; set; }
    }
    public class CarMarkup
    {
        public bool IsDailyServiceFee { get; set; }
        public bool IsMarkUpDisplay { get; set; }
        public string MarkUpType { get; set; }
        public decimal MarkupAmount { get; set; }
        public decimal MarkupAmountFromConf { get; set; }
        public string MarkupName { get; set; }
        public int PromotionType { get; set; }
    }
    public class RateDescription : BaseRateDescription
    {
        public bool IsIncluded { get; set; }
        public string Description { get; set; }
    }

    public class BaseRateDescription
    {
        public string Type { get; set; }
        public decimal Rate { get; set; }
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
        public string DisplayRate
        {
            get
            {
                if (CurrencyCode == "UZS")
                {
                    NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
                    var tmp = Math.Round(Rate).ToString("n", CultureInfo.CurrentCulture);

                    if (tmp.Contains(nfi.NumberDecimalSeparator))
                        return tmp.Split(nfi.NumberDecimalSeparator.ToCharArray())[0];
                    else
                        return tmp;
                }
                else
                    return Rate.ToString("n", CultureInfo.CurrentCulture);
            }
        }
    }

    public class CarLocationInfo
    {
        public string OpeningTime { get; set; }
        public string ClosingTime { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string AddressString
        {
            get 
            {
                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;
                return string.IsNullOrEmpty(Address) ? null : textInfo.ToTitleCase(Address.ToLower());
            }
            
        }
    }
}

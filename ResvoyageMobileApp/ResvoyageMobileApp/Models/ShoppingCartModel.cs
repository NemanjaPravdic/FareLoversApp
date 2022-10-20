using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using ResvoyageMobileApp.Models.Car;
using ResvoyageMobileApp.Models.Flight;
using ResvoyageMobileApp.Models.Hotel;

namespace ResvoyageMobileApp.Models
{
    public class ShoppingCartModel
    {
        public ShoppingCartResponse ShoppingCart { get; set; }
        public Guid SessionId { get; set; }
        public ErrorResult Error { get; set; }
    }
    public class ShoppingCartResponse
    {
        public string Warning { get; set; }
        public string ReservationStatus { get; set; }
        public InsuranceInformation InsuranceInfo { get; set; }
        private List<AvailablePaymentOptions> _availablePaymentOptions = new List<AvailablePaymentOptions>();
        public PricedItinerary Air { get; set; }
        public List<PricedItinerary> Flights { get; set; }
        public List<HotelInformation> Hotels { get; set; }
        public List<CarInformation> Cars { get; set; }
        public List<PassengerInfo> Travellers { get; set; }
        public bool NeedJustification { get; set; }
        public List<BasicCreditCardInfo> PersonalCreditCards { get; set; }
        public string TravelName { get; set; }
        public string TravelReason { get; set; }
        public string UniqueTripIdentifier { get; set; }
        public string ThirdPartyBookingReferenceNumber { get; set; }
        public bool EnableTravelGuard { get; set; }
        public bool EnableBlueRibbonBags { get; set; }
        public string BookingReferenceNumber { get; set; }
        public string StripeClientSecret { get; set; }
        public bool IsShoppingCartEmpty
        {
            get
            {
                return Air == null && (Hotels == null || Hotels.Count == 0)/* && (Cars == null || Cars.Count == 0)*/;
            }
        }
        public List<AvailablePaymentOptions> AvailablePaymentOptions
        {
            get { return _availablePaymentOptions; }
            set { _availablePaymentOptions = value; }
        }
        public PaymentGatewayConfiguration PaymentGateway { get; set; }

        public decimal TotalPrice
        {
            get
            {
                decimal price = 0;
                if (Air != null && Air.AirItineraryPricingInfo != null)
                    price += Air.AirItineraryPricingInfo.TotalPrice;
                if (Hotels != null && Hotels.Count > 0)
                {
                    foreach (var hotel in Hotels)
                    {
                        if (hotel.Rooms != null)
                            price += hotel.Rooms[0].RoomRate;
                    }
                }

                if (Cars != null && Cars.Count > 0)
                {
                    foreach (var car in Cars)
                    {
                        price += car.VehicleInfo.RateTotalAmount;
                    }
                }
                return price;
            }
        }
        public string CurrencyCode
        {
            get
            {
                if (Air != null && Air.AirItineraryPricingInfo != null)
                    return Air.AirItineraryPricingInfo.CurrencyCode;
                else if (Hotels != null && Hotels.Count > 0)
                    return Hotels.FirstOrDefault().CurrencyCode;
                else if (Cars != null && Cars.Count > 0)
                    return Cars.FirstOrDefault().VehicleInfo.CurrencyCode;
                else
                    return null;
            }
        }
        public string DisplayTotalPrice
        {
            get
            {
                if (CurrencyCode != null && CurrencyCode == "UZS")
                {
                    NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
                    var tmp = Math.Round(TotalPrice).ToString("n", CultureInfo.CurrentCulture);

                    if (tmp.Contains(nfi.NumberDecimalSeparator))
                        return tmp.Split(nfi.NumberDecimalSeparator.ToCharArray())[0];
                    else
                        return tmp;
                }
                else
                    return TotalPrice.ToString("n", CultureInfo.CurrentCulture);
            }
        }
        public string TermsUrl { get; set; }
    }
    public class BasicCreditCardInfo
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
    }
    public class PaymentGatewayConfiguration
    {
        public string Type { get; set; }
        public string PaymentGatewayUserName { get; set; }
        public string PaymentGatewayPassword { get; set; }
        public string PaymentGatewayUrl { get; set; }
        public string PaymentGatewayEntityId { get; set; }
    }
    public class AvailablePaymentOptions
    {
        public AvailablePaymentOptions()
        {
            PaymentOptions = new List<PaymentOptions>();
        }

        public TravelTypes TravelType { get; set; }
        public List<PaymentOptions> PaymentOptions { get; set; }
    }
    public class InsuranceInformation
    {
        public string InsuranceVendor { get; set; }
        public decimal TotalInsuranceAmmount { get; set; }
        public string DisclaimerLink { get; set; }
        public string CertificateLink { get; set; }
        public string Error { get; set; }
    }
    public class PassengerInfo
    {
        public PassengerInfo()
        {
            Id = Guid.NewGuid();
        }                
        public Guid Id { get; private set; }              
        public string Title { get; set; }        
        public string TypeCode { get; set; }        
        public string FirstName { get; set; }        
        public string MiddleName { get; set; }        
        public string LastName { get; set; }        
        public string Email { get; set; }        
        public string CountryCode { get; set; }        
        public string Phone { get; set; }        
        public string PhoneCode { get; set; }        
        public string DriversLicence { get; set; }        
        public string NationalIdentity { get; set; }        
        public string LocallyDefinedIDNumber { get; set; }        
        public string MobilePhone { get; set; }        
        public string MobilePhoneCode { get; set; }       
        public bool? Gender { get; set; }        
        public string PassportNumber2 { get; set; }        
        public string CountryOfIssuance2 { get; set; }        
        public string CountryOfNationality2 { get; set; }        
        public DateTime? ExpirationDate2 { get; set; }        
        public DateTime? IssueDate2 { get; set; }   
        public string DateOfBirthString
        {
            get { return DateOfBirth == null ? null : DateOfBirth.Value.ToShortDateString(); }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    try
                    {
                        DateOfBirth = DateTime.Parse(value);
                    }
                    catch
                    {
                    }
                }
            }
        }
        public string PassportExpDateString2
        {
            get { return ExpirationDate2 == null ? null : ExpirationDate2.Value.ToShortDateString(); }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    try
                    {
                        ExpirationDate2 = DateTime.Parse(value);
                    }
                    catch
                    {
                    }
                }
            }
        }
        public string PassportIssuingDateString2
        {
            get { return IssueDate2 == null ? null : IssueDate2.Value.ToShortDateString(); }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    try
                    {
                        IssueDate2 = DateTime.Parse(value);
                    }
                    catch
                    {
                    }
                }
            }
        }
        public string PassportExpDateString
        {
            get { return PassportExpDate == null ? null : PassportExpDate.Value.ToShortDateString(); }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    try
                    {
                        PassportExpDate = DateTime.Parse(value);
                    }
                    catch
                    {
                    }
                }
            }
        }
        public string PassportIssuingDateString
        {
            get { return PassportIssuingDate == null ? null : PassportIssuingDate.Value.ToShortDateString(); }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    try
                    {
                        PassportIssuingDate = DateTime.Parse(value);
                    }
                    catch
                    {
                    }
                }
            }
        }        
        //public List<FligthSeatMap> FlightsSeatMap { get; set; }        
        public List<PaymentInfoPerProductWise> PaymentInfoObject { get; set; }
        
        public int? BaggagePiecesQty { get; set; }
        
        public int? ChosenCostCenterId { get; set; }
        
        public string ChosenCostCenter { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public DateTime? UsaDocumentExpDate { get; set; }
        public DateTime? CubanDocumentExpDate { get; set; }

        public DateTime? PassportExpDate { get; set; }
        public DateTime? PassportIssuingDate { get; set; }
       
        public string UserRole { get; set; }
        
        public int? EmployeeId { get; set; }
        
        public string CorporateEmpId { get; set; }
        
        public string EmployeeJobTitle { get; set; }
        
        public string EmployeeCostCenter { get; set; }
        
        public string EmployeeRecordLocator { get; set; }
        
        public string RedressNumber { get; set; }
        
        public string KnownTravellerId { get; set; }

        
        public string EmployeeIdentifier { get; set; }
        
        public string PassportNo { get; set; }

        
        public string PassportIssuingCountry { get; set; }

        
        public string CountryOfNationality { get; set; }
        
        public string PassportIssuingCountryName { get; set; }
        
        public bool VisaPurchased { get; set; }

        
        public List<string> SpecialServiceRequestCodes { get; set; }

        public bool IsPaymentOptionSelected()
        {
            if (PaymentInfoObject == null) return false;

            return PaymentInfoObject.Any(x => x.IsPaymentSelected());
        }

        
        public bool IsGuestTraveller { get; set; }

        
        public string B2CPassword { get; set; }
    }

    public class PaymentInfoPerProductWise
    {       
        public string TravelType { get; set; }        
        public PaymentCardInfo CardInfo { get; set; }        
        public string PaymentOption { get; set; }        
        public string BillingNumber { get; set; }        
        public string CorporateCreditCard { get; set; }        
        public bool AgencyCreditCard { get; set; }        
        public string CorporateCardCvv { get; set; }        
        public string Guarantee { get; set; }     
        public AddressInfo Address { get; set; }        
        public decimal TravelfusionCardChargeAmount { get; set; }
        public string BillingAddress
        {
            get { return (Address == null) ? "" : Address.ToString(); }
        }
        public bool IsCreditCardChosenAsPayment()
        {
            return CorporateCreditCard == "True" || PaymentOption == "CreditCard";
        }
        public bool IsPaymentSelected()
        {
            return CorporateCreditCard == "True" || !string.IsNullOrEmpty(PaymentOption);
        }
        public string CvvCode
        {
            get
            {
                if (IsCreditCardChosenAsPayment())
                {
                    if (CorporateCreditCard == "True")
                    {
                        return CorporateCardCvv;
                    }
                    else if (PaymentOption == "CreditCard" && CardInfo != null)
                    {
                        return CardInfo.CVV;
                    }
                }
                return "";
            }
        }

    }

    public class PaymentCardInfo
    {
        
        public int Id { get; set; }
        
        public string CardType { get; set; }
        
        public string CardTypeCode { get; set; }
        
        public string CardNumber { get; set; }
        
        public string CardToken { get; set; }
        
        public string CardholderName { get; set; }
        
        public int ExpirationMonth { get; set; }
        
        public int ExpirationYear { get; set; }
        
        public string CVV { get; set; }
        
        public string CardAddress { get; set; }
        
        public string BankName { get; set; }
        
        public string BankPhone { get; set; }
        
        public string BillingPhone { get; set; }
        
        public string CardNonce { get; set; }
        
        public string StripeToken { get; set; }
    }

    public class AddressInfo
    {
        public string CountryCode { get; set; }
        
        public string CountryName { get; set; }
        
        public string ZIP { get; set; }
        
        public string CityName { get; set; }
        public string CityNameOverrided {
            get
            {
                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;
                if (!string.IsNullOrEmpty(CityName))
                    return textInfo.ToTitleCase(CityName.ToLower());
                else
                    return null;
            }
        }

        public string StreetAddress { get; set; }
        
        public string RegionName { get; set; }
        
    }
}

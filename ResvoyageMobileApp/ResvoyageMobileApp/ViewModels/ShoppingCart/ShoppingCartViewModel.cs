using CreditCardValidator;
using Newtonsoft.Json;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Models.Car;
using ResvoyageMobileApp.Models.Hotel;
using ResvoyageMobileApp.Models.Other;
using ResvoyageMobileApp.Models.ShoppingCartModels;
using ResvoyageMobileApp.Models.User;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Services;
using ResvoyageMobileApp.Services.ShoppingCart;
using ResvoyageMobileApp.ViewModels.Car;
using ResvoyageMobileApp.ViewModels.Flight;
using ResvoyageMobileApp.ViewModels.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.ShoppingCart
{
    public class ShoppingCartViewModel : BaseViewModel
    {
        private CurrencyConversionService _currencyConversionService;
        private BookingCompleteService bookingCompleteService;
        public ShoppingCartViewModel(ShoppingCartModel shoppingCart,string travelType)
        {
            _currencyConversionService = new CurrencyConversionService();
            _currency = SelectedCurrency;
            _shoppingCart = shoppingCart.ShoppingCart;
            _sessionId = shoppingCart.SessionId;
            _travelType = travelType;
            _passengerInfo = GeneratePassengerInfo(shoppingCart.ShoppingCart);
            _paymentDetails = new PaymentDetailsViewModel();
            bookingCompleteService = new BookingCompleteService();

            if (_shoppingCart.Air != null && _shoppingCart.Air.AirItineraryPricingInfo != null)
            {
                _flightPricingInfo = new ObservableCollection<PriceFeeDetailViewModel>();
                _shoppingCart.Air.AirItineraryPricingInfo.PTC_FareBreakdowns.ForEach(x => _flightPricingInfo.Add(new PriceFeeDetailViewModel(x, _shoppingCart.Air.AirItineraryPricingInfo.CurrencyCode)));
                _currencySymbol = _shoppingCart.Air.AirItineraryPricingInfo.CurrencySymbol;
                _serverCurrency = _shoppingCart.Air.AirItineraryPricingInfo.CurrencyCode;
            }

            if (_shoppingCart.Cars != null && _shoppingCart.Cars.Count > 0)
            {
                _currencySymbol = _shoppingCart.Cars.FirstOrDefault()?.VehicleInfo.CurrencySymbol;
                _serverCurrency = _shoppingCart.Cars.FirstOrDefault()?.VehicleInfo.CurrencyCode;
                _car =  new CarShoppingCartViewModel(_shoppingCart.Cars.FirstOrDefault());
            }

            if (_shoppingCart.Hotels != null && _shoppingCart.Hotels.Count > 0)
            {
                _currencySymbol = _shoppingCart.Hotels.FirstOrDefault()?.CurrencySymbol;
                _serverCurrency = _shoppingCart.Hotels.FirstOrDefault()?.CurrencyCode;
                _hotel = new HotelShoppingCartViewModel(_shoppingCart.Hotels.FirstOrDefault());
            }

            if (_serverCurrency != Currency.CurrencyCode)
                ExchangePrices();
            
        }

        private Guid _sessionId;

        public Guid SessionId
        {
            get { return _sessionId; }
            set { SetValue(ref _sessionId, value); }
        }
        private ShoppingCartResponse _shoppingCart;

        public ShoppingCartResponse ShoppingCart
        {
            get { return _shoppingCart; }
            set { SetValue(ref _shoppingCart, value); }
        }
        private ObservableCollection<PassengerInfoViewModel> _passengerInfo;

        public ObservableCollection<PassengerInfoViewModel> PassengerInfo
        {
            get { return _passengerInfo; }
            set { SetValue(ref _passengerInfo, value); }
        }
        private ObservableCollection<PriceFeeDetailViewModel> _flightPricingInfo;

        public ObservableCollection<PriceFeeDetailViewModel> FlightPricingInfo
        {
            get { return _flightPricingInfo; }
            set { SetValue(ref _flightPricingInfo, value); }
        }
        private CarShoppingCartViewModel _car;

        public CarShoppingCartViewModel Car
        {
            get { return _car; }
            set { SetValue(ref _car, value); }
        }
        private HotelShoppingCartViewModel _hotel;

        public HotelShoppingCartViewModel Hotel
        {
            get { return _hotel; }
            set { SetValue(ref _hotel, value); }
        }
        private CurrencyInfo _currency;
        public CurrencyInfo Currency
        {
            get { return _currency; }
            set { SetValue(ref _currency, value); }
        }
        private string _serverCurrency;
        public string ServerCurrency
        {
            get { return _serverCurrency; }
            set { SetValue(ref _serverCurrency, value); }
        }
        private PaymentDetailsViewModel _paymentDetails;

        public PaymentDetailsViewModel PaymentDetails
        {
            get { return _paymentDetails; }
            set { SetValue(ref _paymentDetails, value); }
        }
        private string _currencySymbol;

        public string CurrencySymbol
        {
            get { return _currencySymbol; }
            set { SetValue(ref _currencySymbol, value); }
        }
        private string _travelType;

        public string TravelType
        {
            get { return _travelType; }
            set { SetValue(ref _travelType, value); }
        }
        private bool _showWaitScreen;

        public bool ShowWaitScreen
        {
            get { return _showWaitScreen; }
            set { SetValue(ref _showWaitScreen, value); }
        }
        private bool _showAfterBooking;

        public bool ShowAfterBooking
        {
            get { return _showAfterBooking; }
            set { SetValue(ref _showAfterBooking, value); }
        }
        private string _afterBookingTitle;

        public string AfterBookingTitle
        {
            get { return _afterBookingTitle; }
            set { SetValue(ref _afterBookingTitle, value); }
        }
        private string _afterBookingText;

        public string AfterBookingText
        {
            get { return _afterBookingText; }
            set { SetValue(ref _afterBookingText, value); }
        }
        public List<int> ExpirationMonths
        {
            get 
            {
                var list = new List<int>();
                for (int i = 1; i <= 12; i++)
                {
                    list.Add(i);
                }
                return list;
            }
        }
        public List<int> ExpirationYears
        {
            get
            {
                var list = new List<int>();
                var year = DateTime.Now.Year;
                for (int i = 0; i <= 14; i++)
                {
                    list.Add(year+i);
                }
                return list;
            }
        }
        public string AirTicketText
        {
            get
            {
                if (ShoppingCart != null && ShoppingCart.Air != null && ShoppingCart.Air.AirItineraryPricingInfo != null)
                {
                    var text = string.Empty;
                    var tmp = 0;
                    if (ShoppingCart.Air.AirItineraryPricingInfo.PTC_FareBreakdowns.Any(x => x.PassengerType == "ADT"))
                    {
                        var adultObj = ShoppingCart.Air.AirItineraryPricingInfo.PTC_FareBreakdowns.FirstOrDefault(x => x.PassengerType == "ADT");
                        if (adultObj != null)
                        { 
                            text += string.Format("{0} {1}", adultObj.PassengerCount, adultObj.PassengerCount > 1 ? AppResources.SF_ADULTS : AppResources.SF_ADULT);
                            tmp++;
                        }
                    }
                    if (ShoppingCart.Air.AirItineraryPricingInfo.PTC_FareBreakdowns.Any(x => x.PassengerType == "CHD"))
                    {
                        var childObj = ShoppingCart.Air.AirItineraryPricingInfo.PTC_FareBreakdowns.FirstOrDefault(x => x.PassengerType == "CHD");
                        if (childObj != null)
                        {
                            text += tmp > 0 ? ", " : "";
                            text += string.Format("{0} {1}", childObj.PassengerCount, childObj.PassengerCount > 1 ? AppResources.SF_CHILDREN : AppResources.SF_CHILD);
                            tmp++;
                        }
                    }
                    if (ShoppingCart.Air.AirItineraryPricingInfo.PTC_FareBreakdowns.Any(x => x.PassengerType == "INF"))
                    {
                        var infantObj = ShoppingCart.Air.AirItineraryPricingInfo.PTC_FareBreakdowns.FirstOrDefault(x => x.PassengerType == "INF");
                        if (infantObj != null)
                        {
                            text += tmp > 0 ? ", " : "";
                            text += string.Format("{0} {1}", infantObj.PassengerCount, infantObj.PassengerCount > 1 ? AppResources.SF_INFANTS : AppResources.SF_INFANT);
                            tmp++;
                        }
                    }


                    return string.Format("{0} {1}: {2}", ShoppingCart.Air.AirItineraryPricingInfo.PTC_FareBreakdowns.Sum(x => x.PassengerCount), ShoppingCart.Air.AirItineraryPricingInfo.PTC_FareBreakdowns.Count > 1 ? AppResources.SC_TICKETS : AppResources.SC_TICKET, text);
                }
                else
                    return null;
            }
        }
        public  string PayText
        {
            get
            {
                if (ShoppingCart != null && ShoppingCart.TotalPrice != 0)
                    return string.Format("{0} {1}{2}", AppResources.SC_PAY, CurrencySymbol, ShoppingCart.DisplayTotalPrice);
                else
                    return null;
            }
        }
        public ICommand Pay => new Command(BookingComplete);
        private ObservableCollection<PassengerInfoViewModel> GeneratePassengerInfo(ShoppingCartResponse shoppingCart)
        {
            if (shoppingCart != null && shoppingCart.Travellers != null && shoppingCart.Travellers.Count > 0)
            {
                var response = new ObservableCollection<PassengerInfoViewModel>();
                int i = 1;
                int adultId = 1;
                int childId = 1;
                int infantId = 1;
                foreach (var traveller in shoppingCart.Travellers)
                {
                    var passenger = new PassengerInfoViewModel { Id = i, IsChild = traveller.TypeCode != null && traveller.TypeCode != "ADT", TypeCode = traveller.TypeCode };
                    if (traveller.TypeCode == "ADT")
                    {
                        passenger.AdultId = adultId;
                        adultId++;
                    }
                    else if (traveller.TypeCode == "CHD")
                    {
                        passenger.ChildId = childId;
                        childId++;
                    }
                    else if (traveller.TypeCode == "INF")
                    {
                        passenger.InfantId = infantId;
                        infantId++;
                    }
                    response.Add(passenger);
                    i++;
                }
                if (Application.Current.Properties.ContainsKey("UserInfo") && response.FirstOrDefault(x => x.AdultId == 1) != null)
                {
                    var userInfoJson = Application.Current.Properties["UserInfo"].ToString();
                    var userInfo = JsonConvert.DeserializeObject<UserDetails>(userInfoJson);

                    var user = response.FirstOrDefault(x => x.AdultId == 1);
                    user.Title = userInfo.Title;
                    user.FirstName = userInfo.FirstName;
                    user.MiddleName = userInfo.MiddleName;
                    user.LastName = userInfo.LastName;
                    user.Day = userInfo.DateOfBirth.HasValue ? userInfo.DateOfBirth.Value.Day.ToString() : null;
                    user.Month = userInfo.DateOfBirth.HasValue ? userInfo.DateOfBirth.Value.Month.ToString() : null;
                    user.Year = userInfo.DateOfBirth.HasValue ? userInfo.DateOfBirth.Value.Year.ToString() : null;
                    user.Phone = userInfo.PhoneNumber;
                    user.Email = userInfo.EmailAddress;
                    user.Gender = userInfo.Gender;
                    //user.EmployeeId = userInfo.Id;
                }


                return response;
            }
            else
                return null;
        }
        private async void BookingComplete()
        {
            try
            {
                ShowWaitScreen = true;
                if (ValidateRequest())
                {
                    var response = await bookingCompleteService.BookAsync(GenerateRequest());
                    ShowWaitScreen = false;
                    if (response != null && response.ErrorResult != null && response.ErrorResult.ErrorMessage != null)
                    {
                        AfterBookingTitle = AppResources.APP_ERROR;
                        AfterBookingText = response.ErrorResult.ErrorMessage;
                    }
                    else if (response != null && response.IsSuccessful)
                    {
                        AfterBookingTitle = AppResources.APP_SUCCESS;
                        AfterBookingText = string.Format("{0} {1}", AppResources.SC_BOOKING_COMPLETE_TEXT, response.ReferenceNumber);
                    }
                    else
                    {
                        AfterBookingTitle = AppResources.APP_ERROR;
                        AfterBookingText = AppResources.ER_SOME_ERROR_OCCURED;
                    }
                    ShowAfterBooking = true;
                }                

            }
            catch (Exception e)
            {
                ShowWaitScreen = false;
                await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, e.Message, AppResources.APP_OK);
            }
            ShowWaitScreen = false;
        }

        private BookingCompleteRequest GenerateRequest()
        {
            var request = new BookingCompleteRequest();
            request.Travellers = new List<PassengerInfo>();
            request.SessionId = _sessionId.ToString();
            foreach (var passenger in _passengerInfo)
            {
                var traveller = new PassengerInfo
                {
                    Email = passenger.Email,
                    DateOfBirthString = string.Format("{0}-{1}-{2}", passenger.Year, passenger.Month, passenger.Day),
                    FirstName = passenger.FirstName,
                    MiddleName = passenger.MiddleName,
                    LastName = passenger.LastName,
                    Phone = passenger.Phone,
                    Title = passenger.Title,
                    Gender = passenger.Gender == "Male",
                    TypeCode = passenger.TypeCode,
                    //EmployeeId = passenger.EmployeeId
                };
                request.Travellers.Add(traveller);
            }
            request.PaymentDetails = new List<PaymentInfoPerProductWise>();

            var payment = new PaymentInfoPerProductWise { 
                TravelType= _travelType,
                PaymentOption = "CreditCard",
                Address = new AddressInfo { 
                    CityName = _paymentDetails.AddressInfo.CityName,
                    CountryCode = _paymentDetails.AddressInfo.CountryName,
                    CountryName = _paymentDetails.AddressInfo.CountryName,
                    RegionName = _paymentDetails.AddressInfo.RegionName,
                    StreetAddress = _paymentDetails.AddressInfo.StreetAddress,
                    ZIP = _paymentDetails.AddressInfo.ZIP
                },
                CardInfo = new PaymentCardInfo { 
                    CardholderName = _paymentDetails.CardInfo.CardholderName,
                    CardType = GetCardType(),
                    CardTypeCode = GetCardType(),
                    ExpirationMonth = _paymentDetails.CardInfo.ExpirationMonth,
                    ExpirationYear = _paymentDetails.CardInfo.ExpirationYear,
                    CVV = _paymentDetails.CardInfo.CVV,
                    CardNumber = _paymentDetails.CardInfo.CardNumber                    
                }
            };
            request.PaymentDetails.Add(payment);

            return request;
        }

        private bool ValidateRequest()
        {
            foreach (var passenger in _passengerInfo)
            {
                if (string.IsNullOrEmpty(passenger.Title))
                {
                    Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_VALIDATION_TITLE + " " + passenger.TravelerInfo, AppResources.APP_OK);
                    return false;
                }
                else if (string.IsNullOrEmpty(passenger.FirstName))
                {
                    Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_VALIDATION_FIRST_NAME + " " + passenger.TravelerInfo, AppResources.APP_OK);
                    return false;
                }
                else if (string.IsNullOrEmpty(passenger.LastName))
                {
                    Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_VALIDATION_LAST_NAME + " " + passenger.TravelerInfo, AppResources.APP_OK);
                    return false;
                }
                else if (string.IsNullOrEmpty(passenger.Day))
                {
                    Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_VALIDATION_DAY + " " + passenger.TravelerInfo, AppResources.APP_OK);
                    return false;
                }
                else if (string.IsNullOrEmpty(passenger.Month))
                {
                    Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_VALIDATION_MONTH + " " + passenger.TravelerInfo, AppResources.APP_OK);
                    return false;
                }
                else if (string.IsNullOrEmpty(passenger.Year))
                {
                    Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_VALIDATION_YEAR + " " + passenger.TravelerInfo, AppResources.APP_OK);
                    return false;
                }
                else if (string.IsNullOrEmpty(passenger.Gender))
                {
                    Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_VALIDATION_GENDER + " " + passenger.TravelerInfo, AppResources.APP_OK);
                    return false;
                }
                else if (string.IsNullOrEmpty(passenger.Email) && !passenger.IsChild)
                {
                    Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_VALIDATION_EMAIL + " " + passenger.TravelerInfo, AppResources.APP_OK);
                    return false;
                }
                else if (string.IsNullOrEmpty(passenger.Phone) && !passenger.IsChild)
                {
                    Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_VALIDATION_PHONE + " " + passenger.TravelerInfo, AppResources.APP_OK);
                    return false;
                }
            }
            if (string.IsNullOrEmpty(_paymentDetails.CardInfo.CardholderName))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_VALIDATION_CARDHOLDER_NAME, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(_paymentDetails.CardInfo.CardNumber))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_VALIDATION_CARD_NUMBER, AppResources.APP_OK);
                return false;
            }
            else if (_paymentDetails.CardInfo.ExpirationMonth == 0)
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_VALIDATION_CARD_MONTH, AppResources.APP_OK);
                return false;
            }
            else if (_paymentDetails.CardInfo.ExpirationYear == 0)
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_VALIDATION_CARD_YEAR, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(_paymentDetails.CardInfo.CVV))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_VALIDATION_CARD_CVV, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(_paymentDetails.AddressInfo.CountryName))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_VALIDATION_COUNTRY, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(_paymentDetails.AddressInfo.CityName))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_VALIDATION_CITY, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(_paymentDetails.AddressInfo.RegionName))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_VALIDATION_REGION, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(_paymentDetails.AddressInfo.ZIP))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_VALIDATION_ZIP, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(_paymentDetails.AddressInfo.StreetAddress))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_VALIDATION_STREET, AppResources.APP_OK);
                return false;
            }
            return true;
        }

        private string GetCardType()
        {
            if (PaymentDetails != null && PaymentDetails.CardInfo != null && PaymentDetails.CardInfo.CardNumber != null)
            {
                CreditCardDetector detector = new CreditCardDetector(PaymentDetails.CardInfo.CardNumber);
                if (detector.IsValid())
                {
                    switch (detector.Brand)
                    {
                        case CardIssuer.AmericanExpress:
                            return "AX";
                        case CardIssuer.MasterCard:
                            return "MC";
                        case CardIssuer.Visa:
                            return "VI";
                        case CardIssuer.Discover:
                            return "DS";
                        case CardIssuer.DinersClub:
                            return "DN";
                    }
                }
            }
            return null;
        }
        private async void ExchangePrices()
        {
            var fromCurrency = ServerCurrency;
            var rate = await _currencyConversionService.CurrencyConversion(fromCurrency, Currency.CurrencyCode);

            if (FlightPricingInfo != null && FlightPricingInfo.Count > 0)
            {
                var tmp = FlightPricingInfo;

                foreach (var flight in tmp)
                {
                    flight.CurrencyCode = Currency.CurrencyCode;
                    flight.BasePrice = flight.BasePrice * rate;
                    flight.Markup = flight.Markup * rate;
                    flight.Discount = flight.Discount * rate;
                    flight.Tax = flight.Tax * rate;
                    flight.PromotionalDiscount = flight.PromotionalDiscount * rate;
                    flight.DiscountAmountFromContract = flight.DiscountAmountFromContract * rate;

                }
                FlightPricingInfo = new ObservableCollection<PriceFeeDetailViewModel>(tmp);
            }

            if (Hotel != null && Hotel.SelectedHotel != null)
            { 
                var tmp = Hotel.SelectedHotel;
                tmp.DailyRatePerRoom = tmp.DailyRatePerRoom * rate;
                tmp.CurrencyCode = Currency.CurrencyCode;
                if (tmp.Rooms != null && tmp.Rooms.Count > 0)
                {
                    foreach (var room in tmp.Rooms)
                    {
                        room.CurrencyCode = tmp.CurrencyCode;
                        room.RoomRate = room.RoomRate * rate;

                        if (room.CancellationPenalties != null && room.CancellationPenalties.Count > 0)
                        {
                            foreach (var penalty in room.CancellationPenalties)
                            {
                                penalty.CurrencyCode = Currency.CurrencyCode;
                                penalty.Amount = penalty.Amount * Double.Parse(rate.ToString());
                            }
                        }
                    }
                
                }

                Hotel.SelectedHotel = tmp;
            }

            if (Car != null && Car.SelectedCar != null)
            { 
                var tmp = Car.SelectedCar;

                tmp.VehicleInfo.BasePrice = tmp.VehicleInfo.BasePrice * rate;
                tmp.VehicleInfo.RateTotalAmount = tmp.VehicleInfo.RateTotalAmount * rate;
                tmp.VehicleInfo.CurrencyCode = Currency.CurrencyCode;

                if (tmp.VehicleInfo.CarRules != null)
                {
                    if (tmp.VehicleInfo.CarRules.BaseRate != null)
                    { 
                        tmp.VehicleInfo.CarRules.BaseRate.CurrencyCode = Currency.CurrencyCode;
                        tmp.VehicleInfo.CarRules.BaseRate.Rate = tmp.VehicleInfo.CarRules.BaseRate.Rate * rate;
                    }
                    if (tmp.VehicleInfo.CarRules.TotalRate != null)
                    {
                        tmp.VehicleInfo.CarRules.TotalRate.CurrencyCode = Currency.CurrencyCode;
                        tmp.VehicleInfo.CarRules.TotalRate.Rate = tmp.VehicleInfo.CarRules.TotalRate.Rate * rate;
                    }
                    if (tmp.VehicleInfo.CarRules.Fees != null && tmp.VehicleInfo.CarRules.Fees.Count > 0)
                    {
                        foreach (var fee in tmp.VehicleInfo.CarRules.Fees)
                        {
                            fee.CurrencyCode = Currency.CurrencyCode;
                            fee.Rate = fee.Rate * rate;
                        }
                    }
                    if (tmp.VehicleInfo.CarRules.Extras != null && tmp.VehicleInfo.CarRules.Extras.Count > 0)
                    {
                        foreach (var extra in tmp.VehicleInfo.CarRules.Extras)
                        {
                            extra.CurrencyCode = Currency.CurrencyCode;
                            extra.Rate = extra.Rate * rate;
                        }
                    }
                    if (tmp.VehicleInfo.CarRules.Insurance != null && tmp.VehicleInfo.CarRules.Insurance.Count > 0)
                    {
                        foreach (var insurance in tmp.VehicleInfo.CarRules.Insurance)
                        {
                            insurance.CurrencyCode = Currency.CurrencyCode;
                            insurance.Rate = insurance.Rate * rate;
                        }
                    }
                    if (tmp.VehicleInfo.CarRules.AdditionalEquipment != null && tmp.VehicleInfo.CarRules.AdditionalEquipment.Count > 0)
                    {
                        foreach (var additionalEquipment in tmp.VehicleInfo.CarRules.AdditionalEquipment)
                        {
                            additionalEquipment.CurrencyCode = Currency.CurrencyCode;
                            additionalEquipment.Rate = additionalEquipment.Rate * rate;
                        }
                    }
                }

                Car.SelectedCar = tmp;

            }

            if (ShoppingCart != null)
            {
                var symbol = Util.GetCurrencySymbol(Currency.CurrencyCode);

                if (Currency.CurrencyCode == "UZS" || Currency.CurrencyCode == symbol)
                    CurrencySymbol = Currency.CurrencyCode + " ";
                else
                    CurrencySymbol = string.Format("{0} {1}", Currency.CurrencyCode, symbol);

                if (ShoppingCart.Air != null && ShoppingCart.Air.AirItineraryPricingInfo != null)
                {
                    ShoppingCart.Air.AirItineraryPricingInfo.TotalPrice = ShoppingCart.Air.AirItineraryPricingInfo.TotalPrice * rate;
                    ShoppingCart.Air.AirItineraryPricingInfo.CurrencyCode = Currency.CurrencyCode;
                    //ShoppingCart = tmp;
                }
            }
        }
    }
}

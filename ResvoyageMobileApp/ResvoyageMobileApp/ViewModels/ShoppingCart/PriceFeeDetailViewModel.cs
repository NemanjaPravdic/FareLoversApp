using ResvoyageMobileApp.Models.Flight;
using ResvoyageMobileApp.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.ShoppingCart
{
    public class PriceFeeDetailViewModel : BaseViewModel
    {
        public PriceFeeDetailViewModel(PriceFeeDetail priceFeeDetail, string currencyCode)
        {
            _baggages = priceFeeDetail.Baggages;
            _basePrice = priceFeeDetail.BasePrice;
            _markup = priceFeeDetail.Markup;
            _discount = priceFeeDetail.Discount;
            _tax = priceFeeDetail.Tax;
            _discountAmountFromContract = priceFeeDetail.DiscountAmountFromContract;
            _promotionalDiscount = priceFeeDetail.PromotionalDiscount;
            _passengerType = priceFeeDetail.PassengerType;
            _passengerCount = priceFeeDetail.PassengerCount;
            CurrencyCode = currencyCode;
        }
        private List<BaggageInfo> _baggages;

        public List<BaggageInfo> Baggages
        {
            get { return _baggages; }
            set { SetValue(ref _baggages, value); }
        }
        private decimal _basePrice;

        public decimal BasePrice
        {
            get { return _basePrice; }
            set { SetValue(ref _basePrice, value); }
        }
        private decimal _markup;

        public decimal Markup
        {
            get { return _markup; }
            set { SetValue(ref _markup, value); }
        }
        private decimal _discount;

        public decimal Discount
        {
            get { return _discount; }
            set { SetValue(ref _discount, value); }
        }
        private decimal _tax;

        public decimal Tax
        {
            get { return _tax; }
            set { SetValue(ref _tax, value); }
        }
        private decimal _discountAmountFromContract;

        public decimal DiscountAmountFromContract
        {
            get { return _discountAmountFromContract; }
            set { SetValue(ref _discountAmountFromContract, value); }
        }
        private decimal _promotionalDiscount;

        public decimal PromotionalDiscount
        {
            get { return _promotionalDiscount; }
            set { SetValue(ref _promotionalDiscount, value); }
        }
        private string _passengerType;

        public string PassengerType
        {
            get { return _passengerType; }
            set { SetValue(ref _passengerType, value); }
        }
        private int _passengerCount;

        public int PassengerCount
        {
            get { return _passengerCount; }
            set { SetValue(ref _passengerCount, value); }
        }
        public decimal TotalPrice
        {
            get { return (BasePrice + Markup + Discount + Tax + PromotionalDiscount + DiscountAmountFromContract); }
        }
        public string CurrencyCode { get; set; }
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
        public string DisplayFlightPrice
        {
            get
            {
                if (CurrencyCode != null && CurrencyCode == "UZS")
                {
                    NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
                    var tmp = Math.Round(BasePrice).ToString("n", CultureInfo.CurrentCulture);

                    if (tmp.Contains(nfi.NumberDecimalSeparator))
                        return tmp.Split(nfi.NumberDecimalSeparator.ToCharArray())[0];
                    else
                        return tmp;
                }
                else
                    return BasePrice.ToString("n", CultureInfo.CurrentCulture);
            }
        }
        public string DisplayTaxPrice
        {
            get
            {
                if (CurrencyCode != null && CurrencyCode == "UZS")
                {
                    NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
                    var tmp = Math.Round(TotalPrice - BasePrice).ToString("n", CultureInfo.CurrentCulture);

                    if (tmp.Contains(nfi.NumberDecimalSeparator))
                        return tmp.Split(nfi.NumberDecimalSeparator.ToCharArray())[0];
                    else
                        return tmp;
                }
                else
                    return (TotalPrice - BasePrice).ToString("n", CultureInfo.CurrentCulture);
            }
        }
        private bool _pricingInfoVisibility;

        public bool PricingInfoVisibility
        {
            get { return _pricingInfoVisibility; }
            set { SetValue(ref _pricingInfoVisibility, value); }
        }
        public string PassengerText
        {
            get
            {
                var type = string.Empty;
                if (PassengerType == "ADT")
                    type = AppResources.SF_ADULT;
                else if (PassengerType == "CHD")
                    type = AppResources.SF_CHILD;
                else if (PassengerType == "INF")
                    type = AppResources.SF_INFANT;
                return string.Format("{0} x {1}", PassengerCount, type);
            }
        }

        public ICommand ShowPricingInfo => new Command(ShowPrices);

        private void ShowPrices()
        {
            PricingInfoVisibility = !PricingInfoVisibility;
        }
    }
}

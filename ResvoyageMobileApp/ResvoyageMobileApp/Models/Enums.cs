using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models
{
    public enum CabinType
    {
        Economy,
        Business,
        First,
        Premium
    }
    public enum SearchType
    {
        OneWay,
        RoundTrip,
        MultiCity
    }
    public enum PaymentOptions
    {
        CreditCard,
        CorpCreditCard,
        CallMe,
        SID,
        ProfileCardType,
        Check,
        Cash,
        B2BCallMe,
        B2BInvoiceCorporation,
        WizardNumber,
        CarVendorBillingNumber,
    }
    public enum TravelTypes
    {
        AIR,
        CAR,
        HOTEL,
        AIRWebBooking
    }
    public enum CarProvider
    {
        GDS,
        OnRequest,
        EnjoyCubaCars
    }

    public enum GenderType
    { 
        Male,
        Female
    }
}

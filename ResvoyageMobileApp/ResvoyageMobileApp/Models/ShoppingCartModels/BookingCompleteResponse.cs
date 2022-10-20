using ResvoyageMobileApp.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.ShoppingCartModels
{
    public class BookingCompleteResponse
    {
        public string ReferenceNumber { get; set; }
        public List<AirlineBookingReference> AirlineBookingReferences { get; set; }
        public bool IsSuccessful { get; set; }
        public bool InsurancePurchased { get; set; }
        public Error Error { get; set; }
        public ErrorResult ErrorResult { get; set; }
        public BookWarning Warning { get; set; }
        public BlueRibbonPurchaseResponse BlueRibbonPurchaseResponse { get; set; }
    }
    public class BookWarning
    {
        public string Type { get; set; }
        public string Message { get; set; }
    }
    public class AirlineBookingReference
    {
        public string FlightCode { get; set; }
        public string ConfirmationNumber { get; set; }
    }
    public class BlueRibbonPurchaseResponse
    {
        public BlueRibbonServiceData Data { get; set; }
        public List<BlueRibbonErrors> Errors { get; set; }
        public bool Status { get; set; }
        public string StatusCode { get; set; }
    }

    public class BlueRibbonErrors
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetail { get; set; }
        public int ErrorLevel { get; set; }
    }

    public class BlueRibbonServiceData
    {
        public string ServiceNumber { get; set; }
        public double TotalPrice { get; set; }
    }
}

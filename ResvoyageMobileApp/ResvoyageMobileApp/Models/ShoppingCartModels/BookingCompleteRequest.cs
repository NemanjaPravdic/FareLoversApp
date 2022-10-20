using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.ShoppingCartModels
{
    public class BookingCompleteRequest
    {
        public string SessionId { get; set; }
        public List<PassengerInfo> Travellers { get; set; }
        public List<PaymentInfoPerProductWise> PaymentDetails { get; set; }
        public string NotesForAgent { get; set; }
        public string BaseUrl { get; set; }
        public bool PurchaseInsurance { get; set; }
        public bool PurchaseBlueRibbonBags { get; set; }
        public string CubaTravelReasonCode { get; set; }
        public List<string> CarSpecialEquipment { get; set; }
        public decimal VisaAmount { get; set; }
        public decimal TransportationPrice { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.ViewModels.ShoppingCart
{
    public class PaymentDetailsViewModel : BaseViewModel
    {
		public PaymentDetailsViewModel()
		{
			_addressInfo = new AddressViewModel();
			_cardInfo = new CardInfoViewModel();
		}
		private string _travelType;

		public string TravelType
		{
			get { return _travelType; }
			set { SetValue(ref _travelType, value); }
		}
		private string _paymentOption;

		public string PaymentOption
		{
			get { return _paymentOption; }
			set { SetValue(ref _paymentOption, value); }
		}

		private AddressViewModel _addressInfo;

		public AddressViewModel AddressInfo
		{
			get { return _addressInfo; }
			set { SetValue(ref _addressInfo, value); }
		}
		private CardInfoViewModel _cardInfo;

		public CardInfoViewModel CardInfo
		{
			get { return _cardInfo; }
			set { SetValue(ref _cardInfo, value); }
		}

	}
}

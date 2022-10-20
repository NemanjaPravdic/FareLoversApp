using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.ViewModels.ShoppingCart
{
    public class CardInfoViewModel : BaseViewModel
    {
		private string _cardholderName;

		public string CardholderName
		{
			get { return _cardholderName; }
			set { SetValue(ref _cardholderName, value); }
		}
		private string _cardType;

		public string CardType
		{
			get { return _cardType; }
			set { SetValue(ref _cardType, value); }
		}
		private string _cardTypeCode;

		public string CardTypeCode
		{
			get { return _cardTypeCode; }
			set { SetValue(ref _cardTypeCode, value); }
		}
		private string _cardNumber;

		public string CardNumber
		{
			get { return _cardNumber; }
			set { SetValue(ref _cardNumber, value); }
		}
		private int _expirationMonth;

		public int ExpirationMonth
		{
			get { return _expirationMonth; }
			set { SetValue(ref _expirationMonth, value); }
		}
		private int _expirationYear;

		public int ExpirationYear
		{
			get { return _expirationYear; }
			set { SetValue(ref _expirationYear, value); }
		}
		private string _cvv;

		public string CVV
		{
			get { return _cvv; }
			set { SetValue(ref _cvv, value); }
		}

	}
}

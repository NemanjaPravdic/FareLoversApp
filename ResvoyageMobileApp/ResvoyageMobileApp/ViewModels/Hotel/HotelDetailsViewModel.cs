using ResvoyageMobileApp.Models.Hotel;
using ResvoyageMobileApp.Models.Other;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Services;
using ResvoyageMobileApp.Services.Hotel;
using ResvoyageMobileApp.Views.Flight;
using ResvoyageMobileApp.Views.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ResvoyageMobileApp.ViewModels.Hotel
{
    public class HotelDetailsViewModel : BaseViewModel
    {
		private AddHotelToCart _addHotelToCartService;
		private CurrencyConversionService _currencyConversionService;
		public HotelDetailsViewModel(Guid hotelId, Guid sessionId, HotelRequestViewModel request, HotelInformation hotelInformation)
		{
			_hotelId = hotelId;
			_sessionId = sessionId;
			_request = request;
			_addHotelToCartService = new AddHotelToCart();
			if (hotelInformation != null)
			{
				_currencyConversionService = new CurrencyConversionService();
				_currency = SelectedCurrency;
				_hotelInformation = hotelInformation;

				if(_hotelInformation.Rooms != null && _hotelInformation.Rooms.Count > 0)
					_hotelInformation.Rooms.ForEach(x => x.CurrencyCode = _hotelInformation.CurrencyCode);

				if (!string.IsNullOrEmpty(_hotelInformation.Latitude) && !string.IsNullOrEmpty(_hotelInformation.Longitude))
				{
					var longitude = decimal.Parse(_hotelInformation.Longitude, CultureInfo.InvariantCulture);
					var latitude = decimal.Parse(_hotelInformation.Latitude, CultureInfo.InvariantCulture);
					Position position = new Position((double)latitude, (double)longitude);
					MapSpan mapSpan = new MapSpan(position, 0.01, 0.01);

					Map = new Map(mapSpan);
					Map.HeightRequest = 200;
					Pin pin = new Pin
					{
						Label = _hotelInformation.HotelName,
						Address = _hotelInformation.HotelAddress?.StreetAddress,
						Type = PinType.Place,
						Position = position
					};
					Map.Pins.Add(pin);
				}

				if (Currency.CurrencyCode != HotelInformation.CurrencyCode)
					ExchangePrices();


			}
		}
		private Guid _hotelId;
		public Guid HotelId
		{
			get { return _hotelId; }
			set { SetValue(ref _hotelId, value); }
		}
		private Guid _sessionId;
		public Guid SessionId
		{
			get { return _sessionId; }
			set { SetValue(ref _sessionId, value); }
		}
		private HotelRequestViewModel _request;
		public HotelRequestViewModel Request
		{
			get { return _request; }
			set { SetValue(ref _request, value); }
		}
		private HotelInformation _hotelInformation;
		public HotelInformation HotelInformation
		{
			get { return _hotelInformation; }
			set { SetValue(ref _hotelInformation, value); }
		}
		public Map Map { get; private set; }
		private bool _showWaitScreen;

		public bool ShowWaitScreen
		{
			get { return _showWaitScreen; }
			set { SetValue(ref _showWaitScreen, value); }
		}
		public string RequestTextInfo
		{
			get
			{
				return string.Format("{0} - {1}", _request.CheckInDateString, _request.CheckOutDateString);
			}
		}
		public string RoomRateText
		{
			get
			{
				return string.Format("{0} {1} {2} {3} {4} {5}", AppResources.RD_TOTAL_FARE_FOR, _request.NumNights, _request.NumNights == 1 ? AppResources.RD_NIGHT : AppResources.RD_NIGHTS,
					AppResources.APP_FOR, _request.NoOfRooms, _request.NoOfRooms == 1 ? AppResources.HS_ROOM : AppResources.HS_ROOMS);
			}
		}
		private CurrencyInfo _currency;
		public CurrencyInfo Currency
		{
			get { return _currency; }
			set{ SetValue(ref _currency, value);}
		}
		public ICommand AddToShoppingCart => new Command<RoomInfo>(AddRoomToShoppingCart);

		private async void AddRoomToShoppingCart(RoomInfo obj)
		{
			try
			{
				ShowWaitScreen = true;
				var response = await _addHotelToCartService.AddToCartAsync(_sessionId, obj, _request);
				if (response != null && response.ShoppingCart != null && response.ShoppingCart.Hotels != null)
				{
					var page = new ShoppingCartPage(response, "HOTEL");
					var navigation = Application.Current.MainPage as Shell;
					await navigation.Navigation.PushAsync(page, true);
					ShowWaitScreen = false;
				}
				else if (response.Error != null && response.Error.ErrorMessage != null)
				{
					ShowWaitScreen = false;
					await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, response.Error.ErrorMessage, AppResources.APP_OK);
				}
				else
				{
					ShowWaitScreen = false;
					await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.ER_SOME_ERROR_OCCURED, AppResources.APP_OK);
				}
			}
			catch (Exception e)
			{
				await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, e.Message, AppResources.APP_OK);
			}
		}
		private async void ExchangePrices()
		{
			if (HotelInformation != null)
			{
				var fromCurrency = HotelInformation.CurrencyCode;
				var rate = await _currencyConversionService.CurrencyConversion(fromCurrency, Currency.CurrencyCode);

				var tmp = HotelInformation;

				tmp.CurrencyCode = Currency.CurrencyCode;
				tmp.DailyRatePerRoom = tmp.DailyRatePerRoom * rate;

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
			}
		}

	}
}

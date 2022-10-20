using ResvoyageMobileApp.Models.Car;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Services.Car;
using ResvoyageMobileApp.Views.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Car
{
    public class CarDetailsViewModel : BaseViewModel
    {
		private AddCarToShoppingCartService _addCarToShoppingCartService;
		public CarDetailsViewModel(Guid carId, CarRequestViewModel request,Guid sessionId, CarInformation carInfo)
		{
			_carId = carId;
			_sessionId = sessionId;
			_request = request;
			_carInformation = carInfo;
			_showWaitScreen = false;
			_addCarToShoppingCartService = new AddCarToShoppingCartService();
		}
		private Guid _carId;
		public Guid CarId
		{
			get { return _carId; }
			set { SetValue(ref _carId, value); }
		}
		private Guid _sessionId;
		public Guid SessionId
		{
			get { return _sessionId; }
			set { SetValue(ref _sessionId, value); }
		}
		private CarRequestViewModel _request;
		public CarRequestViewModel Request
		{
			get { return _request; }
			set { SetValue(ref _request, value); }
		}
		private CarInformation _carInformation;
		public CarInformation CarInformation
		{
			get { return _carInformation; }
			set { SetValue(ref _carInformation, value); }
		}
		private bool _showWaitScreen;
		public bool ShowWaitScreen
		{
			get { return _showWaitScreen; }
			set { SetValue(ref _showWaitScreen, value); }
		}

		public ICommand AddToCart => new Command(AddCarToShoppingCart);

		private async void AddCarToShoppingCart()
		{
			try
			{
				ShowWaitScreen = true;
				var response = await _addCarToShoppingCartService.AddToCartAsync(_carId, _sessionId);
				if (response != null && response.ShoppingCart != null && response.ShoppingCart.Hotels != null)
				{
					var page = new ShoppingCartPage(response, "CAR");
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
	}
}

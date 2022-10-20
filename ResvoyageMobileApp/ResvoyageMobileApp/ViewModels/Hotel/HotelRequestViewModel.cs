using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ResvoyageMobileApp.ViewModels.Hotel
{
    public class HotelRequestViewModel : BaseViewModel
    {
		public HotelRequestViewModel()
		{
			_roomsInfo = new ObservableCollection<RoomViewModel> { new RoomViewModel() };
		}
		private string _placeId;

		public string PlaceId
		{
			get { return _placeId; }
			set { SetValue(ref _placeId, value); }
		}
		private string _hotelCityCode;

		public string HotelCityCode
		{
			get { return _hotelCityCode; }
			set { SetValue(ref _hotelCityCode, value); }
		}
		private string _cityName;

		public string CityName
		{
			get { return _cityName; }
			set { SetValue(ref _cityName, value); }
		}
		private string _checkInDate;

		public string CheckInDate
		{
			get { return _checkInDate; }
			set { SetValue(ref _checkInDate, value); }
		}
		private string _checkInDateString;

		public string CheckInDateString
		{
			get { return _checkInDateString; }
			set { SetValue(ref _checkInDateString, value); }
		}
		private string _checkInDateDayString;

		public string CheckInDateDayString
		{
			get { return _checkInDateDayString; }
			set { SetValue(ref _checkInDateDayString, value); }
		}
		private string _checkOutDate;

		public string CheckOutDate
		{
			get { return _checkOutDate; }
			set { SetValue(ref _checkOutDate, value); }
		}
		private string _checkOutDateString;

		public string CheckOutDateString
		{
			get { return _checkOutDateString; }
			set { SetValue(ref _checkOutDateString, value); }
		}
		private string _checkOutDateDayString;

		public string CheckOutDateDayString
		{
			get { return _checkOutDateDayString; }
			set { SetValue(ref _checkOutDateDayString, value); }
		}
		private int _adults=1;

		public int Adults
		{
			get { return _adults; }
			set { SetValue(ref _adults, value); }
		}
		private int _children;

		public int Children
		{
			get { return _children; }
			set { SetValue(ref _children, value); }
		}
		private int _noOfRooms = 1;

		public int NoOfRooms
		{
			get { return _noOfRooms; }
			set { SetValue(ref _noOfRooms, value); }
		}
		private int _numNights;

		public int NumNights
		{
			get { return _numNights; }
			set { SetValue(ref _numNights, value); }
		}
		private ObservableCollection<RoomViewModel> _roomsInfo;

		public ObservableCollection<RoomViewModel> RoomsInfo
		{
			get { return _roomsInfo; }
			set { SetValue(ref _roomsInfo, value); }
		}
		private ObservableCollection<RoomViewModel> _selectedRoomsInfo;

		public ObservableCollection<RoomViewModel> SelectedRoomsInfo
		{
			get { return _selectedRoomsInfo; }
			set { SetValue(ref _selectedRoomsInfo, value); }
		}

	}
}

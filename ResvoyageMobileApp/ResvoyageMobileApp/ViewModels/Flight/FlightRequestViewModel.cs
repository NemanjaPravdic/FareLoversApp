using ResvoyageMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.ViewModels.Flight
{
    public class FlightRequestViewModel : BaseViewModel
    {
		private int _adults = 1;

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

		private int _infants;

		public int Infants
		{
			get { return _infants; }
			set { SetValue(ref _infants, value); }
		}
		private CabinType _cabin;

		public CabinType Cabin
		{
			get { return _cabin; }
			set { SetValue(ref _cabin, value); }
		}

		private string _departureDate1;

		public string DepartureDate1
		{
			get { return _departureDate1; }
			set { SetValue(ref _departureDate1, value); }
		}
		private string _departureDate1String;

		public string DepartureDate1String
		{
			get { return _departureDate1String; }
			set { SetValue(ref _departureDate1String, value); }
		}
		private string _departureDate1DayString;

		public string DepartureDate1DayString
		{
			get { return _departureDate1DayString; }
			set { SetValue(ref _departureDate1DayString, value); }
		}

		private string _departureDate2;

		public string DepartureDate2
		{
			get { return _departureDate2; }
			set { SetValue(ref _departureDate2, value); }
		}
		private string _departureDate2String;

		public string DepartureDate2String
		{
			get { return _departureDate2String; }
			set { SetValue(ref _departureDate2String, value); }
		}
		private string _departureDate2DayString;

		public string DepartureDate2DayString
		{
			get { return _departureDate2DayString; }
			set { SetValue(ref _departureDate2DayString, value); }
		}

		private string _departureDate3;

		public string DepartureDate3
		{
			get { return _departureDate3; }
			set { SetValue(ref _departureDate3, value); }
		}
		private string _departureDate3String;

		public string DepartureDate3String
		{
			get { return _departureDate3String; }
			set { SetValue(ref _departureDate3String, value); }
		}
		private string _departureDate3DayString;

		public string DepartureDate3DayString
		{
			get { return _departureDate3DayString; }
			set { SetValue(ref _departureDate3DayString, value); }
		}
		private string _from1Iata;

		public string From1Iata
		{
			get { return _from1Iata; }
			set { SetValue(ref _from1Iata, value); }
		}
		private string _from1City;

		public string From1City
		{
			get { return _from1City; }
			set { SetValue(ref _from1City, value); }
		}
		private string _from2Iata;

		public string From2Iata
		{
			get { return _from2Iata; }
			set { SetValue(ref _from2Iata, value); }
		}
		private string _from2City;

		public string From2City
		{
			get { return _from2City; }
			set { SetValue(ref _from2City, value); }
		}
		private string _from3Iata;

		public string From3Iata
		{
			get { return _from3Iata; }
			set { SetValue(ref _from3Iata, value); }
		}
		private string _from3City;

		public string From3City
		{
			get { return _from3City; }
			set { SetValue(ref _from3City, value); }
		}
		private string _to1Iata;

		public string To1Iata
		{
			get { return _to1Iata; }
			set { SetValue(ref _to1Iata, value); }
		}
		private string _to1City;

		public string To1City
		{
			get { return _to1City; }
			set { SetValue(ref _to1City, value); }
		}
		private string _to2Iata;

		public string To2Iata
		{
			get { return _to2Iata; }
			set { SetValue(ref _to2Iata, value); }
		}
		private string _to2City;

		public string To2City
		{
			get { return _to2City; }
			set { SetValue(ref _to2City, value); }
		}
		private string _to3Iata;

		public string To3Iata
		{
			get { return _to3Iata; }
			set { SetValue(ref _to3Iata, value); }
		}
		private string _to3City;

		public string To3City
		{
			get { return _to3City; }
			set { SetValue(ref _to3City, value); }
		}
		private SearchType _searchType;

		public SearchType SearchType
		{
			get { return _searchType; }
			set { SetValue(ref _searchType, value); }
		}
		private int _multiCityActieSegments;

		public int MultiCityActieSegments
		{
			get { return _multiCityActieSegments; }
			set { SetValue(ref _multiCityActieSegments, value); }
		}
		private bool _isFromPopularPlaces;

		public bool IsFromPopularPlaces
		{
			get { return _isFromPopularPlaces; }
			set { SetValue(ref _isFromPopularPlaces, value); }
		}

	}
}

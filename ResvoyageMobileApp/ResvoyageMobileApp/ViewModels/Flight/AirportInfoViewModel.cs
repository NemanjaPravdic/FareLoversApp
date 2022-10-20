using ResvoyageMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.ViewModels.Flight
{
    public class AirportInfoViewModel : BaseViewModel
    {
        public AirportInfoViewModel(AirportInfo info)
        {
            _code = info.Code;
            _name = info.Name;
            _city = info.City;
            _region = info.Region;
            _country = info.Country;

        }
        private string _code;

        public string Code
        {
            get { return _code; }
            set { SetValue(ref _code, value); }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }
        private string _city;

        public string City
        {
            get { return _city; }
            set { SetValue(ref _city, value); }
        }
        private string _region;

        public string Region
        {
            get { return _region; }
            set { SetValue(ref _region, value); }
        }
        private string _country;

        public string Country
        {
            get { return _country; }
            set { SetValue(ref _country, value); }
        }

    }
}

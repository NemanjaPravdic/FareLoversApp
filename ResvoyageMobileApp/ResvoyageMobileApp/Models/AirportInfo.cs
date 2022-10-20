using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models
{
    public class AirportInfo
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public DateTime SelectedDate {get;set;}
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.Flight
{
    public class AirportDestination
    {
        public List<AirportInfo> Airports { get; set; }
        public ErrorResult Error { get; set; }
    }
}

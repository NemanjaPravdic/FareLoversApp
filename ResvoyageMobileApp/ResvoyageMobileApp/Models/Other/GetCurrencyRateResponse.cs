using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.Other
{
    public class GetCurrencyRateResponse
    {
        public string From { get; set; }
        public string To { get; set; }
        public decimal Rate { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.User
{
    public class UserLocation
    {
        [JsonProperty("longitude")]
        public string Longitude { get; set; }
        [JsonProperty("latitude")]
        public string Latitude { get; set; }
        [JsonProperty("languages")]
        public string Languages { get; set; }
        [JsonProperty("currency")]
        public string CurrencyCode { get; set; }
        [JsonProperty("currency_name")]
        public string CurrencyName { get; set; }
    }
    
}

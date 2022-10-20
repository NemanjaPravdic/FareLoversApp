using Newtonsoft.Json;
using RestSharp;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Models.User;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ResvoyageMobileApp.Services.Flight
{
    public class PlaceService : BaseService
    {
        public async Task<List<AirportInfo>> GetNearbyAirports()
        {
            try
            {
                var response = new UserLocation();
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    return await GetAirports(location.Longitude.ToString(), location.Latitude.ToString());
                }
                else if (Application.Current.Properties.ContainsKey("UserLocation"))
                {
                    var placeInfo = JsonConvert.DeserializeObject<UserLocation>(Application.Current.Properties["UserLocation"]?.ToString());
                    if (!string.IsNullOrEmpty(placeInfo.Longitude) && !string.IsNullOrEmpty(placeInfo.Latitude))
                    {
                        return await GetAirports(placeInfo.Longitude.ToString(), placeInfo.Latitude.ToString());
                    }

                }
                else
                {
                    var locationByIp = GetPlaceInfoByIp();
                    if (locationByIp != null && !string.IsNullOrEmpty(locationByIp.Longitude) && !string.IsNullOrEmpty(locationByIp.Latitude))
                    {
                        return await GetAirports(locationByIp.Longitude.ToString(), locationByIp.Latitude.ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                if (Application.Current.Properties.ContainsKey("UserLocation"))
                {
                    var placeInfo = JsonConvert.DeserializeObject<UserLocation>(Application.Current.Properties["UserLocation"]?.ToString());
                    if (!string.IsNullOrEmpty(placeInfo.Longitude) && !string.IsNullOrEmpty(placeInfo.Latitude))
                    { 
                        return await GetAirports(placeInfo.Longitude.ToString(), placeInfo.Latitude.ToString());
                    }
                }
                else
                {
                    var locationByIp = GetPlaceInfoByIp();
                    if (locationByIp != null && !string.IsNullOrEmpty(locationByIp.Longitude) && !string.IsNullOrEmpty(locationByIp.Latitude))
                    {
                        return await GetAirports(locationByIp.Longitude.ToString(), locationByIp.Latitude.ToString());
                    }

                }
            }
            return null;
        }
        public UserLocation GetPlaceInfoByIp()
        {
            var client = new RestClient("https://ipapi.co/json/");
            var request = new RestRequest()
            {
                Method = Method.GET
            };

            var response = client.Execute(request);

            var info = JsonConvert.DeserializeObject<UserLocation>(response.Content);
            Application.Current.Properties["UserLocation"] = response.Content;
            Application.Current.SavePropertiesAsync();

            return info;

        }
        private async Task<List<AirportInfo>> GetAirports(string longitude, string latitude)
        {
            var token = await GetToken();
            var restRequest = new RestRequest("api/v1/air/references/nearby?longitude=" + longitude + "&latitude=" + latitude);
            restRequest.AddHeader("Content-Type", "application/json-patch+json");
            restRequest.AddHeader("Authorization", "Bearer " + token);
            restRequest.AddHeader("Accept", "application/json");

            var restResut = await _client.ExecuteAsync(restRequest);
            var response = new List<AirportInfo>();
            if (restResut.IsSuccessful && restResut.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var results = JsonConvert.DeserializeObject<List<AirportInfo>>(restResut.Content);
                response = results;
            }

            return response;
        }
    }
}

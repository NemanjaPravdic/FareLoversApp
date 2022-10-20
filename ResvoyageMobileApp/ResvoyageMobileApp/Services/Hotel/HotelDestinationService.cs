using Newtonsoft.Json;
using RestSharp;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Models.Hotel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ResvoyageMobileApp.Services.Hotel
{
    public class HotelDestinationService : BaseService
    {
        public async Task<HotelDestination> GetDestinationAutocompleteResults(string search)
        {
            var token = await GetToken();
            var restRequest = new RestRequest("api/v1/hotel/references/destination/" + search.ToLower());
            restRequest.AddHeader("Content-Type", "application/json-patch+json");
            restRequest.AddHeader("Authorization", "Bearer " + token);
            restRequest.AddHeader("Accept", "application/json");

            var restResut = await _client.ExecuteAsync(restRequest);
            var response = new HotelDestination();
            if (restResut.IsSuccessful && restResut.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var results = JsonConvert.DeserializeObject<List<HotelCityData>>(restResut.Content);
                response.Resutls = results;
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorResult>(restResut.Content);
                response.Error = error;
            }
            return response;

        }
    }
}

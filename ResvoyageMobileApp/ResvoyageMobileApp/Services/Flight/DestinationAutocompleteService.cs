using Newtonsoft.Json;
using RestSharp;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Models.Flight;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ResvoyageMobileApp.Services.Flight
{
    public class DestinationAutocompleteService : BaseService
    {
        public async Task<AirportDestination> GetDestinationAutocompleteResults(string search)
        {
            var token = await GetToken();
            var restRequest = new RestRequest("api/v1/air/references/airports?search=" + search);
            restRequest.AddHeader("Content-Type", "application/json-patch+json");
            restRequest.AddHeader("Authorization", "Bearer " + token);
            restRequest.AddHeader("Accept", "application/json");

            var restResut = await _client.ExecuteAsync(restRequest);
            var response = new AirportDestination();
            if (restResut.IsSuccessful && restResut.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var results = JsonConvert.DeserializeObject<List<AirportInfo>>(restResut.Content);
                response.Airports = results;
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

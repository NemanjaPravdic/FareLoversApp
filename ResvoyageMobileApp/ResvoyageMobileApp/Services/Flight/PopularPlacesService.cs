using Newtonsoft.Json;
using RestSharp;
using ResvoyageMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ResvoyageMobileApp.Services.Flight
{
    public class PopularPlacesService : BaseService
    {
        public async Task<List<Place>> GetPopularPlaces()
        {
            var token = await GetToken();
            var restRequest = new RestRequest("api/v1/air/popularplaces");
            restRequest.AddHeader("Content-Type", "application/json-patch+json");
            restRequest.AddHeader("Authorization", "Bearer " + token);
            restRequest.AddHeader("Accept", "application/json");

            var restResut = await _client.ExecuteAsync(restRequest);
            var response = new List<Place>();
            if (restResut.IsSuccessful && restResut.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var results = JsonConvert.DeserializeObject<List<Place>>(restResut.Content);
                response = results;
            }

            return response;
        }
    }
}

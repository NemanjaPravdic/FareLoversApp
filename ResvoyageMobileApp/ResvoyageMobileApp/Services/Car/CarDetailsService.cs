using Newtonsoft.Json;
using RestSharp;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Models.Car;
using ResvoyageMobileApp.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ResvoyageMobileApp.Services.Car
{
    public class CarDetailsService : BaseService
    {
        public async Task<CarRules> GetCarDetailsResponseAsync(Guid sessionId, Guid carId)
        {
            var token = await GetToken();
            var url = string.Format("api/v1/car/details?sessionid={0}&carid={1}", sessionId, carId);
            var restRequest = new RestRequest(url);
            restRequest.AddHeader("Content-Type", "application/json-patch+json");
            restRequest.AddHeader("Authorization", "Bearer " + token);
            restRequest.AddHeader("Accept", "application/json");

            var restResut = await _client.ExecuteAsync(restRequest);
            var response = new CarRules();
            if (restResut.IsSuccessful && restResut.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response = JsonConvert.DeserializeObject<CarRules>(restResut.Content);
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorResult>(restResut.Content);
                response.ErrorResult = error;
            }
            return response;
        }
    }
}

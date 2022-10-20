using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Models.Flight;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ResvoyageMobileApp.Services.Flight
{
    public class AddToCartService : BaseService
    {
        public async Task<ShoppingCartModel> AddToCartAsync(PreparedFlightDetails flightDetails)
        {
            var token = await GetToken();
            var restRequest = new RestRequest("/api/v1/cart/air/add", Method.POST);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", "Bearer " + token);
            restRequest.AddJsonBody(GenerateRequest(flightDetails));
            IRestResponse restResut = await _client.ExecuteAsync(restRequest);
            var response = new ShoppingCartModel();
            if (restResut.IsSuccessful && restResut.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response = JsonConvert.DeserializeObject<ShoppingCartModel>(restResut.Content);
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorResult>(restResut.Content);
                response.Error = error;
            }
                
            return response;
        }

        private AddToCartModel GenerateRequest(PreparedFlightDetails flightDetails)
        {
            return new AddToCartModel
            {
                SessionId = flightDetails.SessionId,
                ItemId = flightDetails.Id
            };
        }
    }
}

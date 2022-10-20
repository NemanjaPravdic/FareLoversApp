using Newtonsoft.Json;
using RestSharp;
using ResvoyageMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ResvoyageMobileApp.Services.Car
{
    public class AddCarToShoppingCartService : BaseService
    {
        public async Task<ShoppingCartModel> AddToCartAsync(Guid CarId, Guid SesionId)
        {
            var token = await GetToken();
            var restRequest = new RestRequest("/api/v1/cart/car/add", Method.POST);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", "Bearer " + token);
            restRequest.AddJsonBody(new AddToCartModel { ItemId = CarId, SessionId = SesionId});
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

    }
}

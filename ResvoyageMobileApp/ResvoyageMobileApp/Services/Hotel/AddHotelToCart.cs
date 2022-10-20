using Newtonsoft.Json;
using RestSharp;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Models.Hotel;
using ResvoyageMobileApp.ViewModels.Hotel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ResvoyageMobileApp.Services.Hotel
{
    public class AddHotelToCart : BaseService
    {
        public async Task<ShoppingCartModel> AddToCartAsync(Guid sessionId, RoomInfo room, HotelRequestViewModel request)
        {
            var token = await GetToken();
            var restRequest = new RestRequest("/api/v1/cart/hotel/add", Method.POST);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", "Bearer " + token);
            restRequest.AddJsonBody(GenerateRequest(sessionId, room, request));
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

        private HotelDetailsRequest GenerateRequest(Guid sessionId, RoomInfo room, HotelRequestViewModel request)
        {
            return new HotelDetailsRequest
            {
                SessionId = sessionId.ToString(),
                RoomId = room.Id.ToString(),
                Adults = request.Adults,
                Children = request.Children
            };
        }
    }
}

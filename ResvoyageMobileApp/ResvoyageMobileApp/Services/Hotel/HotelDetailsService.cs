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
    public class HotelDetailsService : BaseService
    {
        public async Task<HotelSearchResponse> GetHotelDetailsResponseAsync(Guid sessionId, Guid hotelId)
        {
            var token = await GetToken();
            var url = string.Format("api/v1/hotel/details?SessionId={0}&HotelId={1}",sessionId,hotelId);
            var restRequest = new RestRequest(url);
            restRequest.AddHeader("Content-Type", "application/json-patch+json");
            restRequest.AddHeader("Authorization", "Bearer " + token);
            restRequest.AddHeader("Accept", "application/json");

            var restResut = await _client.ExecuteAsync(restRequest);
            var response = new HotelSearchResponse();
            if (restResut.IsSuccessful && restResut.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response = JsonConvert.DeserializeObject<HotelSearchResponse>(restResut.Content);
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

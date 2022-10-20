using Newtonsoft.Json;
using RestSharp;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Models.Booking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ResvoyageMobileApp.Services.Bookings
{
    public class BookingService : BaseService
    {
        public async Task<BookingInfoResponse> GetBookingInfoAsync(int userId)
        {
            var token = await GetToken();
            var restRequest = new RestRequest("api/v1/reservation/user/"+userId);
            restRequest.AddHeader("Content-Type", "application/json-patch+json");
            restRequest.AddHeader("Authorization", "Bearer " + token);
            restRequest.AddHeader("Accept", "application/json");

            var restResut = await _client.ExecuteAsync(restRequest);
            var response = new BookingInfoResponse();
            if (restResut.IsSuccessful && restResut.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response.Bookings = JsonConvert.DeserializeObject<List<BookingInfo>>(restResut.Content);
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

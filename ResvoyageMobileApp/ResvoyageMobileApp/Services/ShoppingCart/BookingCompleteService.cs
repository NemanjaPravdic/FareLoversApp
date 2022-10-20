using Newtonsoft.Json;
using RestSharp;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Models.ShoppingCartModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ResvoyageMobileApp.Services.ShoppingCart
{
    public class BookingCompleteService : BaseService
    {
        public async Task<BookingCompleteResponse> BookAsync(BookingCompleteRequest request)
        {
            var token = await GetToken();
            var restRequest = new RestRequest("/api/v1/cart/book", Method.POST);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", "Bearer " + token);
            restRequest.AddJsonBody(request);
            IRestResponse restResut = await _client.ExecuteAsync(restRequest);
            var response = new BookingCompleteResponse();
            if (restResut.IsSuccessful && restResut.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response = JsonConvert.DeserializeObject<BookingCompleteResponse>(restResut.Content);
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

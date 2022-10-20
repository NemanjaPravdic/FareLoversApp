using Newtonsoft.Json;
using RestSharp;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ResvoyageMobileApp.Services.User
{
    public class UserBaseService : BaseService
    {
        public async Task<UserDetailsResponse> GetUserDetailsAsync(string username)
        {
            var token = await GetToken();
            var restRequest = new RestRequest("api/v1/users/"+ username + "/details");
            restRequest.AddHeader("Content-Type", "application/json-patch+json");
            restRequest.AddHeader("Authorization", "Bearer " + token);
            restRequest.AddHeader("Accept", "application/json");

            var restResut = await _client.ExecuteAsync(restRequest);
            var response = new UserDetailsResponse();
            if (restResut.IsSuccessful && restResut.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var details = JsonConvert.DeserializeObject<UserDetails>(restResut.Content);
                response.UserDetails = details;
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

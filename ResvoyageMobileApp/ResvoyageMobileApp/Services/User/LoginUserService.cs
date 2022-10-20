using Newtonsoft.Json;
using RestSharp;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ResvoyageMobileApp.Services.User
{
    public class LoginUserService : BaseService
    {
        public async Task<UserLoginResoponse> LoginUserAsync(string username, string password, bool IsSocialNetwork = false)
        {
            var restRequest = new RestRequest("api/v1/token", Method.POST);
            restRequest.AddHeader("Content-Type", "application/json");

            var request = new UserLoginRequest { Username = username };
            if (!IsSocialNetwork)
                request.Password = password;                

            restRequest.AddJsonBody(request);
            IRestResponse restResut = await _client.ExecuteAsync(restRequest);

            var response = JsonConvert.DeserializeObject<UserLoginResoponse>(restResut.Content);

            if (!string.IsNullOrEmpty(response?.Token))
            {
                response.IsSuccessful = true;
                Application.Current.Properties["RVToken"] = response.Token;
            }
            else
            {
                if(response == null)
                    response = new UserLoginResoponse();

                response.IsSuccessful = false;

                if (string.IsNullOrEmpty(response.Message))
                    response.Message = "Something went wrong!";
            }
            return response;
        }
    }
}

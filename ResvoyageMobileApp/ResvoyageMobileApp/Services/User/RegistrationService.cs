using Newtonsoft.Json;
using RestSharp;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Models.User;
using ResvoyageMobileApp.ViewModels.Other;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ResvoyageMobileApp.Services.User
{
    public class RegistrationService : BaseService
    {
        public async Task<UserRegistrationResponse> RegisterUserAsync(UserViewModel userViewModel, bool IsFromSocial = false)
        {
            var request = TransformUserViewModel(userViewModel, IsFromSocial);
            var token = await GetToken();
            var restRequest = new RestRequest("/api/v1/users/create", Method.POST);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", "Bearer " + token);
            restRequest.AddJsonBody(request);
            IRestResponse restResut = await _client.ExecuteAsync(restRequest);
            var response = new UserRegistrationResponse();
            if (restResut.IsSuccessful && restResut.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response = JsonConvert.DeserializeObject<UserRegistrationResponse>(restResut.Content);
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorResult>(restResut.Content);
                response.ErrorResult = error;
            }
            return response;
        }

        private UserRequest TransformUserViewModel(UserViewModel userViewModel, bool IsFromSocial = false)
        {
            return new UserRequest
            {
                Password = userViewModel.Password,
                DateOfBirth = userViewModel.DateOfBirth,
                FirstName = userViewModel.FirstName,
                MiddleName = userViewModel.MiddleName,
                LastName = userViewModel.LastName,
                Phone = userViewModel.Phone,
                Title = userViewModel.Title,
                EmailAddress = userViewModel.Email,
                Gender = userViewModel.Gender != null ? userViewModel.Gender.ToLower() == "male" ? Models.GenderType.Male : Models.GenderType.Female : Models.GenderType.Male,
                Username = userViewModel.Username,
                IsFromSocialNetwork = IsFromSocial
            };
        }
    }
}

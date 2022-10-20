using Newtonsoft.Json;
using ResvoyageMobileApp.Models.User;
using ResvoyageMobileApp.ViewModels.Other;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ResvoyageMobileApp.Services.User
{
    public class SocialNetworkService : UserBaseService
    {
        private LoginUserService _loginUserService;
        private RegistrationService _registrationService;

        public SocialNetworkService()
        {
            _loginUserService = new LoginUserService();
            _registrationService = new RegistrationService();
        }

        public async Task<UserLoginResoponse> RegisterAndLogin(UserViewModel userViewModel)
        {
            var loginResponse = await _loginUserService.LoginUserAsync(userViewModel.Username, null, true);

            if (loginResponse.IsSuccessful && !string.IsNullOrEmpty(loginResponse.Token))
            {
                var userInfo = await GetUserDetailsAsync(userViewModel.Username);
                if (userInfo.UserDetails != null)
                {
                    SetSessionData(userInfo.UserDetails, loginResponse.Token);
                    return loginResponse;
                }
                else if (userInfo.Error != null && !string.IsNullOrEmpty(userInfo.Error.ErrorMessage))
                {
                    return new UserLoginResoponse() { IsSuccessful = false, Message = userInfo.Error.ErrorMessage };
                }
                else
                { 
                    return new UserLoginResoponse() { IsSuccessful = false, Message = "Something went wrong!" };
                }

            }
            else
            {
                var registrationResponse = await _registrationService.RegisterUserAsync(userViewModel, true);

                if (registrationResponse.IsSuccessful && registrationResponse.UserInformation != null)
                {
                    loginResponse = await _loginUserService.LoginUserAsync(userViewModel.Username, null, true);
                    var userInfo = await GetUserDetailsAsync(userViewModel.Username);
                    if (userInfo.UserDetails != null)
                    {
                        SetSessionData(userInfo.UserDetails, loginResponse.Token);
                        return loginResponse;
                    }
                    else if (userInfo.Error != null && !string.IsNullOrEmpty(userInfo.Error.ErrorMessage))
                    {
                        return new UserLoginResoponse() { IsSuccessful = false, Message = userInfo.Error.ErrorMessage };
                    }
                    else
                    {
                        return new UserLoginResoponse() { IsSuccessful = false, Message = "Something went wrong!" };
                    }
                }
                else
                {
                    var response = new UserLoginResoponse() { IsSuccessful = false };
                    if (registrationResponse.ErrorResult != null && registrationResponse.ErrorResult.ErrorMessage != null)
                    {
                        response.Message = registrationResponse.ErrorResult.ErrorMessage;
                    }
                    else
                    {
                        response.Message = "Something went wrong";
                    }

                    return response;
                }
            }
        }

        private void SetSessionData(UserDetails user, string token)
        {
            var userJson = JsonConvert.SerializeObject(user);
            Application.Current.Properties.Remove("UserInfo");
            Application.Current.Properties.Remove("Token");

            Application.Current.Properties.Add("UserInfo", userJson);
            Application.Current.Properties.Add("Token", token);
            Application.Current.Properties["IsLoggedIn"] = true;
            Application.Current.SavePropertiesAsync();

        }
    }
}

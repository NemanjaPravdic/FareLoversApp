using Newtonsoft.Json;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Services.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Other
{
    public class RegistrationViewModel : BaseViewModel
    {
        private RegistrationService _registrationService;
        private LoginUserService _loginUserService;
        public RegistrationViewModel()
        {
            _user = new UserViewModel();
            _registrationService = new RegistrationService();
            _loginUserService = new LoginUserService();
        }
        private UserViewModel _user;

        public UserViewModel User
        {
            get { return _user; }
            set { SetValue(ref _user, value); }
        }
        private bool _showWaitScreen;

        public bool ShowWaitScreen
        {
            get { return _showWaitScreen; }
            set { SetValue(ref _showWaitScreen, value); }
        }
        public ICommand SingUpCommand => new Command(SingUp);

        private async void SingUp()
        {
            ShowWaitScreen = true;
            if (ValidateRequest())
            {
                try
                {
                    var response = await _registrationService.RegisterUserAsync(User);
                    if (response.IsSuccessful && response.UserInformation != null)
                    {
                        var loginResoponse = await _loginUserService.LoginUserAsync(User.Username, User.Password);

                        if (loginResoponse.IsSuccessful && !string.IsNullOrEmpty(loginResoponse.Token))
                        {
                            var userJson = JsonConvert.SerializeObject(response.UserInformation);
                            Application.Current.Properties.Remove("UserInfo");
                            Application.Current.Properties.Remove("Password");
                            Application.Current.Properties.Remove("Token");

                            Application.Current.Properties.Add("UserInfo", userJson);
                            Application.Current.Properties.Add("Password", User.Password);
                            Application.Current.Properties.Add("Token", loginResoponse.Token);
                            Application.Current.Properties["IsLoggedIn"] = true;
                            await Application.Current.SavePropertiesAsync();


                            var navigation = Application.Current.MainPage as Shell;
                            await navigation.Navigation.PopAsync();
                            await navigation.Navigation.PopAsync();
                            ShowWaitScreen = false;
                        }
                        else if (loginResoponse.Message != null)
                        {
                            ShowWaitScreen = false;
                            await _navigation.DisplayAlert(AppResources.APP_ERROR, loginResoponse.Message, AppResources.APP_OK);
                        }
                        else
                        {
                            ShowWaitScreen = false;
                            await _navigation.DisplayAlert(AppResources.APP_ERROR, AppResources.ER_SOME_ERROR_OCCURED, AppResources.APP_OK);
                        }
                    }
                    else if (response.ErrorResult != null && !string.IsNullOrEmpty(response.ErrorResult.ErrorMessage))
                    {
                        ShowWaitScreen = false;
                        await _navigation.DisplayAlert(AppResources.APP_ERROR, response.ErrorResult.ErrorMessage, AppResources.APP_OK);
                    }
                    else
                    {
                        ShowWaitScreen = false;
                        await _navigation.DisplayAlert(AppResources.APP_ERROR, AppResources.ER_SOME_ERROR_OCCURED, AppResources.APP_OK);
                    }

                }
                catch (Exception ex)
                {
                    ShowWaitScreen = false;
                    await _navigation.DisplayAlert(AppResources.APP_ERROR, ex.Message, AppResources.APP_OK);
                }
            }
            ShowWaitScreen = false;

        }

        private bool ValidateRequest()
        {
            if (string.IsNullOrEmpty(User.Username))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, Resources.AppResources.U_USERNAME + Resources.AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(User.Title))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, Resources.AppResources.SC_TITLE + Resources.AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(User.FirstName))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, Resources.AppResources.SC_FIRST_NAME + Resources.AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(User.LastName))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, Resources.AppResources.SC_LAST_NAME + Resources.AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(User.Day))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, Resources.AppResources.SC_DAY + Resources.AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(User.Month))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, Resources.AppResources.SC_MONTH + Resources.AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(User.Year))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, Resources.AppResources.SC_YEAR + Resources.AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(User.Gender))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, Resources.AppResources.SC_GENDER + Resources.AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(User.Email))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, Resources.AppResources.SC_EMAIL + Resources.AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (!IsValidEmail(User.Email))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, Resources.AppResources.VR_EMAIL_IS_NOT_IN_VALID_FORMAT, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(User.Password))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, Resources.AppResources.U_PASSWORD + Resources.AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(User.RepeatPassword))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, Resources.AppResources.U_REPEAT_PASSWORD + Resources.AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (User.Password != User.RepeatPassword)
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, Resources.AppResources.VR_PASSWORD_DOES_NOT_MATCH, AppResources.APP_OK);
                return false;
            }

            return true;
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}

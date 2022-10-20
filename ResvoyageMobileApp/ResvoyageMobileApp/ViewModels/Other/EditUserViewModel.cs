using Newtonsoft.Json;
using ResvoyageMobileApp.Models.User;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Services.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Other
{
    public class EditUserViewModel : BaseViewModel
    {
        private EditUserService _editUserService;
        public EditUserViewModel(UserDetails userDetails)
        {
            _editUserService = new EditUserService();

            _user = TransformUserDetails(userDetails);

            if (Application.Current.Properties.ContainsKey("Password"))
                _oldPasswordVisibility = true;

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
        private bool _oldPasswordVisibility;

        public bool OldPasswordVisibility
        {
            get { return _oldPasswordVisibility; }
            set { SetValue(ref _oldPasswordVisibility, value); }
        }
        public ICommand EditProfileCommand => new Command(EditProfile);

        private async void EditProfile()
        {
            ShowWaitScreen = true;

            if (ValidateRequest())
            {
                try
                {
                    var response = await _editUserService.EditUserAsync(User);

                    if (response.IsSuccessful && response.UserInformation != null)
                    {
                        var userJson = JsonConvert.SerializeObject(response.UserInformation);
                        Application.Current.Properties.Remove("UserInfo");

                        if(Application.Current.Properties.ContainsKey("Password"))
                            Application.Current.Properties.Remove("Password");

                        Application.Current.Properties.Add("UserInfo", userJson);
                        Application.Current.Properties.Add("Password", string.IsNullOrEmpty(User.Password) ? User.OldPassword : User.Password);
                        await Application.Current.SavePropertiesAsync();


                        ShowWaitScreen = false;
                        await _navigation.DisplayAlert(AppResources.APP_SUCCESS, AppResources.EU_SUCCESSFULY_EDIT, AppResources.APP_OK);
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
            if (string.IsNullOrEmpty(User.Title))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_TITLE + AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(User.FirstName))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_FIRST_NAME + AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(User.LastName))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_LAST_NAME + AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(User.Day))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_DAY + AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(User.Month))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_MONTH + AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(User.Year))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_YEAR + AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(User.Gender))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_GENDER + AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(User.Email))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.SC_EMAIL + AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (!IsValidEmail(User.Email))
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.VR_EMAIL_IS_NOT_IN_VALID_FORMAT, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(User.Password) && !OldPasswordVisibility)
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.U_NEW_PASSWORD + AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (string.IsNullOrEmpty(User.RepeatPassword) && !OldPasswordVisibility)
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.U_REPEAT_PASSWORD + AppResources.VR_IS_MANADTORY_FIELD, AppResources.APP_OK);
                return false;
            }
            else if (User.Password != User.RepeatPassword)
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.VR_PASSWORD_DOES_NOT_MATCH, AppResources.APP_OK);
                return false;
            }
            else if (Application.Current.Properties.ContainsKey("Password") && User.OldPassword != Application.Current.Properties["Password"].ToString())
            {
                Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.EU_WRONG_OLD_PASSWORD, AppResources.APP_OK);
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

        private UserViewModel TransformUserDetails(UserDetails userDetails)
        {
            return new UserViewModel 
            {
                Id = userDetails.Id,
                Day = userDetails.DateOfBirth?.Day.ToString(),
                Email = userDetails.EmailAddress,
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                Gender = userDetails.Gender,
                Month = userDetails.DateOfBirth?.Month.ToString(),
                Phone = userDetails.PhoneNumber,
                Title = userDetails.Title,
                Username = userDetails.UserName,
                Year = userDetails.DateOfBirth?.Year.ToString(),
                IsMale = userDetails.Gender != null && userDetails.Gender == "Male",
                IsFemale = userDetails.Gender != null && userDetails.Gender == "Female"
            };
        }

    }
}

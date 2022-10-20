using Newtonsoft.Json;
using ResvoyageMobileApp.Interfaces;
using ResvoyageMobileApp.Models.Other;
using ResvoyageMobileApp.Models.User;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Services.User;
using ResvoyageMobileApp.Views;
using ResvoyageMobileApp.Views.Other;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Other
{
    public class LoginViewModel : BaseViewModel
    {
        private LoginUserService _loginUserService;
        private UserBaseService _userDetailsService;
        private SocialNetworkService _socialNetworkService;
        private Account account;
        [Obsolete]
        private AccountStore store;

        public bool IsAppleSignInAvailable { get { return appleSignInService?.IsAvailable ?? false; } }

        IAppleSignInService appleSignInService;

        [Obsolete]
        public LoginViewModel()
        {
            _user = new UserViewModel();
            _loginUserService = new LoginUserService();
            _userDetailsService = new UserBaseService();
            _socialNetworkService = new SocialNetworkService();
            store = AccountStore.Create();
            appleSignInService = DependencyService.Get<IAppleSignInService>();
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

        public ICommand ContinueWitoutLoginCommand => new Command(ContinueWitoutLogin);
        public ICommand LoginCommand => new Command(Login);
        public ICommand GoToRegisterPageCommand => new Command(GoToRegisterPage);
        public ICommand SignInWithAppleCommand => new Command(OnAppleSignInRequest);
        [Obsolete]
        public ICommand FacebookLoginCommand => new Command(FacebookLogin);
        [Obsolete]
        public ICommand GoogleLoginCommand => new Command(GoogleLogin);
        private void ContinueWitoutLogin()
        {
            Application.Current.Properties["IsLoggedIn"] = true;
            var navigation = Application.Current.MainPage as Shell;
            navigation.Navigation.PopAsync(true);
        }
        private void GoToRegisterPage()
        {
            var page = new RegistrationPage();
            var navigation = Application.Current.MainPage as Shell;
            navigation.Navigation.PushAsync(page, true);

        }
        private async void OnAppleSignInRequest()
        {

            var account = await appleSignInService.SignInAsync();
            if (account != null && !string.IsNullOrEmpty(account.UserId))
            {
                var email = !string.IsNullOrEmpty(account.Email) ? account.Email : Application.Current.Properties[account.UserId].ToString();

                if (!string.IsNullOrEmpty(email))
                    Application.Current.Properties[account.UserId] = email;
                else
                {
                    var navigation = Application.Current.MainPage as ContentPage;
                    await navigation.DisplayAlert(AppResources.APP_ERROR, AppResources.ER_SOME_ERROR_OCCURED, AppResources.APP_OK);
                }

                var register = await _socialNetworkService.RegisterAndLogin(new UserViewModel { Email = email, Password = new Guid().ToString(), Username = email, FirstName = account.FirstName, LastName = account.LastName});

                if (register.IsSuccessful)
                {
                    var navigation = Application.Current.MainPage as Shell;
                    await navigation.Navigation.PopAsync(true);
                    ShowWaitScreen = false;
                }
                else if (register.Message != null)
                {
                    ShowWaitScreen = false;
                    await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, register.Message, AppResources.APP_OK);
                }
                else
                {
                    ShowWaitScreen = false;
                    await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.ER_SOME_ERROR_OCCURED, AppResources.APP_OK);
                }


            }

        }
        private async void Login()
        { 
            ShowWaitScreen = true;
            if (string.IsNullOrEmpty(User.Username))
            {
                var navigation = Application.Current.MainPage as ContentPage;
                await navigation.DisplayAlert(AppResources.APP_ERROR, AppResources.U_USERNAME_EMPTY_FIELD, AppResources.APP_OK);
                ShowWaitScreen = false;
            }
            else if (string.IsNullOrEmpty(User.Password))
            {
                var navigation = Application.Current.MainPage as ContentPage;
                await navigation.DisplayAlert(AppResources.APP_ERROR, AppResources.U_PASSWORD_EMPTY_FIELD, AppResources.APP_OK);
                ShowWaitScreen = false;
            }
            else
            {
                try
                {
                    var result = await _loginUserService.LoginUserAsync(User.Username, User.Password);
                    if (result.IsSuccessful)
                    {
                        var user = await _userDetailsService.GetUserDetailsAsync(User.Username);

                        if (user.UserDetails != null)
                        {
                            var userJson = JsonConvert.SerializeObject(user.UserDetails);
                            Application.Current.Properties.Remove("UserInfo");
                            Application.Current.Properties.Remove("Password");
                            Application.Current.Properties.Remove("Token");

                            Application.Current.Properties.Add("UserInfo", userJson);
                            Application.Current.Properties.Add("Password", User.Password);
                            Application.Current.Properties.Add("Token", result.Token);
                            Application.Current.Properties["IsLoggedIn"] = true;
                            await Application.Current.SavePropertiesAsync();


                            var navigation = Application.Current.MainPage as Shell;
                            await navigation.Navigation.PopAsync(true);
                            ShowWaitScreen = false;
                        }
                        else
                        {
                            ShowWaitScreen = false;
                            if (user.Error != null && !string.IsNullOrEmpty(user.Error.ErrorMessage))
                            {
                                await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, user.Error.ErrorMessage, AppResources.APP_OK);
                            }
                            else
                            { 
                                await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.ER_SOME_ERROR_OCCURED, AppResources.APP_OK);
                            }
                        }
                    }
                    else
                    {
                        ShowWaitScreen = false;
                        await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, result.Message, AppResources.APP_OK);
                    }
                }
                catch (Exception e)
                {
                    await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, e.Message, AppResources.APP_OK);
                }
                
            }
        }

        [Obsolete]
        public void FacebookLogin()
        {
            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();

            var authenticator = new OAuth2Authenticator(
                Constants.FacebookClientId,
                Constants.FacebookScope,
                new Uri(Constants.FacebookAuthorizeUrl),
                new Uri(Constants.FacebookAccessTokenUrl),
                null,
                true);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
        }
        [Obsolete]
        public void GoogleLogin()
        {
            string clientId = null;
            string redirectUri = null;
            //Xamarin.Auth.CustomTabsConfiguration.CustomTabsClosingMessage = null;            

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Constants.GoogleiOSClientId;
                    redirectUri = Constants.GoogleiOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = Constants.GoogleAndroidClientId;
                    redirectUri = Constants.GoogleAndroidRedirectUrl;
                    break;
            }

            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();

            var authenticator = new OAuth2Authenticator(
                clientId,
                null,
                Constants.GoogleScope,
                new Uri(Constants.GoogleAuthorizeUrl),
                new Uri(redirectUri),
                new Uri(Constants.GoogleAccessTokenUrl),
                null,
                true);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
        }

        [Obsolete]
        private async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }


            if (e.IsAuthenticated)
            {
                ShowWaitScreen = true;

                if (authenticator.AuthorizeUrl.Host == "www.facebook.com")
                {
                    FacebookAccount facebookAccount = null;

                    var httpClient = new HttpClient();

                    var json = await httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=id,name,first_name,last_name,email,picture.type(large)&access_token=" + e.Account.Properties["access_token"]);

                    facebookAccount = JsonConvert.DeserializeObject<FacebookAccount>(json);

                    await store.SaveAsync(account = e.Account, Constants.AppName);

                    var register = await _socialNetworkService.RegisterAndLogin(TransformFacebookAccount(facebookAccount));

                    if (register.IsSuccessful)
                    {
                        var navigation = Application.Current.MainPage as Shell;
                        await navigation.Navigation.PopAsync(true);
                        ShowWaitScreen = false;
                    }
                    else if (register.Message != null)
                    {
                        ShowWaitScreen = false;
                        await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, register.Message, AppResources.APP_OK);
                    }
                    else
                    {
                        ShowWaitScreen = false;
                        await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.ER_SOME_ERROR_OCCURED, AppResources.APP_OK);
                    }
                }
                else
                {
                    GoogleUser user = null;

                    // If the user is authenticated, request their basic user data from Google
                    // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                    var request = new OAuth2Request("GET", new Uri(Constants.GoogleUserInfoUrl), null, e.Account);
                    var response = await request.GetResponseAsync();
                    if (response != null)
                    {
                        // Deserialize the data and store it in the account store
                        // The users email address will be used to identify data in SimpleDB
                        string userJson = await response.GetResponseTextAsync();
                        user = JsonConvert.DeserializeObject<GoogleUser>(userJson);
                    }

                    if (account != null)
                    {
                        store.Delete(account, Constants.AppName);
                    }

                    await store.SaveAsync(account = e.Account, Constants.AppName);

                    var register = await _socialNetworkService.RegisterAndLogin(TransformGoogleAccount(user));

                    if (register.IsSuccessful)
                    {
                        var navigation = Application.Current.MainPage as Shell;
                        await navigation.Navigation.PopAsync(true);
                        ShowWaitScreen = false;
                    }
                    else if(register.Message != null)
                    {
                        ShowWaitScreen = false;

                        await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, register.Message, AppResources.APP_OK);
                    }
                    else
                    {
                        ShowWaitScreen = false;

                        await Application.Current.MainPage.DisplayAlert(AppResources.APP_ERROR, AppResources.ER_SOME_ERROR_OCCURED, AppResources.APP_OK);
                    }

                }
            }
        }

        [Obsolete]
        private void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }
        private UserViewModel TransformFacebookAccount(FacebookAccount facebookAccount)
        {
            return new UserViewModel
            {
                Username = facebookAccount.Email,
                FirstName = facebookAccount.First_Name,
                LastName = facebookAccount.Last_Name,
                Email = facebookAccount.Email,
                Password = new Guid().ToString()
            };
        }
        private UserViewModel TransformGoogleAccount(GoogleUser googleUser)
        {
            return new UserViewModel
            {
                Username = googleUser.Email,
                FirstName = googleUser.GivenName,
                LastName = googleUser.FamilyName,
                Email = googleUser.Email,
                Password = new Guid().ToString()
            };
        }
    }
}

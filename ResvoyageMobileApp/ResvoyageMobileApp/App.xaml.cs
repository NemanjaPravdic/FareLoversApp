using ResvoyageMobileApp.Views;
using System;
using Xamarin.Forms;
using ResvoyageMobileApp.Services;
using Xamarin.Forms.Xaml;
using RestSharp;
using Newtonsoft.Json;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Views.Other;
using ResvoyageMobileApp.Models.Other;
using ResvoyageMobileApp.Services.User;
using ResvoyageMobileApp.Models.User;
using ResvoyageMobileApp.Resources;
using Plugin.Multilingual;
using System.Globalization;
using ResvoyageMobileApp.Services.Flight;
using System.Collections.Generic;
using ResvoyageMobileApp.Interfaces;
using Xamarin.Essentials;

[assembly: ExportFont("SF Pro Display Bold.ttf", Alias ="DisplayBold")]
[assembly: ExportFont("SF Pro Display Light.ttf", Alias ="DisplayLight")]
[assembly: ExportFont("SF Pro Display Medium.ttf", Alias ="DisplayMedium")]
[assembly: ExportFont("SF Pro Display Regular.ttf", Alias ="DisplayRegular")]
[assembly: ExportFont("SF Pro Display Semibold.ttf", Alias ="DisplaySemibold")]
[assembly: ExportFont("SF Pro Display Thin.ttf", Alias ="DisplayThin")]
[assembly: ExportFont("SF Pro Text Medium.ttf", Alias ="TextMedium")]
[assembly: ExportFont("SF Pro Text Regular.ttf", Alias = "TextRegular")]
[assembly: ExportFont("SF Pro Text Semibold.ttf", Alias = "TextSemibold")]
namespace ResvoyageMobileApp
{
    public partial class App : Application
    {
        public const string LoggedInKey = "LoggedIn";
        public const string AppleUserIdKey = "AppleUserIdKey";
        string userId;
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjU2OTA3QDMxMzgyZTMxMmUzME4vcWlJT09XNDRjR3pSZ0FvNGd1V3JpR21aSU41dGY2M3ZuUnlYKzFGV0U9");
            GenerateToken();
            InitializeComponent();
            Xamarin.Forms.Device.SetFlags(new string[] { "RadioButton_Experimental", "Shapes_Experimental", "MediaElement_Experimental" });
            SetCulture();
            MainPage = new HomePage();
        }




        protected async override void OnStart()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                var location = await Geolocation.GetLocationAsync();
            }
            var appleSignInService = DependencyService.Get<IAppleSignInService>();
            if (appleSignInService != null)
            {
                var userId = await SecureStorage.GetAsync(App.AppleUserIdKey);
                if (appleSignInService.IsAvailable && !string.IsNullOrEmpty(userId))
                {

                    var credentialState = await appleSignInService.GetCredentialStateAsync(userId);

                    switch (credentialState)
                    {
                        case AppleSignInCredentialState.Authorized:
                            //Normal app workflow...
                            break;
                        case AppleSignInCredentialState.NotFound:
                        case AppleSignInCredentialState.Revoked:
                            //Logout;
                            SecureStorage.Remove(App.AppleUserIdKey);
                            Preferences.Set(App.LoggedInKey, false);
                            break;
                    }
                }
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void GenerateToken()
        {
            if (Application.Current.Properties.ContainsKey("UserInfo"))
            {
                GenerateTokenForUser();
            }
            else
            {
                GenerateTokenForAgency();
            }
        }

        private async void GenerateTokenForUser()
        {
            var service = new LoginUserService();
            var userJson = Application.Current.Properties["UserInfo"].ToString();
            var user = JsonConvert.DeserializeObject<UserDetails>(userJson);

            if (Application.Current.Properties.ContainsKey("Password"))
            {
                var password = Application.Current.Properties["Password"].ToString();
                var response = await service.LoginUserAsync(user.UserName, password);

                if (response.Token != null)
                {
                    Application.Current.Properties["RVToken"] = response.Token;
                    Application.Current.Properties["TokenDate"] = DateTime.Now.ToString();
                }
            }
            else
            {
                var response = await service.LoginUserAsync(user.UserName, null, true);

                if(response.Token != null)
                {
                    Application.Current.Properties["RVToken"] = response.Token;
                    Application.Current.Properties["TokenDate"] = DateTime.Now.ToString();
                }

            }
        }
        protected void GenerateTokenForAgency()
        {
            RestClient _client = new RestClient(Constants.AppThomalexServiceUrl);
            string token = null;
            var request = new RestRequest("api/v1/public/token?clientname="+Constants.AppClientName);
            request.AddHeader("Content-Type", "application/json-patch+json");
            request.AddHeader("Accept", "application/json");

            var response = _client.Get(request);
            var responseData = JsonConvert.DeserializeObject<UserToken>(response.Content);

            token = responseData.Token;
            Application.Current.Properties["RVToken"] = token;
            Application.Current.Properties["TokenDate"] = DateTime.Now.ToString();

        }
        private void SetCulture()
        {
            var placeService = new PlaceService();
            var userLocation = placeService.GetPlaceInfoByIp();
            if (Current.Properties.ContainsKey("Culture"))
            {
                var cultureJson = Current.Properties["Culture"].ToString();
                var culture = JsonConvert.DeserializeObject<CultureInfo>(cultureJson);
                AppResources.Culture = culture;
                CrossMultilingual.Current.CurrentCultureInfo = culture;
            }
            else
            {
                if (Application.Current.Properties.ContainsKey("UserLocation"))
                {
                    var placeInfo = JsonConvert.DeserializeObject<UserLocation>(Application.Current.Properties["UserLocation"]?.ToString());
                    var languageCodes = placeInfo?.Languages?.Split(',');
                    var availableLanguages = new List<string> { "en", "de", "ru", "es", "uz", "fr", "uz-Cyrl"};
                    if (languageCodes != null && languageCodes.Length > 0 && availableLanguages.Contains(languageCodes[0]))
                    {
                        var culture = new CultureInfo(languageCodes[0]);
                        AppResources.Culture = culture;
                        CrossMultilingual.Current.CurrentCultureInfo = culture;
                    }
                    else
                    {
                        var culture = CrossMultilingual.Current.DeviceCultureInfo;
                        AppResources.Culture = culture;
                    }

                }
                else
                {
                    var culture = CrossMultilingual.Current.DeviceCultureInfo;
                    AppResources.Culture = culture;
                }
            }
        }
    }
}

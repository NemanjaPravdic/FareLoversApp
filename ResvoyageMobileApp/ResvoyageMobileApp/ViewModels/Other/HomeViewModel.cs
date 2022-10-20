using Newtonsoft.Json;
using Plugin.Multilingual;
using ResvoyageMobileApp.Models.Other;
using ResvoyageMobileApp.Models.User;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Views.Bookings;
using ResvoyageMobileApp.Views.Other;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Other
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            if (!Application.Current.Properties.ContainsKey("UserInfo"))
                _isUserLoggedIn = false;
            else
            {
                _isUserLoggedIn = true;
                var userJson = Application.Current.Properties["UserInfo"].ToString();
                _user = JsonConvert.DeserializeObject<UserDetails>(userJson);
            }

            _languages = new ObservableCollection<Language>()
            {
                new Language { DisplayName =  "English", ShortName = "en" },
                new Language { DisplayName =  "Deutsche - German", ShortName = "de" },
                new Language { DisplayName =  "Français - French", ShortName = "fr" },
                new Language { DisplayName =  "Español - Spanish", ShortName = "es" },
                new Language { DisplayName =  "Русский - Russian", ShortName = "ru" },
                new Language { DisplayName =  "Oʻzbekcha - Uzbek", ShortName = "uz" },
                new Language { DisplayName =  "Ўзбек - Uzbek", ShortName = "uz-Cyrl" },
               // new Language { DisplayName =  "中文 - Chinese (simplified)", ShortName = "zh-Hans" }
            };
            _currency = SelectedCurrency;
        }
        private bool _isUserLoggedIn;

        public bool IsUserLoggedIn
        {
            get { return _isUserLoggedIn; }
            set { SetValue(ref _isUserLoggedIn, value); }
        }
        private double _screenSize;

        public double ScreenSize
        {
            get { return _screenSize; }
            set { SetValue(ref _screenSize, value); }
        }

        private Language _language;
        public Language Language
        {
            get { return _language; }
            set { 
                SetValue(ref _language, value);
                try
                {
                    var culture = new CultureInfo(value.ShortName);
                    var cultureJson = JsonConvert.SerializeObject(culture);
                    Application.Current.Properties["Culture"] = cultureJson;
                    AppResources.Culture = culture;
                    CrossMultilingual.Current.CurrentCultureInfo = culture;
                    Application.Current.MainPage = new ResvoyageMobileApp.Views.HomePage();
                }
                catch (Exception)
                {
                }
            }
        }
        private UserDetails _user;
        public UserDetails User
        {
            get { return _user; }
            set { SetValue(ref _user, value); }
        }
        private ObservableCollection<Language> _languages;
        public ObservableCollection<Language> Languages
        {
            get { return _languages; }
            set { SetValue(ref _languages, value); }
        }

        private CurrencyInfo _currency;
        public CurrencyInfo Currency
        {
            get { return _currency; }
            set
            {
                SetValue(ref _currency, value);
                var currencyJson = JsonConvert.SerializeObject(value);
                Application.Current.Properties["Currency"] = currencyJson;
            }
        }
        public ICommand HomeCommand => new Command(Home);
        public ICommand PickerFocusCommand => new Command<Object>(PickerFocus);
        public ICommand EditProfileCommand => new Command(EditProfile);
        public ICommand MyBookingsCommand => new Command(MyBookings);
        public ICommand UserAgreementCommand => new Command(UserAgreement);
        public ICommand ContactCommand => new Command(Contact);
        public ICommand PrivacyPolicyCommand => new Command(PrivacyPolicy);
        public ICommand LinkClickCommand => new Command<string>(LinkClick);
        public ICommand PhoneDilerCommand => new Command<string>(PhoneDiler);
        public ICommand SendMailCommand => new Command(SendMail);
        public ICommand LoginCommand => new Command(Login);
        public ICommand LogoutCommand => new Command(Logout);

        private void Home()
        {
            Shell.Current.FlyoutIsPresented = false;
        }

        private void PickerFocus(Object obj)
        {
            var picker = (Picker)obj;
            picker.Focus();
            Shell.Current.FlyoutIsPresented = false;
        }
        private async void LinkClick(string link)
        {
            await Browser.OpenAsync(link, BrowserLaunchMode.SystemPreferred);
        }
        private void PhoneDiler(string number)
        {
            PhoneDialer.Open(number);
        }
        private async void SendMail()
        {
            //Device.OpenUri(new Uri("mailto:customerservice@farelovers.com"));
            var message = new EmailMessage
            {
                //Subject = subject,
                //Body = body,
                To = new List<string> { "customerservice@farelovers.com" },
                //Cc = ccRecipients,
                //Bcc = bccRecipients
            };
            await Email.ComposeAsync(message);
        }
        
        private void EditProfile()
        {
            var navigation = Application.Current.MainPage as Shell;

            if (Application.Current.Properties.ContainsKey("UserInfo"))
            {
                var userJson = Application.Current.Properties["UserInfo"].ToString();
                var user = JsonConvert.DeserializeObject<UserDetails>(userJson);
                if (user == null)
                {
                    Shell.Current.FlyoutIsPresented = false;
                    navigation.DisplayAlert(AppResources.APP_ERROR, Resources.AppResources.ER_SOME_ERROR_OCCURED, AppResources.APP_OK);
                }
                Shell.Current.FlyoutIsPresented = false;
                navigation.Navigation.PushAsync(new EditUserPage(user));
            }
            else
            {
                Shell.Current.FlyoutIsPresented = false;
                navigation.DisplayAlert(AppResources.APP_ERROR, Resources.AppResources.ER_SOME_ERROR_OCCURED, AppResources.APP_OK);
            }
        }
        private void MyBookings()
        {
            var navigation = Application.Current.MainPage as Shell;

            if (Application.Current.Properties.ContainsKey("UserInfo"))
            {
                var userJson = Application.Current.Properties["UserInfo"].ToString();
                var user = JsonConvert.DeserializeObject<UserDetails>(userJson);
                if (user == null)
                {
                    Shell.Current.FlyoutIsPresented = false;
                    navigation.DisplayAlert(AppResources.APP_ERROR, Resources.AppResources.ER_SOME_ERROR_OCCURED, AppResources.APP_OK);
                }
                Shell.Current.FlyoutIsPresented = false;
                navigation.Navigation.PushAsync(new MyBookingsPage());
            }
            else
            {
                Shell.Current.FlyoutIsPresented = false;
                navigation.DisplayAlert(AppResources.APP_ERROR, Resources.AppResources.ER_SOME_ERROR_OCCURED, AppResources.APP_OK);
            }
        }
        private void Login()
        {
            Shell.Current.FlyoutIsPresented = false;
            var navigation = Application.Current.MainPage as Shell;
            navigation.Navigation.PushAsync(new LoginPage());
        }
        
        private void PrivacyPolicy()
        {
            Shell.Current.FlyoutIsPresented = false;
            var navigation = Application.Current.MainPage as Shell;
            navigation.Navigation.PushAsync(new PrivacyPolicyPage());
        }
        private void UserAgreement()
        {
            Shell.Current.FlyoutIsPresented = false;
            var navigation = Application.Current.MainPage as Shell;
            navigation.Navigation.PushAsync(new UserAgreementPage());
        }
        private void Contact()
        {
            Shell.Current.FlyoutIsPresented = false;
            var navigation = Application.Current.MainPage as Shell;
            navigation.Navigation.PushAsync(new ContactPage());
        }
        private void Logout()
        {
            Application.Current.Properties.Remove("UserInfo");
            Application.Current.Properties.Remove("Password");
            Application.Current.Properties.Remove("Token");
            Application.Current.Properties["IsLoggedIn"] = false;
            Application.Current.SavePropertiesAsync();

            var navigation = Application.Current.MainPage as Shell;
            navigation.Navigation.PushAsync(new LoginPage());
            Shell.Current.FlyoutIsPresented = false;
        }
    }
}

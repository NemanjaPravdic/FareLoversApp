using ResvoyageMobileApp.ViewModels.Other;
using ResvoyageMobileApp.Views.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResvoyageMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : Shell
    {
        public HomePage()
        {
            InitializeComponent();
            if (!Application.Current.Properties.ContainsKey("IsLoggedIn"))
                Navigation.PushAsync(new LoginPage());

        }
        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);
            var viewModel = new HomeViewModel();
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            viewModel.ScreenSize = Math.Round(mainDisplayInfo.Height / mainDisplayInfo.Density);
            BindingContext = viewModel;
        }
    }
}
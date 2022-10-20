using ResvoyageMobileApp.Models.User;
using ResvoyageMobileApp.ViewModels.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResvoyageMobileApp.Views.Other
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserPage : ContentPage
    {
        public EditUserPage(UserDetails user)
        {
            InitializeComponent();
            BindingContext = new EditUserViewModel(user);
        }
    }
}
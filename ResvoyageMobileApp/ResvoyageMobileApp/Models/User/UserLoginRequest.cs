using ResvoyageMobileApp.Models.Other;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.User
{
    public class UserLoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ClientSiteCode { get; set; } = Constants.AppClientName;
    }
}

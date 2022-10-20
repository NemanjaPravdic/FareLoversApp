using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.User
{
    public class UserLoginResoponse
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
    }
}

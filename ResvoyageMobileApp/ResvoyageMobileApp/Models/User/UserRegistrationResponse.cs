using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.User
{
    public class UserRegistrationResponse
    {
        public bool IsSuccessful { get; set; }
        public UserDetails UserInformation { get; set; }
        public ErrorResult ErrorResult { get; set; }
    }
}

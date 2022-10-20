using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.User
{
    public class UserDetailsResponse
    {
        public UserDetails UserDetails { get; set; }
        public ErrorResult Error { get; set; }
    }
}

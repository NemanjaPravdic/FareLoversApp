using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.Other
{
    public static class Constants
    {
        public static string AppName = "Farelovers";
        public static string AppClientName = "dNPR3lvHD4s";
        public static string AppThomalexServiceUrl = "https://rest.resvoyage.com";

        public static string FacebookScope = "email";
        public static string FacebookAuthorizeUrl = "https://www.facebook.com/dialog/oauth/";
        public static string FacebookAccessTokenUrl = "https://www.facebook.com/connect/login_success.html";
        public static string FacebookUserInfoUrl = "https://graph.facebook.com/me?fields=email&access_token={accessToken}";

        public static string FacebookClientId = "343880090312431";

        // Google OAuth
        // For Google login, configure at https://console.developers.google.com/
        public static string GoogleiOSClientId = "77248674489-7urdhah2m6irgbhujjabj6eoej6jf5v9.apps.googleusercontent.com";
        public static string GoogleAndroidClientId = "77248674489-pmrrtvbj3k3834rmhps7moakatvfmsab.apps.googleusercontent.com";

        // These values do not need changing
        public static string GoogleScope = "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile";
        public static string GoogleAuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string GoogleAccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string GoogleUserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string GoogleiOSRedirectUrl = "com.googleusercontent.apps.77248674489-7urdhah2m6irgbhujjabj6eoej6jf5v9:/oauth2redirect";
        public static string GoogleAndroidRedirectUrl = "com.googleusercontent.apps.77248674489-pmrrtvbj3k3834rmhps7moakatvfmsab:/oauth2redirect";
    }
}

using Newtonsoft.Json;
using RestSharp;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Models.Other;
using ResvoyageMobileApp.Models.User;
using ResvoyageMobileApp.Services.User;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ResvoyageMobileApp.Services
{
    public class BaseService
    {
        protected RestClient _client = new RestClient(Constants.AppThomalexServiceUrl);
        protected async Task<string> GetToken()
        {
            string token = null;
            DateTime? tokenDate = null;

            if (Application.Current.Properties.ContainsKey("TokenDate"))
            {
                string tokenDateString = Application.Current.Properties["TokenDate"].ToString();
                var tmpDate = new DateTime();

                if (DateTime.TryParse(tokenDateString, out tmpDate))
                {
                    tokenDate = tmpDate;
                }
            }

            if(Application.Current.Properties.ContainsKey("RVToken"))
                token = Application.Current.Properties["RVToken"].ToString();

            if (string.IsNullOrEmpty(token) || tokenDate == null || tokenDate.Value.AddHours(2) < DateTime.Now)
            {
                token = await GenerateToken();
            }

            return token;
        }

        private async Task<string> GenerateToken()
        {
            if (Application.Current.Properties.ContainsKey("UserInfo"))
            {
                return await GenerateTokenForUser();
            }
            else
            {
                return await GenerateTokenForAgency();
            }
        }

        private async Task<string> GenerateTokenForUser()
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
                    return response.Token;
                }
                else
                    return string.Empty;
            }
            else
            {
                var response = await service.LoginUserAsync(user.UserName, null, true);

                if (response.Token != null)
                {
                    Application.Current.Properties["RVToken"] = response.Token;
                    Application.Current.Properties["TokenDate"] = DateTime.Now.ToString();
                    return response.Token;
                }
                else
                    return string.Empty;

            }
        }
        private async Task<string> GenerateTokenForAgency()
        {
            RestClient _client = new RestClient(Constants.AppThomalexServiceUrl);
            string token = null;
            var request = new RestRequest("api/v1/public/token?clientname=" + Constants.AppClientName);
            request.AddHeader("Content-Type", "application/json-patch+json");
            request.AddHeader("Accept", "application/json");

            var response = await _client.ExecuteAsync(request);
            var responseData = JsonConvert.DeserializeObject<UserToken>(response.Content);

            token = responseData.Token;
            Application.Current.Properties["RVToken"] = token;
            Application.Current.Properties["TokenDate"] = DateTime.Now.ToString();
            return token;

        }
        protected string ObjToQueryString(object request, string separator = ",")
        {
            if (request == null)
                throw new ArgumentNullException("request");

            // Get all properties on the object
            var properties = request.GetType().GetProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();
            var list = new List<QueryData>();

            foreach (var item in properties)
            {                
                if (!(item.Value is int[]))
                {
                    list.Add(new QueryData { Key = item.Key, Value = item.Value.ToString() });
                }
            }

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType
                                        ? valueType.GetGenericArguments()[0]
                                        : valueType.GetElementType();
                if (valueElemType.IsPrimitive || valueElemType == typeof(string))
                {
                    
                    var enumerable = properties[key] as IEnumerable;
                    //properties[key] = string.Join(separator, enumerable.Cast<object>());
                    
                    foreach (var item in enumerable)
                    {
                        list.Add(new QueryData() { Key = key, Value = item.ToString() });

                    }
                }
            }

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join("&", list
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value.ToString()))));
        }
    }
    public class QueryData
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}

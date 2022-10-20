using Newtonsoft.Json;
using RestSharp;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Models.Other;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ResvoyageMobileApp.Services
{
    public class CurrencyConversionService : BaseService
    {
        public async Task<decimal> CurrencyConversion(string fromCurrency, string toCurrency)
        {
            var token = await GetToken();
            var restRequest = new RestRequest("/api/v1/currency/getcurrencyrate?from=" + fromCurrency+"&to="+toCurrency);
            restRequest.AddHeader("Content-Type", "application/json-patch+json");
            restRequest.AddHeader("Authorization", "Bearer " + token);
            restRequest.AddHeader("Accept", "application/json");

            var restResut = _client.Execute(restRequest);
            var response = new GetCurrencyRateResponse();
            if (restResut.IsSuccessful && restResut.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response = JsonConvert.DeserializeObject<GetCurrencyRateResponse>(restResut.Content);
                return response.Rate;
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorResult>(restResut.Content);
                return 1;
            }
        }
    }
}

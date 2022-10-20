using Newtonsoft.Json;
using RestSharp;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Models.Flight;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ResvoyageMobileApp.Services.Flight
{
    public class FareRulesService : BaseService
    {
        public async Task<FareRulesResponse> GetFareRules(Guid sessionId, Guid flightId)
        {
            var token = await GetToken();
            var restRequest = new RestRequest("/api/v1/air/references/flight/rules?sessionid=" + sessionId.ToString() + "&itineraryid=" + flightId.ToString());
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", "Bearer " + token);
            IRestResponse restResut = await _client.ExecuteAsync(restRequest);
            var response = new FareRulesResponse();
            if (restResut.IsSuccessful && restResut.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response = JsonConvert.DeserializeObject<FareRulesResponse>(restResut.Content);
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorResult>(restResut.Content);
                response.Error = error;
            }

            return response;
        }
    }
}

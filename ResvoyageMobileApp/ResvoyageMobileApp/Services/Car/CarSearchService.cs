using Newtonsoft.Json;
using RestSharp;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Models.Car;
using ResvoyageMobileApp.ViewModels.Car;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ResvoyageMobileApp.Services.Car
{
    public class CarSearchService : BaseService
    {
        public async Task<CarSearchResponse> GetCarResponseAsync(CarRequestViewModel request)
        {
            var token = await GetToken();
            var url = "api/v1/car/search?" + PrepareCareRequestForURL(request);
            var restRequest = new RestRequest(url);
            restRequest.AddHeader("Content-Type", "application/json-patch+json");
            restRequest.AddHeader("Authorization", "Bearer " + token);
            restRequest.AddHeader("Accept", "application/json");

            var restResut = await _client.ExecuteAsync(restRequest);
            var response = new CarSearchResponse();
            if (restResut.IsSuccessful && restResut.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response = JsonConvert.DeserializeObject<CarSearchResponse>(restResut.Content);
                /*var organizedFlights = OrganizeFlightResponse(flights);
                response.FlightList = organizedFlights;*/
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorResult>(restResut.Content);
                response.Error = error;
            }
            return response;
        }

        private string PrepareCareRequestForURL(CarRequestViewModel request)
        {
            var response = new CarSearchRequest();

            response.PickupCity = request.PickupCityIata;
            response.PickupDate = request.PickupDate;
            response.DropOffCity = request.DropOffCityIata;
            response.DropOffDate = request.DropOffDate;

            if (!string.IsNullOrEmpty(request.DropoffTime))
            {
                int dropoffTime;
                if (Int32.TryParse(request.DropoffTime.Split(':')[0], out dropoffTime))
                    response.DropoffTime = dropoffTime;
            }
            if (!string.IsNullOrEmpty(request.PickupTime))
            {
                int pickupTime;
                if (Int32.TryParse(request.PickupTime.Split(':')[0], out pickupTime))
                    response.PickupTime = pickupTime;
            }

            return ObjToQueryString(response);

        }
    }
}

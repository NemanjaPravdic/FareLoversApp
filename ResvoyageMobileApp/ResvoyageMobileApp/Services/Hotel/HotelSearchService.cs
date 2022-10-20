using Newtonsoft.Json;
using RestSharp;
using ResvoyageMobileApp.Models;
using ResvoyageMobileApp.Models.Hotel;
using ResvoyageMobileApp.ViewModels.Hotel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ResvoyageMobileApp.Services.Hotel
{
    public class HotelSearchService : BaseService
    {
        public async Task<HotelSearchResponse> GetHotelResponseAsync(HotelRequestViewModel requestViewModel)
        {
            var token = await GetToken();
            var url = "api/v1/hotel/search?" + PrepareHotelRequestForURL(requestViewModel);
            var restRequest = new RestRequest(url);
            restRequest.AddHeader("Content-Type", "application/json-patch+json");
            restRequest.AddHeader("Authorization", "Bearer " + token);
            restRequest.AddHeader("Accept", "application/json");

            var restResut = await _client.ExecuteAsync(restRequest);
            var response = new HotelSearchResponse();
            if (restResut.IsSuccessful && restResut.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response = JsonConvert.DeserializeObject<HotelSearchResponse>(restResut.Content);
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

        private string PrepareHotelRequestForURL(HotelRequestViewModel requestViewModel)
        {
            var newRequest = new HotelSearchRequest()
            { 
                RoomCount = requestViewModel.NoOfRooms,
                CheckInDate = requestViewModel.CheckInDate,
                CheckOutDate = requestViewModel.CheckOutDate,
                PlaceId = requestViewModel.PlaceId               
            };

            if (string.IsNullOrEmpty(newRequest.PlaceId))
                newRequest.HotelCityCode = requestViewModel.HotelCityCode;

            if (requestViewModel.RoomsInfo != null)
            {
                if (requestViewModel.RoomsInfo[0] != null)
                {
                    newRequest.Children = requestViewModel.RoomsInfo[0].Children;
                    newRequest.Adults = requestViewModel.RoomsInfo[0].Adults;
                    newRequest.ChildrenAge = new int[requestViewModel.RoomsInfo[0].ChildrenAge.Count];
                    for (int i = 0; i < requestViewModel.RoomsInfo[0].ChildrenAge.Count; i++)
                    {
                        newRequest.ChildrenAge[i] = requestViewModel.RoomsInfo[0].ChildrenAge[i].Age;
                    }
                }
                if (requestViewModel.NoOfRooms > 1)
                {
                    newRequest.Children2 = requestViewModel.RoomsInfo[1].Children;
                    newRequest.Adults2 = requestViewModel.RoomsInfo[1].Adults;
                    newRequest.ChildrenAge2 = new int[requestViewModel.RoomsInfo[1].ChildrenAge.Count];
                    for (int i = 0; i < requestViewModel.RoomsInfo[1].ChildrenAge.Count; i++)
                    {
                        newRequest.ChildrenAge2[i] = requestViewModel.RoomsInfo[1].ChildrenAge[i].Age;
                    }
                }
                if (requestViewModel.NoOfRooms > 2)
                {
                    newRequest.Children3 = requestViewModel.RoomsInfo[2].Children;
                    newRequest.Adults3 = requestViewModel.RoomsInfo[2].Adults;
                    newRequest.ChildrenAge3 = new int[requestViewModel.RoomsInfo[2].ChildrenAge.Count];
                    for (int i = 0; i < requestViewModel.RoomsInfo[2].ChildrenAge.Count; i++)
                    {
                        newRequest.ChildrenAge3[i] = requestViewModel.RoomsInfo[2].ChildrenAge[i].Age;
                    }
                }
                if (requestViewModel.NoOfRooms > 3)
                {
                    newRequest.Children4 = requestViewModel.RoomsInfo[3].Children;
                    newRequest.Adults4 = requestViewModel.RoomsInfo[3].Adults;
                    newRequest.ChildrenAge4 = new int[requestViewModel.RoomsInfo[3].ChildrenAge.Count];
                    for (int i = 0; i < requestViewModel.RoomsInfo[3].ChildrenAge.Count; i++)
                    {
                        newRequest.ChildrenAge4[i] = requestViewModel.RoomsInfo[3].ChildrenAge[i].Age;
                    }
                }
            }

            return ObjToQueryString(newRequest);
        }
    }
}

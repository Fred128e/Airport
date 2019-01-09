using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Airport.MVC.Data;
using Airport.MVC.Models;
using Newtonsoft.Json;
using AutoMapper;

namespace Airport.MVC.Services
{
    public class FlightRepository : IFlightRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly FlightDbContext _context;
        private const string baseUrl = "https://localhost:44371/";

        public FlightRepository(IHttpClientFactory httpClient, FlightDbContext context)
        {
            _httpClientFactory = httpClient;
            _context = context;
        }
        //https://localhost:44371/api/flights/search

        public async Task<IEnumerable<FlightViewModel>> SearchFlight(FlightViewModel flight)
        {
            List<FlightViewModel> flights = null;
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PostAsJsonAsync($"{baseUrl}api/flights/search",flight);
            if(response.IsSuccessStatusCode)
            {
              flights= JsonConvert.DeserializeObject<List<FlightViewModel>>(await response.Content.ReadAsStringAsync());
            }

            return flights;
        }
        public async Task<IEnumerable<FlightViewModel>> GetAllFlights()
        {
            List<FlightViewModel> flight = null;
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient
                .GetAsync($"{baseUrl}api/flights/");
            if(response.IsSuccessStatusCode)
            {
                flight = JsonConvert.DeserializeObject<List<FlightViewModel>>(await response.Content.ReadAsStringAsync());
            }

            return flight;
        }

        public async Task<IEnumerable<FlightViewModel>> GetFlight(int id)
        {
            List<FlightViewModel> flight = null;
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"{baseUrl}api/{id}/");

            if(response.IsSuccessStatusCode)
            {
                flight = JsonConvert.DeserializeObject<List<FlightViewModel>>(await response.Content.ReadAsStringAsync());
            }

            return flight;
        }

        public async Task AddFlight(FlightViewModel flight)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PostAsJsonAsync($"{baseUrl}api/flights",flight);
            if(response.IsSuccessStatusCode)
            {
                JsonConvert.DeserializeObject<List<FlightViewModel>>(await response.Content.ReadAsStringAsync());
            }

        }

        public async Task<bool> UpdateFlight(int id)
        {
            var url = $"{baseUrl}api/flights/edit/{id}";
            var httpClient = _httpClientFactory.CreateClient();
            string json = JsonConvert.SerializeObject(id);

            var content = new StringContent(json,UnicodeEncoding.UTF8,"application/json");
            var response = await httpClient.PostAsync(url, content);

            return response.IsSuccessStatusCode;

            //var httpClient = _httpClientFactory.CreateClient();
            //var response = await httpClient.PutAsJsonAsync($"{baseUrl}api/flights{id}",id);
            //if(response.IsSuccessStatusCode)
            //{
            //    JsonConvert.DeserializeObject<List<FlightViewModel>>(await response.Content.ReadAsStringAsync());
            //}

        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

    }
}

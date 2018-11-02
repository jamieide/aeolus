using Aeolus.ApiClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aeolus.Web.AeolusApiService
{
    public class AeolusApiService : IAeolusApiService
    {
        private readonly ILogger<AeolusApiService> _logger;
        private readonly HttpClient _httpClient;

        public AeolusApiService(ILogger<AeolusApiService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<Station[]> GetStationsForState(string state)
        {
            var uri = $"api/nws/stations?state={state}";
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var stations = JsonConvert.DeserializeObject<Station[]>(await response.Content.ReadAsStringAsync());
            return stations;
        }

        public async Task<StationObservations[]> GetStationObservationsForState(string state, DateTime start, DateTime end)
        {
            var stations = await GetStationsForState(state);
            var tasks = new List<Task<StationObservations>>();
            foreach (var station in stations)
            {
                tasks.Add(GetStationObservations(station.Identifier, start, end));
            }

            var results = await Task.WhenAll(tasks);
            return results;
        }

        public async Task<StationObservations> GetStationObservations(string stationIdentifier, DateTime start, DateTime end)
        {
            var uri = $"api/nws/stations/{stationIdentifier}/observations?start={start.ToString("yyyy-MM-dd")}&end={end.ToString("yyyy-MM-dd")}";
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<StationObservations>(await response.Content.ReadAsStringAsync());
        }
    }
}
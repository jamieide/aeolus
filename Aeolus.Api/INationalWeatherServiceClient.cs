using System;
using System.Threading.Tasks;
using Aeolus.ApiClient;

namespace Aeolus.Api
{
    /// <summary>
    /// Interface for NWS API client.
    /// </summary>
    public interface INationalWeatherServiceClient
    {
        Task<Station[]> FindStations(double latitude, double longitude);
        Task<Station> GetStation(string stationIdentifier);
        Task<Station[]> GetStationsForState(string state);
        Task<Observation[]> GetObservations(string stationIdentifier, DateTime start, DateTime end);
        Task<StationObservations> GetStationObservations(string stationIdentifier, DateTime start, DateTime end);
    }
}
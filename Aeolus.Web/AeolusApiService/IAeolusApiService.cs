using System;
using System.Threading.Tasks;
using Aeolus.ApiClient;

namespace Aeolus.Web.AeolusApiService
{
    public interface IAeolusApiService
    {
        Task<Station[]> GetStationsForState(string state);
        Task<StationObservations[]> GetStationObservationsForState(string state, DateTime start, DateTime end);
        Task<StationObservations> GetStationObservations(string stationIdentifier, DateTime start, DateTime end);
    }
}
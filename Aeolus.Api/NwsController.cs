using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Aeolus.ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aeolus.Api
{
    [ApiController]
    [Route("api/nws")]
    public class NwsController : Controller
    {
        private readonly ILogger<NwsController> _logger;
        private readonly INationalWeatherServiceClient _nwsClient;

        public NwsController(ILogger<NwsController> logger, INationalWeatherServiceClient nwsClient)
        {
            _logger = logger;
            _nwsClient = nwsClient;
        }

        [HttpGet("echo")]
        public async Task<string> Echo()
        {
            return await Task.FromResult("echo");
        }

        [HttpGet("stations/find")]
        public async Task<IActionResult> FindStations(double lat, double lon)
        {
            // could use viewmodel with validation attributes but that doesn't work for observation endpoints
            // because it binds from both route and querystring.

            if (lat < -90 || lat > 90)
            {
                ModelState.AddModelError("lat", "Latitude must be between -90 and 90.");
            }

            if (lon < -180 || lon > 180)
            {
                ModelState.AddModelError("lon", "Longitude must be between -180 and 180.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stations = await _nwsClient.FindStations(lat, lon);
            return Json(stations);
        }

        [HttpGet("stations/{stationIdentifier:alpha}")]
        public async Task<IActionResult> GetStation(string stationIdentifier)
        {
            var station = await _nwsClient.GetStation(stationIdentifier);
            if (station == null)
            {
                return NotFound();
            }

            return Json(station);
        }

        [HttpGet("stations/{stationIdentifier:alpha}/observations")]
        public async Task<IActionResult> GetObservations(string stationIdentifier, DateTime start, DateTime end)
        {
            if (end < start)
            {
                ModelState.AddModelError("end", "End data must be after start date.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stationObservations = await _nwsClient.GetStationObservations(stationIdentifier, start, end);
            if (stationObservations == null)
            {
                return NotFound();
            }
            return Json(stationObservations);
        }
    }
}
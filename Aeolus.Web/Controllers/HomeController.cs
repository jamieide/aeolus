using System;
using System.Threading.Tasks;
using Aeolus.Web.AeolusApiService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aeolus.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAeolusApiService _aeolusApiService;

        public HomeController(ILogger<HomeController> logger, IAeolusApiService aeolusApiService)
        {
            _logger = logger;
            _aeolusApiService = aeolusApiService;
        }

        public async Task<IActionResult> Index()
        {
            var start = new DateTime(2018, 11, 1);
            var end = new DateTime(2018, 11, 2);
            var stationObservations = await _aeolusApiService.GetStationObservationsForState("vt", start, end);
            return await Task.FromResult(View());
        }
    }
}
using System;
using System.Threading.Tasks;
using Aeolus.Web.AeolusApiService;
using Aeolus.Web.Analysis;
using Aeolus.Web.Models;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(View());
        }

        [HttpGet]
        public async Task<IActionResult> GetStationList(string state)
        {
            var stations = await _aeolusApiService.GetStationsForState(state);
            return PartialView("_StationList", stations);
        }

        [HttpPost]
        public async Task<IActionResult> GetAnalysis([FromBody]AnalysisRequestViewModel vm)
        {
            var stationObservations = await _aeolusApiService.GetStationObservations(vm.StationIdentifier, vm.Start, vm.End);
            var factory = new WindSpeedBinFactory();
            var bins = factory.Create(stationObservations, vm.Strategy);
            var analysis = new PowerGenerationAnalysis(stationObservations.Station, bins, vm);
            return PartialView("_AnalysisResults", analysis);
        }
    }
}
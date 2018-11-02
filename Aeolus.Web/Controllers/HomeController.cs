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
            return await Task.FromResult(View());
        }

        public async Task<IActionResult> StationList(string state)
        {
            var stations = await _aeolusApiService.GetStationsForState(state);
            return PartialView("_StationList", stations);
        }
    }
}
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Aeolus.Web.AeolusApiService;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aeolus.Web.Tests
{
    [TestClass]
    public class AeolusApiClientTests
    {
        private static HttpClient _apiClient;
        private static IAeolusApiService _apiService;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _apiClient = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost")
            };
            _apiService = new AeolusApiService.AeolusApiService(new NullLogger<AeolusApiService.AeolusApiService>(), _apiClient);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _apiClient?.Dispose();
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            var stations = await _apiService.GetStationsForState("vt");
            Assert.IsTrue(stations.Any());
        }
    }
}

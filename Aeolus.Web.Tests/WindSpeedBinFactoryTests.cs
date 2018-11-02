using Aeolus.ApiClient;
using Aeolus.Web.Analysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Aeolus.Web.Tests
{
    [TestClass]
    public class WindSpeedBinFactoryTests
    {
        [TestMethod]
        public void StationAndDateCorrect()
        {
            var stationObservations = CreateTestData();

            var factory = new WindSpeedBinFactory();
            var target = factory.Create(stationObservations, NormalizeWindSpeedStrategy.AverageWindSpeed);
            Assert.AreEqual(1, target.Length);
            Assert.AreEqual("TEST", target[0].StationIdentifier);
            Assert.AreEqual(new DateTime(2018, 11, 2), target[0].Date);
        }

        [TestMethod]
        public void NormalizeWindSpeedStrategyMin()
        {
            var stationObservations = CreateTestData();

            var factory = new WindSpeedBinFactory();
            var target = factory.Create(stationObservations, NormalizeWindSpeedStrategy.AverageWindSpeed);
            Assert.AreEqual(new[] { 3, 18, 9 }.Average(), target[0].NormalizedWindSpeed);
        }

        [TestMethod]
        public void NormalizedWindSpeedStrategyMinNotChangedByNull()
        {
            var stationObservations = CreateTestData();
            stationObservations.Observations.Append(new Observation()
            {
                Timestamp = new DateTime(2018, 11, 2, 0, 30, 0, DateTimeKind.Utc),
                WindSpeed = null
            });

            var factory = new WindSpeedBinFactory();
            var target = factory.Create(stationObservations, NormalizeWindSpeedStrategy.AverageWindSpeed);
            Assert.AreEqual(new[] { 3, 18, 9 }.Average(), target[0].NormalizedWindSpeed);
        }

        [TestMethod]
        public void NormalizeWindSpeedStrategyMax()
        {
            var stationObservations = CreateTestData();

            var factory = new WindSpeedBinFactory();
            var target = factory.Create(stationObservations, NormalizeWindSpeedStrategy.MaxWindSpeed);
            Assert.AreEqual(new[] { 3, 18, 9 }.Max(), target[0].NormalizedWindSpeed);
        }

        private static StationObservations CreateTestData()
        {
            var stationObservations = new StationObservations
            {
                Station = new Station()
                {
                    Identifier = "TEST"
                },
                Observations = new Observation[]
                {
                    new Observation()
                    {
                        Timestamp = new DateTime(2018, 11, 2, 0, 5, 0, DateTimeKind.Utc),
                        WindSpeed = 3
                    },
                    new Observation()
                    {
                        Timestamp = new DateTime(2018, 11, 2, 0, 10, 0, DateTimeKind.Utc),
                        WindSpeed = 18
                    },
                    new Observation()
                    {
                        Timestamp = new DateTime(2018, 11, 2, 0, 20, 0, DateTimeKind.Utc),
                        WindSpeed = 9
                    },
                }
            };
            return stationObservations;
        }
    }
}
using System;
using System.Linq;
using Aeolus.ApiClient;
using Aeolus.Web.Models;

namespace Aeolus.Web.Analysis
{
    public class PowerGenerationAnalysis
    {

        public PowerGenerationAnalysis(Station station, WindSpeedBin[] windSpeedBins, AnalysisRequestViewModel analysisRequest)
        {
            Station = station;
            Start = windSpeedBins.Min(x => x.Date);
            End = windSpeedBins.Max(x => x.Date);
            BinCount = windSpeedBins.Length;

            // https://www.windpowerengineering.com/construction/calculate-wind-power-output/
            https://www.ajdesigner.com/phpwindpower/wind_generator_power.php
            AverageWindSpeed = windSpeedBins.Average(x => x.NormalizedWindSpeed) ?? 0;
            var rotorSweptArea = Math.PI * Math.Pow(analysisRequest.RotorRadius, 2);

            EstimatedKw = (0.5 * analysisRequest.AirDensity * rotorSweptArea * analysisRequest.PerformanceCoefficient * Math.Pow(AverageWindSpeed, 3))/1000;
            EstimatedKwh = (End - Start).TotalHours * EstimatedKw;
        }

        public Station Station { get; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int BinCount { get; set; }
        public double AverageWindSpeed { get; set; }
        public double EstimatedKw { get; set; }
        public double EstimatedKwh { get; set; }
    }
}
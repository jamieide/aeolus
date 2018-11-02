using System;
using Aeolus.Web.Analysis;

namespace Aeolus.Web.Models
{
    public class AnalysisRequestViewModel
    {
        public string StationIdentifier { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double AirDensity { get; set; }
        public double RotorRadius { get; set; }
        public double PerformanceCoefficient { get; set; }
        public NormalizeWindSpeedStrategy Strategy { get; set; }
    }
}
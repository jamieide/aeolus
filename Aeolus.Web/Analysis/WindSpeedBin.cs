using System;
using Aeolus.ApiClient;

namespace Aeolus.Web.Analysis
{
    public class WindSpeedBin
    {
        public WindSpeedBin(string stationIdentifier, DateTime date, double? normalizedWindSpeed)
        {
            StationIdentifier = stationIdentifier;
            Date = date;
            NormalizedWindSpeed = normalizedWindSpeed;
        }

        public string StationIdentifier { get;  }
        /// <summary>
        /// Date contains year, month, day, hour only
        /// </summary>
        public DateTime Date { get; }
        public double? NormalizedWindSpeed { get;  }
    }
}
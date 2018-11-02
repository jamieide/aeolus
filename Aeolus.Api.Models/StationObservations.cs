using System;
using System.Linq;

namespace Aeolus.ApiClient
{
    /// <summary>
    /// Primary class for displaying combined station and observations data.
    /// </summary>
    public class StationObservations
    {
        public StationObservations()
        { }

        public StationObservations(Station station, Observation[] observations)
        {
            Station = station;
            Observations = observations;
        }

        public Station Station { get; set; }
        public Observation[] Observations { get; set; }
    }
}
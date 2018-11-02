using System;
using System.Linq;

namespace Aeolus.ApiClient
{
    /// <summary>
    /// Primary class for displaying combined station and observations data.
    /// </summary>
    public class StationObservations
    {
        public StationObservations(Station station, Observation[] observations)
        {
            Station = station;
            Observations = observations;
        }

        public Station Station { get; }
        public Observation[] Observations { get; }
    }
}
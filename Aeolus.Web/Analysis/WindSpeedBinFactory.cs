using System;
using System.Collections.Generic;
using System.Linq;
using Aeolus.ApiClient;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2;

namespace Aeolus.Web.Analysis
{
    public class WindSpeedBinFactory
    {
        public WindSpeedBin[] Create(StationObservations stationObservations, NormalizeWindSpeedStrategy normalizeWindSpeedStrategy)
        {
            var stationIdentifier = stationObservations.Station.Identifier;
            var grps = stationObservations.Observations
                .GroupBy(x => new DateTime(x.Timestamp.Year, x.Timestamp.Month, x.Timestamp.Day, x.Timestamp.Hour, 0, 0, DateTimeKind.Utc));
            var date = grps.Min(x => x.Key);
            var maxDate = grps.Max(x => x.Key);

            var bins = new List<WindSpeedBin>();
            while (date <= maxDate)
            {
                double? normalizedWindSpeed;
                var grp = grps.SingleOrDefault(x => x.Key == date);
                if (grp == null)
                {
                    normalizedWindSpeed = null;
                }
                else
                {
                    var strategy = GetStrategyFunc(normalizeWindSpeedStrategy);
                    normalizedWindSpeed = strategy(grp);
                }
                var bin = new WindSpeedBin(stationIdentifier, date, normalizedWindSpeed);
                bins.Add(bin);
                date = date.AddHours(1);
            }

            return bins.ToArray();
        }

        private static Func<IGrouping<DateTime, Observation>, double?> GetStrategyFunc(NormalizeWindSpeedStrategy strategy)
        {
            switch (strategy)
            {
                case NormalizeWindSpeedStrategy.MaxWindSpeed:
                    return x => x.Max(y => y.WindSpeed);
                case NormalizeWindSpeedStrategy.AverageWindSpeed:
                    return x => x.Average(y => y.WindSpeed);
                default:
                    throw new ArgumentOutOfRangeException(strategy.ToString());
            }
        }
    }
}
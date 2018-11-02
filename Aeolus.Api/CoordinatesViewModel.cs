using System.ComponentModel.DataAnnotations;

namespace Aeolus.Api
{
    /// <summary>
    /// Latitude & longitude pair view model for validating input
    /// </summary>
    public class CoordinatesViewModel
    {
        [Range(-90, 90)]
        public double Lat { get; set; }

        [Range(-180, 180)]
        public double Lon { get; set; }
    }
}
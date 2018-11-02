using System;
using System.ComponentModel.DataAnnotations;

namespace Aeolus.Api
{
    public class ObservationsRequestViewModel
    {
        [Required]
        public string StationIdentifier { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
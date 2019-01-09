using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Airport.MVC.Models
{
    public class FlightViewModel
    {
        [Key]
        public int FlightId { get; set; }
        public string AirCraftType { get; set; }

        public string FromLocation { get; set; }

        public string ToLocation { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }
    }
}

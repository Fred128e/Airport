using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Airport.Models
{
    public partial class Flight
    {
        public int FlightId { get; set; }
        public string AirCraftType { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}

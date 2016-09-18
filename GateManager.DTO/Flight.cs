using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GateManager.DTO
{
    public enum FlightStatus
    {
        Active,
        Conflict,
        Cancelled
    }

    public class Flight
    {
        [Required]
        public string FlightCode { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ArrivalTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DepartureTime { get; set; }

        public FlightStatus Status { get; set; }
    }
}

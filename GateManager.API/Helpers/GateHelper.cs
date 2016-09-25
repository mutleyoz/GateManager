using GateManager.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GateManager.API.Helpers
{
    public static class GateHelper
    {
        public static bool HasGateConflict(this List<Flight> flights, Flight flight)
        {
            bool result = false;

            var activeFlights = flights.Where(f => f.Status != FlightStatus.Cancelled).ToList();
            activeFlights.ForEach( f =>
            {
                if ( f.FlightCode != flight.FlightCode && ((flight.ArrivalTime >= f.ArrivalTime && flight.ArrivalTime <= f.DepartureTime) || 
                                                           (flight.DepartureTime >= f.ArrivalTime && flight.DepartureTime <= f.DepartureTime) ||
                                                           (flight.ArrivalTime <= f.ArrivalTime && flight.DepartureTime >= f.DepartureTime)))
                {
                    result =  true;
                }
            });

            return result;
        }
    }
}
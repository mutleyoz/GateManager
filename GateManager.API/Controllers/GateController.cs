using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GateManager.DTO;
using GateManager.Repository;
using GateManager.API.Helpers;
using System.Web.Http.Cors;

namespace GateManager.API.Controllers
{
    [RoutePrefix("api")]
    [EnableCors(origins: "http://localhost:63342", headers: "*", methods: "*")]
    public class GateController : ApiController
    {
        private IGateRepository _repository;

        public GateController(IGateRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("gates/{gatenumber}/flights")]
        public async Task<IHttpActionResult> GetFlights(int gatenumber)
        {
            return Ok(await _repository.GetFlights(gatenumber));
        }

        [HttpGet]
        [Route("gates/{gatenumber}/flights/{flightcode}")]
        public async Task<IHttpActionResult> GetFlights(int gatenumber, string flightcode)
        {
            var flights = await _repository.GetFlights(gatenumber);
            if (flights.Any(f => f.FlightCode == flightcode))
            {
                return Ok(flights.First(f => f.FlightCode == flightcode));
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        [Route("gates/{gatenumber}/flights")]
        public async Task<IHttpActionResult> CreateFlight(int gatenumber, Flight flight)
        {
            if (ModelState.IsValid)
            {
                var currentFlights = await _repository.GetFlights(gatenumber);
                if (currentFlights.HasGateConflict(flight))
                {
                    flight.Status = FlightStatus.Conflict;
                }

                await _repository.CreateFlight(gatenumber, flight);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("gates/{gatenumber}/flights")]
        public async Task<IHttpActionResult> UpdateFlight(int gatenumber, Flight flight)
        {
            if (ModelState.IsValid)
            {
                if (flight.ArrivalTime < flight.DepartureTime)
                {
                    var currentFlights = await _repository.GetFlights(gatenumber);
                    flight.Status = currentFlights.HasGateConflict(flight) ? FlightStatus.Conflict : FlightStatus.Scheduled;

                    await _repository.UpdateFlight(gatenumber, flight);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("gates/{gatenumber}/flights/{flightcode}/gate/{targetgatenumber}")]
        public async Task<IHttpActionResult> MoveFlight(int gatenumber, string flightcode, int targetgatenumber)
        {
            var flights = await _repository.GetFlights(gatenumber);
            if (flights != null)
            {
                var flight = flights.FirstOrDefault(f => f.FlightCode == flightcode);
                if (flight != null)
                {
                    var targetGateFlights = await _repository.GetFlights(targetgatenumber);
                    if (targetGateFlights.HasGateConflict(flight))
                    {
                        flight.Status = FlightStatus.Conflict;
                    }
                    else
                    {
                        flight.Status = FlightStatus.Scheduled;
                    }
                    await _repository.MoveFlight(gatenumber, flight, targetgatenumber);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("gates/{gatenumber}/flights/{flightcode}/bump")]
        public async Task<IHttpActionResult> BumpFlights(int gatenumber, string flightcode)
        {
            var flights = await _repository.GetFlights(gatenumber);
            if (flights != null)
            {
                // Updating all flights after the flight in conflict
                var conflictFlightIndex = flights.FindIndex(f => f.FlightCode == flightcode) + 1;
                var conflictFlight = flights.FirstOrDefault(f => f.FlightCode == flightcode);

                if (conflictFlightIndex >= 0 && conflictFlight != null)
                {
                    // Clear the conflict status
                    conflictFlight.Status = FlightStatus.Scheduled;
                    await _repository.UpdateFlight(gatenumber, conflictFlight);

                    // Iterate through remaining flights on this gate and bump them (if required).
                    for (int i = conflictFlightIndex; i < flights.Count(); i++)
                    {
                        var currentFlight = flights[i];
                        var previousFlight = flights[i - 1];

                        TimeSpan gateDuration = currentFlight.DepartureTime - currentFlight.ArrivalTime;
                        if (flights.HasGateConflict(currentFlight))
                        {
                            currentFlight.ArrivalTime = previousFlight.DepartureTime.AddMinutes(1);
                            currentFlight.DepartureTime = currentFlight.ArrivalTime.Add(gateDuration);

                            await _repository.UpdateFlight(gatenumber, currentFlight);
                        }
                    }
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("gates/{gatenumber}/flights/{flightcode}")]
        public async Task<IHttpActionResult> CancelFlight(int gatenumber, string flightcode)
        {
            var flights = await _repository.GetFlights(gatenumber);
            if (flights != null)
            {
                var flight = flights.FirstOrDefault(f => f.FlightCode == flightcode);
                if (flight != null)
                {
                    flight.Status = FlightStatus.Cancelled;
                    await _repository.UpdateFlight(gatenumber, flight);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("gates/{gatenumber}/flights/{flightcode}/alternate")]
        public async Task<IHttpActionResult> GetAlternateGates(int gatenumber, string flightcode)
        {
            var selectedGateFlights = await _repository.GetFlights(gatenumber);
            var selectedFlight = selectedGateFlights.FirstOrDefault(f => f.FlightCode == flightcode);
            List<Gate> availableGates = new List<Gate>();

            if (selectedFlight != null)
            {
                var gates = await _repository.GetGates();

                foreach (var gate in gates)
                {
                    if (gate.GateNumber != gatenumber)
                    {
                        var flights = await _repository.GetFlights(gate.GateNumber);
                        if (!flights.HasGateConflict(selectedFlight))
                        {
                            availableGates.Add(gate);
                        }
                    }

                }
            }
            else
            {
                return NotFound();
            }

            return Ok(availableGates);
        }

        [HttpGet]
        [Route("gates")]
        public async Task<IHttpActionResult> GetAllGates()
        {
            return Ok(await _repository.GetGates());
        }


    }
}

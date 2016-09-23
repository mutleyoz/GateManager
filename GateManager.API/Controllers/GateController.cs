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
                var currentFlights = await _repository.GetFlights(gatenumber);
                if (currentFlights.HasGateConflict(flight))
                {
                    flight.Status = FlightStatus.Conflict;
                }

                await _repository.UpdateFlight(gatenumber, flight);
                return Ok();
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
                        flight.Status = FlightStatus.Active;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GateManager.DTO;

namespace GateManager.Repository
{
    public class GateFlights
    {
        public Gate Gate { get; set; }

        public List<Flight> Flights { get; set; }
    }

    public class GateRepository : IGateRepository
    {
        private static List<GateFlights> _gateFlights;

        private static object _padlock = new object();

        public GateRepository()
        {
            _gateFlights = new List<GateFlights>()
            {
                new GateFlights
                {
                    Gate = new Gate { GateNumber = 23 },
                    Flights = new List<Flight>
                    {
                        new Flight{ FlightId = 1, FlightCode = "QF183", ArrivalTime = new DateTime(2016, 9, 9, 9, 0, 0), DepartureTime = new DateTime(2016, 9, 9, 9, 29, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 2, FlightCode = "LF987", ArrivalTime = new DateTime(2016, 9, 9, 9, 30, 0), DepartureTime = new DateTime(2016, 9, 9, 9, 59, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 3, FlightCode = "NZ992", ArrivalTime = new DateTime(2016, 9, 9, 10, 0, 0), DepartureTime = new DateTime(2016, 9, 9, 10, 29, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 4, FlightCode = "PK129", ArrivalTime = new DateTime(2016, 9, 9, 12, 0, 0), DepartureTime = new DateTime(2016, 9, 9, 12, 29, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 5, FlightCode = "QF239", ArrivalTime = new DateTime(2016, 9, 9, 14, 0, 0), DepartureTime = new DateTime(2016, 9, 9, 14, 29, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 6, FlightCode = "PK221", ArrivalTime = new DateTime(2016, 9, 9, 14, 30, 0), DepartureTime = new DateTime(2016, 9, 9, 14, 59, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 7, FlightCode = "RN982", ArrivalTime = new DateTime(2016, 9, 9, 16, 0, 0), DepartureTime = new DateTime(2016, 9, 9, 16, 29, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 8, FlightCode = "PK556", ArrivalTime = new DateTime(2016, 9, 9, 17, 0, 0), DepartureTime = new DateTime(2016, 9, 9, 17, 29, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 9, FlightCode = "FJ001", ArrivalTime = new DateTime(2016, 9, 9, 17, 30, 0), DepartureTime = new DateTime(2016, 9, 9, 15, 59, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 10, FlightCode = "FJ003", ArrivalTime = new DateTime(2016, 9, 9, 19, 0, 0), DepartureTime = new DateTime(2016, 9, 9, 19, 29, 0), Status = FlightStatus.Active },
                    }
                },
                new GateFlights
                {
                    Gate = new Gate { GateNumber = 24 },
                    Flights = new List<Flight>
                    {
                        new Flight{ FlightId = 11, FlightCode = "LB002", ArrivalTime = new DateTime(2016, 9, 9, 0, 0, 0), DepartureTime = new DateTime(2016, 9, 9, 0, 29, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 12, FlightCode = "XF001", ArrivalTime = new DateTime(2016, 9, 9, 0, 30, 0), DepartureTime = new DateTime(2016, 9, 9, 0, 59, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 13, FlightCode = "QF343", ArrivalTime = new DateTime(2016, 9, 9, 3, 0, 0), DepartureTime = new DateTime(2016, 9, 9, 3, 29, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 14, FlightCode = "QF345", ArrivalTime = new DateTime(2016, 9, 9, 4, 30, 0), DepartureTime = new DateTime(2016, 9, 9, 4, 59, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 15, FlightCode = "VP103", ArrivalTime = new DateTime(2016, 9, 9, 8, 0, 0), DepartureTime = new DateTime(2016, 9, 9, 8, 29, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 16, FlightCode = "FJ003", ArrivalTime = new DateTime(2016, 9, 9, 9, 0, 0), DepartureTime = new DateTime(2016, 9, 9, 9, 29, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 17, FlightCode = "PL939", ArrivalTime = new DateTime(2016, 9, 9, 9, 30, 0), DepartureTime = new DateTime(2016, 9, 9, 9, 59, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 18, FlightCode = "FP930", ArrivalTime = new DateTime(2016, 9, 9, 13, 0, 0), DepartureTime = new DateTime(2016, 9, 9, 13, 29, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 19, FlightCode = "LF004", ArrivalTime = new DateTime(2016, 9, 9, 14, 0, 0), DepartureTime = new DateTime(2016, 9, 9, 14, 29, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 19, FlightCode = "VP930", ArrivalTime = new DateTime(2016, 9, 9, 17, 30, 0), DepartureTime = new DateTime(2016, 9, 9, 17, 59, 0), Status = FlightStatus.Active },
                    }
                },
                new GateFlights
                {
                    Gate = new Gate { GateNumber = 25 },
                    Flights = new List<Flight>
                    {
                        new Flight{ FlightId = 11, FlightCode = "TF390", ArrivalTime = new DateTime(2016, 9, 9, 4, 0, 0), DepartureTime = new DateTime(2016, 9, 9, 4, 29, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 12, FlightCode = "FJ101", ArrivalTime = new DateTime(2016, 9, 9, 5, 30, 0), DepartureTime = new DateTime(2016, 9, 9, 5, 59, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 13, FlightCode = "XF003", ArrivalTime = new DateTime(2016, 9, 9, 6, 0, 0), DepartureTime = new DateTime(2016, 9, 9, 6, 29, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 14, FlightCode = "RL900", ArrivalTime = new DateTime(2016, 9, 9, 10, 30, 0), DepartureTime = new DateTime(2016, 9, 9, 10, 59, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 15, FlightCode = "NZ010", ArrivalTime = new DateTime(2016, 9, 9, 11, 0, 0), DepartureTime = new DateTime(2016, 9, 9, 11, 29, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 16, FlightCode = "FJ404", ArrivalTime = new DateTime(2016, 9, 9, 16, 0, 0), DepartureTime = new DateTime(2016, 9, 9, 16, 29, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 17, FlightCode = "AP301", ArrivalTime = new DateTime(2016, 9, 9, 17, 30, 0), DepartureTime = new DateTime(2016, 9, 9, 17, 59, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 18, FlightCode = "PL903", ArrivalTime = new DateTime(2016, 9, 9, 20, 0, 0), DepartureTime = new DateTime(2016, 9, 9, 20, 29, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 19, FlightCode = "LF009", ArrivalTime = new DateTime(2016, 9, 9, 21, 0, 0), DepartureTime = new DateTime(2016, 9, 9, 21, 29, 0), Status = FlightStatus.Active },
                        new Flight{ FlightId = 19, FlightCode = "FJ940", ArrivalTime = new DateTime(2016, 9, 9, 21, 30, 0), DepartureTime = new DateTime(2016, 9, 9, 21, 59, 0), Status = FlightStatus.Active },
                    }
                }
            };
        }

        public async Task<List<Gate>> GetGates()
        {
            return await Task.Run(() => _gateFlights.Select(gf => gf.Gate).ToList());
        }

        public async Task<List<Flight>> GetFlights(int gateNumber)
        {
            if (!_gateFlights.Where(gf => gf.Gate.GateNumber == gateNumber).Any())
                throw new ArgumentException("Invalid gate number provided");

            return await Task.Run(() => _gateFlights.SingleOrDefault(gf => gf.Gate.GateNumber == gateNumber).Flights);

        }

        public async Task CreateFlight(int gateNumber, DTO.Flight flight)
        {
            if (!_gateFlights.Any(gf => gf.Gate.GateNumber == gateNumber))
                throw new ArgumentException("Invalid gate number provided");

            await Task.Run(() => _gateFlights.FirstOrDefault(gf => gf.Gate.GateNumber == gateNumber).Flights.Add(flight));
        }

        public async Task UpdateFlight(int gateNumber, DTO.Flight flight)
        {
            if (!_gateFlights.Where(gf => gf.Gate.GateNumber == gateNumber).Any())
            {
                throw new ArgumentException("Invalid gate number provided");
            }

            if (!_gateFlights.FirstOrDefault(gf => gf.Gate.GateNumber == gateNumber).Flights.Any(f => f.FlightCode == flight.FlightCode))
            {
                throw new ArgumentException("Invalid flight number not found for gate");
            }

            await Task.Run(() =>
            {
                var currentFlight = _gateFlights.FirstOrDefault(gf => gf.Gate.GateNumber == gateNumber).Flights.FirstOrDefault(f => f.FlightCode == flight.FlightCode);
                currentFlight = flight;
            });
        }

        public async Task MoveFlight(int gateNumber, DTO.Flight flight, int targetGateNumber)
        {
            if (!_gateFlights.Where(gf => gf.Gate.GateNumber == gateNumber).Any())
            {
                throw new ArgumentException("Invalid gate number provided");
            }

            if (!_gateFlights.Where(gf => gf.Gate.GateNumber == targetGateNumber).Any())
            {
                throw new ArgumentException("Invalid target gate number provided");
            }
            
            if (!_gateFlights.FirstOrDefault(gf => gf.Gate.GateNumber == gateNumber).Flights.Any(f => f.FlightCode == flight.FlightCode))
            {
                throw new ArgumentException("Invalid flight number not found for gate");
            }

            lock(_padlock)
            {
                _gateFlights.Single(gf => gf.Gate.GateNumber == gateNumber).Flights.RemoveAll(f => f.FlightCode == flight.FlightCode);
                _gateFlights.Single(gf => gf.Gate.GateNumber == targetGateNumber).Flights.Add(flight);
            }
        }
    }
}

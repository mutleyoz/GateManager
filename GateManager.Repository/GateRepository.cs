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
                        new Flight{  FlightCode = "QF183", ArrivalTime =  DateTime.Today.AddHours(9).AddMinutes(0).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(9).AddMinutes(29).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "LF987", ArrivalTime = DateTime.Today.AddHours(9).AddMinutes(30).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(9).AddMinutes(59).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "NZ992", ArrivalTime = DateTime.Today.AddHours(10).AddMinutes(0).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(10).AddMinutes(29).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "PK129", ArrivalTime = DateTime.Today.AddHours(12).AddMinutes(0).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(12).AddMinutes(29).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "QF239", ArrivalTime = DateTime.Today.AddHours(14).AddMinutes(0).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(14).AddMinutes(29).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "PK221", ArrivalTime = DateTime.Today.AddHours(14).AddMinutes(30).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(14).AddMinutes(59).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "RN982", ArrivalTime = DateTime.Today.AddHours(16).AddMinutes(0).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(16).AddMinutes(29).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "PK556", ArrivalTime = DateTime.Today.AddHours(17).AddMinutes(0).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(17).AddMinutes(29).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "FJ001", ArrivalTime = DateTime.Today.AddHours(18).AddMinutes(0).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(18).AddMinutes(29).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "FJ003", ArrivalTime = DateTime.Today.AddHours(19).AddMinutes(0).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(19).AddMinutes(29).ToUniversalTime(), Status = FlightStatus.Scheduled },
                    }
                },
                new GateFlights
                {
                    Gate = new Gate { GateNumber = 24 },
                    Flights = new List<Flight>
                    {
                        new Flight{  FlightCode = "LB002", ArrivalTime = DateTime.Today.AddHours(0).AddMinutes(30).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(0).AddMinutes(59).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "XF001", ArrivalTime = DateTime.Today.AddHours(1).AddMinutes(30).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(1).AddMinutes(59).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "QF343", ArrivalTime = DateTime.Today.AddHours(3).AddMinutes(0).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(3).AddMinutes(29).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "QF345", ArrivalTime = DateTime.Today.AddHours(4).AddMinutes(30).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(4).AddMinutes(59).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "VP103", ArrivalTime = DateTime.Today.AddHours(8).AddMinutes(0).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(8).AddMinutes(29).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "FJ003", ArrivalTime = DateTime.Today.AddHours(9).AddMinutes(0).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(9).AddMinutes(29).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "PL939", ArrivalTime = DateTime.Today.AddHours(9).AddMinutes(30).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(9).AddMinutes(59).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "FP930", ArrivalTime = DateTime.Today.AddHours(13).AddMinutes(0).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(13).AddMinutes(29).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "LF004", ArrivalTime = DateTime.Today.AddHours(14).AddMinutes(0).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(14).AddMinutes(29).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "VP930", ArrivalTime = DateTime.Today.AddHours(17).AddMinutes(30).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(17).AddMinutes(59).ToUniversalTime(), Status = FlightStatus.Scheduled },
                    }
                },
                new GateFlights
                {
                    Gate = new Gate { GateNumber = 25 },
                    Flights = new List<Flight>
                    {
                        new Flight{  FlightCode = "TF390", ArrivalTime = DateTime.Today.AddHours(4).AddMinutes(0).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(4).AddMinutes(29).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "FJ101", ArrivalTime = DateTime.Today.AddHours(5).AddMinutes(30).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(5).AddMinutes(59).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "XF003", ArrivalTime = DateTime.Today.AddHours(6).AddMinutes(0).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(6).AddMinutes(29).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "RL900", ArrivalTime = DateTime.Today.AddHours(10).AddMinutes(30).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(10).AddMinutes(59).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "NZ010", ArrivalTime = DateTime.Today.AddHours(11).AddMinutes(0).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(11).AddMinutes(29).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "FJ404", ArrivalTime = DateTime.Today.AddHours(16).AddMinutes(0).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(16).AddMinutes(29).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "AP301", ArrivalTime = DateTime.Today.AddHours(17).AddMinutes(30).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(17).AddMinutes(59).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "PL903", ArrivalTime = DateTime.Today.AddHours(20).AddMinutes(0).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(20).AddMinutes(29).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "LF009", ArrivalTime = DateTime.Today.AddHours(21).AddMinutes(0).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(21).AddMinutes(29).ToUniversalTime(), Status = FlightStatus.Scheduled },
                        new Flight{  FlightCode = "FJ940", ArrivalTime = DateTime.Today.AddHours(21).AddMinutes(30).ToUniversalTime(), DepartureTime = DateTime.Today.AddHours(21).AddMinutes(59).ToUniversalTime(), Status = FlightStatus.Scheduled },
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

            return await Task.Run(() => _gateFlights.SingleOrDefault(gf => gf.Gate.GateNumber == gateNumber).Flights.Where(f => f.Status != FlightStatus.Cancelled).OrderBy( f => f.ArrivalTime).ToList());

        }

        public async Task CreateFlight(int gateNumber, DTO.Flight flight)
        {
            if (!_gateFlights.Any(gf => gf.Gate.GateNumber == gateNumber))
                throw new ArgumentException("Invalid gate number provided");

            if(_gateFlights.Single(gf => gf.Gate.GateNumber == gateNumber).Flights.Any(f => f.FlightCode == flight.FlightCode && f.Status != FlightStatus.Cancelled))
                throw new ArgumentException("Flight is already scheduled at this gate");

            flight.ArrivalTime = flight.ArrivalTime.ToUniversalTime();
            flight.DepartureTime = flight.DepartureTime.ToUniversalTime();

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
                if(currentFlight != null)
                {
                    currentFlight.ArrivalTime = flight.ArrivalTime.ToUniversalTime();
                    currentFlight.DepartureTime = flight.DepartureTime.ToUniversalTime();
                    currentFlight.Status = flight.Status;
                }
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

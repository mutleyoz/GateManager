using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GateManager.API.Controllers;
using Newtonsoft.Json;
using GateManager.DTO;
using GateManager.Repository;
using System.Web.Http.Results;

namespace GateManager.test
{
    [TestClass]
    public class GateTests
    {
        public GateController _gateApi;

        [TestInitialize]
        public void Init()
        {
            _gateApi = new GateController( new GateRepository());
        }

        [TestMethod]
        public void confirm_3_gates_available()
        {
            var result = Task.Run(() => _gateApi.GetAllGates()).Result;

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<List<Gate>>));

            var typedResult = result as OkNegotiatedContentResult<List<Gate>>;

            Assert.AreEqual(typedResult.Content.Count(), 3);
        }

        [TestMethod]
        public void confirm_10_flights_within_gate_23()
        {
            var result = Task.Run(() => _gateApi.GetFlights(23)).Result;

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<List<Flight>>));

            var typedResult = result as OkNegotiatedContentResult<List<Flight>>;

            Assert.AreEqual(typedResult.Content.Count(), 10); 
        }

        [TestMethod]
        public void cancel_flight_FJ003_at_gate_24()
        {
            var result = Task.Run(() => _gateApi.CancelFlight(24, "FJ003")).Result;
            Assert.IsInstanceOfType(result, typeof(OkResult));

            // Check that the flight is now cancelled.

            var flights = Task.Run(() => _gateApi.GetFlights(24)).Result as OkNegotiatedContentResult<List<Flight>>;
            Assert.IsNotNull(flights);
            Assert.AreEqual(flights.Content.FirstOrDefault(f => f.FlightCode == "FJ003").Status, FlightStatus.Cancelled);
        }

        [TestMethod]
        public void create_flight_at_gate_23_with_no_gate_conflict()
        {
            Flight flight = new Flight
            {
                FlightId = 101,
                FlightCode = "QFA012",
                ArrivalTime = new DateTime(2016, 9, 9, 10, 30,0 ),
                DepartureTime= new DateTime(2016, 9, 9, 10, 59, 0 ),
                Status = FlightStatus.Active
            };

            var result = Task.Run(() => _gateApi.CreateFlight(23, flight)).Result;
            Assert.IsInstanceOfType(result, typeof(OkResult));

            var flights = Task.Run(() => _gateApi.GetFlights(23)).Result as OkNegotiatedContentResult<List<Flight>>;
            Assert.IsNotNull(flights);

            // Assert that the flight has been added and is not in conflict.
            Assert.AreEqual(flights.Content.Count, 11);
            Assert.IsTrue(flights.Content.Any(f => f.FlightCode == "QFA012" && f.Status == FlightStatus.Active));
        }

        [TestMethod]
        public void create_flight_at_gate_24_with_gate_conflict()
        {
            Flight flight = new Flight
            {
                FlightId = 102,
                FlightCode = "TGW112",
                ArrivalTime = new DateTime(2016, 9, 9, 9, 20, 0),
                DepartureTime = new DateTime(2016, 9, 9, 9, 49, 0),
                Status = FlightStatus.Active
            };

            var result = Task.Run(() => _gateApi.CreateFlight(24, flight)).Result;
            Assert.IsInstanceOfType(result, typeof(OkResult));

            var flights = Task.Run(() => _gateApi.GetFlights(24)).Result as OkNegotiatedContentResult<List<Flight>>;
            Assert.IsNotNull(flights);

            // Assert that the flight has been added and IS in conflict.
            Assert.AreEqual(flights.Content.Count, 11);
            Assert.IsTrue(flights.Content.Any(f => f.FlightCode == "TGW112" && f.Status == FlightStatus.Conflict));
        }

        [TestMethod]
        public void move_flight_PK221_from_gate_23_to_gate_24()
        {
            var flights = Task.Run(() => _gateApi.GetFlights(23)).Result as OkNegotiatedContentResult<List<Flight>>;
            Assert.IsNotNull(flights);

            var flight = flights.Content.FirstOrDefault(f => f.FlightCode == "PK221");
            Assert.IsNotNull(flight);

            Task.Run(() => _gateApi.MoveFlight(23, flight.FlightCode, 24)).Wait();

            // Assert that flight is no longer available at gate 23, assert that it is available and not in conflict at gate 24.

            var gate23Flights= Task.Run(() => _gateApi.GetFlights(23)).Result as OkNegotiatedContentResult<List<Flight>>;
            Assert.IsNotNull(gate23Flights);
            Assert.AreEqual(9, gate23Flights.Content.Count);
            Assert.IsFalse(gate23Flights.Content.Any(f => f.FlightCode == "PK221"));

            var gate24Flights = Task.Run(() => _gateApi.GetFlights(24)).Result as OkNegotiatedContentResult<List<Flight>>;
            Assert.IsNotNull(gate24Flights);
            Assert.AreEqual(11, gate24Flights.Content.Count);
            Assert.IsTrue(gate24Flights.Content.Any(f => f.FlightCode == "PK221"));
        }

        [TestMethod]
        public void move_flight_VP930_from_gate_24_to_gate_25()
        {
            var flights = Task.Run(() => _gateApi.GetFlights(24)).Result as OkNegotiatedContentResult<List<Flight>>;
            Assert.IsNotNull(flights);

            var flight = flights.Content.FirstOrDefault(f => f.FlightCode == "VP930");
            Assert.IsNotNull(flight);

            Task.Run(() => _gateApi.MoveFlight(24, flight.FlightCode, 25)).Wait();

            // Assert that flight is no longer available at gate 24, assert that it is available and IS in conflict at gate 25.

            var gate24Flights = Task.Run(() => _gateApi.GetFlights(24)).Result as OkNegotiatedContentResult<List<Flight>>;
            Assert.IsNotNull(gate24Flights);
            Assert.AreEqual(9, gate24Flights.Content.Count);
            Assert.IsFalse(gate24Flights.Content.Any(f => f.FlightCode == "VP930"));

            var gate25Flights = Task.Run(() => _gateApi.GetFlights(25)).Result as OkNegotiatedContentResult<List<Flight>>;
            Assert.IsNotNull(gate25Flights);
            Assert.AreEqual(11, gate25Flights.Content.Count);
            Assert.IsTrue(gate25Flights.Content.Any(f => f.FlightCode == "VP930"));
            Assert.AreEqual(FlightStatus.Conflict, gate25Flights.Content.First(f => f.FlightCode == "VP930").Status);
        }


        [TestMethod]
        public void get_gates_with_no_conflicts_for_fight_LF987()
        {
            var gates = Task.Run(() => _gateApi.GetAlternateGates(23, "LF987")).Result as OkNegotiatedContentResult<List<Gate>>;

            // Assert that gate 25 is only alternate gate available for flight LF987
            Assert.AreEqual(1, gates.Content.Count());
            Assert.IsTrue(gates.Content.Any(g => g.GateNumber == 25));
        }
    }
}

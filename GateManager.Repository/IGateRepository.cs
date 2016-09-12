using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GateManager.DTO;

namespace GateManager.Repository
{
    public interface IGateRepository
    {
        Task<List<Gate>> GetGates();

        Task<List<Flight>> GetFlights(int gateNumber);

        Task CreateFlight(int gateNumber, Flight flight);

        Task UpdateFlight(int gateNumber, Flight flight);

        Task MoveFlight(int gateNumber, DTO.Flight flight, int targetGateNumber);
    }
}

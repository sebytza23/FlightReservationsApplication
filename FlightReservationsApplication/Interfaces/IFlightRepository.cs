using FlightReservationsApplication.Models;
using FlightReservationsApplication.Utils;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationsApplication.Interfaces
{
    public interface IFlightRepository : IRepository<Flight>
    {
        Task<Flight> CreateFlight(Flight flight);
        Task EditFlight(Flight flight);
        Task DeleteFlight(Flight flight);
        Task<Flight> GetFlightById(int? id);
        Task<FlightIndexModel> FlightsWithAllData(int pageSize, int pageNumber);
        Task<DbSet<Aircraft>> GetAircrafts();
        Task<DbSet<Airline>> GetAirlines();
        Task<DbSet<Airport>> GetAirports();        
    }
}

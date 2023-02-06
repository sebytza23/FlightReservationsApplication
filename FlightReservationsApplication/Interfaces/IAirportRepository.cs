using FlightReservationsApplication.Models;

namespace FlightReservationsApplication.Interfaces
{
    public interface IAirportRepository : IRepository<Airport>
    {
        Task<Airport> CreateAirport(Airport airport);
        Task<List<string>> GetAirportsLocations();
    }
}

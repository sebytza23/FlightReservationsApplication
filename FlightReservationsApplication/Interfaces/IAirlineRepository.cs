using FlightReservationsApplication.Models;

namespace FlightReservationsApplication.Interfaces
{
    public interface IAirlineRepository : IRepository<Airline>
    {
        Task<Airline> CreateAirline(Airline airline);
    }
}

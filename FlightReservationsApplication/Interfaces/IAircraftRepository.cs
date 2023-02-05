using FlightReservationsApplication.Models;

namespace FlightReservationsApplication.Interfaces
{
    public interface IAircraftRepository : IRepository<Aircraft>
    {
        Task<Aircraft> CreateAircraft(Aircraft aircraft);
    }
}

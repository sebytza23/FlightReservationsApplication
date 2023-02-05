using FlightReservationsApplication.Models;

namespace FlightReservationsApplication.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<Reservation> CreateReservation(Reservation reservation);

    }
}

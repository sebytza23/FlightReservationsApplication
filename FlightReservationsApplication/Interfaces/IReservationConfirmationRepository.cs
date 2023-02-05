using FlightReservationsApplication.Models;

namespace FlightReservationsApplication.Interfaces
{
    public interface IReservationConfirmationRepository : IRepository<ReservationConfirmation>
    {
        Task<ReservationConfirmation> CreateReservationConfirmation(ReservationConfirmation reservationConfirmation);
    }
}

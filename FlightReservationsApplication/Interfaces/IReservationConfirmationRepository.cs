using FlightReservationsApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationsApplication.Interfaces
{
    public interface IReservationConfirmationRepository : IRepository<ReservationConfirmation>
    {
        Task<ReservationConfirmation> CreateReservationConfirmation(ReservationConfirmation reservationConfirmation);
        Task EditReservationConfirmation(ReservationConfirmation reservationConfirmation);
        Task DeleteReservationConfirmation(ReservationConfirmation reservationConfirmation);
        Task<List<ReservationConfirmation>> ReservationConfirmationsWithAll();
        Task<ReservationConfirmation> GetReservationConfirmationById(int? id);
        Task<DbSet<Employee>> GetEmployees();
        Task<DbSet<Reservation>> GetReservations();
    }
}

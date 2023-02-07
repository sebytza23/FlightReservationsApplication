using FlightReservationsApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationsApplication.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<Reservation> CreateReservation(Reservation reservation);
        Task EditReservation(Reservation reservation);
        Task DeleteReservation(Reservation reservation);
        Task<List<Reservation>> ReservationWithAll();
        Task<List<Reservation>> ReservationWithData();
        Task<Reservation> GetReservationById(int? id);
        Task<Reservation> GetReservationWithDataById(int? id);
        Task<DbSet<Customer>> GetCustomers();
        Task<DbSet<Seat>> GetSeats();
        Task<DbSet<Flight>> GetFlights();
        Task<DbSet<ReservationConfirmation>> GetReservationConfirmations();
        IEnumerable<object> GetStatuses();
    }
}

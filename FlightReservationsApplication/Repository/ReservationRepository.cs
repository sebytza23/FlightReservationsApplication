using FlightReservationsApplication.Context;
using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Models;
using FlightReservationsApplication.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Principal;

namespace FlightReservationsApplication.Repository
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Reservation> CreateReservation(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task EditReservation(Reservation reservation)
        {
            await UpdateAsync(reservation);
        }

        public async Task DeleteReservation(Reservation reservation)
        {

            if (reservation == null) return;
            reservation.ReservationConfirmation = await _context.ReservationConfirmations.FirstOrDefaultAsync(a => a.ReservationConfirmationID == reservation.ReservationConfirmationID);
            if (reservation.ReservationConfirmation != null)
            {
                _context.ReservationConfirmations.Remove(reservation.ReservationConfirmation);
                await _context.SaveChangesAsync();
            }
            await DeleteAsync(reservation);

        }

        public async Task<List<Reservation>> ReservationWithAll()
        {
            string[] includes = { "Customer","Seat","ReservationConfirmation" };
            List<Reservation> reservations = await (await Include(includes)).ToListAsync();
            return reservations;
        }

        public async Task<Reservation> GetReservationById(int? id)
        {
            string[] includes = { "Customer", "Seat", "ReservationConfirmation" };
            return await (await Include(includes)).FirstOrDefaultAsync(c => c.ReservationID == id);
        }

        public async Task<DbSet<Customer>> GetCustomers() =>  _context.Customers;
        public async Task<DbSet<Seat>> GetSeats() => _context.Seats;
        public async Task<DbSet<ReservationConfirmation>> GetReservationConfirmations() => _context.ReservationConfirmations;
        public IEnumerable<object> GetStatuses() =>
            from Status s in Enum.GetValues(typeof(Status))
            select new { ID = (int)s, Status = s.ToString() };
    }
}

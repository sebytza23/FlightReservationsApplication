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
            Seat seat = await _context.Seats.FindAsync(reservation.SeatID);
            if (seat != null)
            {
                seat.IsAvailable = false;
                _context.Seats.Update(seat);
                await _context.SaveChangesAsync();
            }
            return reservation;
        }

        public async Task EditReservation(Reservation reservation)
        {
            await UpdateAsync(reservation);
        }

        public async Task DeleteReservation(Reservation reservation)
        {

            if (reservation == null) return;
            ReservationConfirmation reservationConfirmation = await _context.ReservationConfirmations.FirstOrDefaultAsync(a => a.ReservationConfirmationID == reservation.ReservationConfirmationID);
            if (reservationConfirmation != null)
            {
                reservation.ReservationConfirmationID = null;
                _context.Reservations.Update(reservation);
                await _context.SaveChangesAsync();
                Seat seat = await _context.Seats.FindAsync(reservation.SeatID);
                if (seat != null)
                {
                    seat.IsAvailable = true;
                    _context.Seats.Update(seat);
                    await _context.SaveChangesAsync();
                }
                _context.Reservations.Update(reservation);
                _context.ReservationConfirmations.Remove(reservationConfirmation);
                await _context.SaveChangesAsync();
            }
            Seat _seat = await _context.Seats.FirstOrDefaultAsync(s => s.SeatID == reservation.SeatID);
            if (_seat != null)
            {
                _seat.IsAvailable = true;
                _context.Seats.Update(_seat);
                await _context.SaveChangesAsync();
            }
            await DeleteAsync(reservation);

        }
        public async Task<List<Reservation>> ReservationWithData()
        {
            string[] includes = { "Customer", "Seat" };
            List<Reservation> reservations = await (await Include(includes)).ToListAsync();
            return reservations;
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

        public async Task<Reservation> GetReservationWithDataById(int? id)
        {
            string[] includes = { "Customer", "Seat" };
            return await (await Include(includes)).FirstOrDefaultAsync(c => c.ReservationID == id);
        }

        public async Task<DbSet<Customer>> GetCustomers() =>  _context.Customers;
        public async Task<DbSet<Seat>> GetSeats() => _context.Seats;
        public async Task<DbSet<Flight>> GetFlights() => _context.Flights;
        public async Task<DbSet<ReservationConfirmation>> GetReservationConfirmations() => _context.ReservationConfirmations;
        public IEnumerable<object> GetStatuses() =>
            from Status s in Enum.GetValues(typeof(Status))
            select new { ID = (int)s, Status = s.ToString() };
    }
}

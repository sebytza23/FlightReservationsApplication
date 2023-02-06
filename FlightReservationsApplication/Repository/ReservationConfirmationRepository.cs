using FlightReservationsApplication.Context;
using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Models;
using FlightReservationsApplication.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Principal;

namespace FlightReservationsApplication.Repository
{
    public class ReservationConfirmationRepository : Repository<ReservationConfirmation>, IReservationConfirmationRepository
    {
        public ReservationConfirmationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ReservationConfirmation> CreateReservationConfirmation(ReservationConfirmation reservationConfirmation)
        {
            await _context.ReservationConfirmations.AddAsync(reservationConfirmation);
            await _context.SaveChangesAsync();
            reservationConfirmation = await GetReservationConfirmationById(reservationConfirmation.ReservationConfirmationID);
            reservationConfirmation.Reservation.ReservationConfirmationID = reservationConfirmation.ReservationConfirmationID;
            if (reservationConfirmation.ConfirmationDate != null)
                reservationConfirmation.Reservation.Status = Status.Accepted;
            if (reservationConfirmation.DeclinedDate != null)
                reservationConfirmation.Reservation.Status = Status.Declined;
            _context.ReservationConfirmations.Update(reservationConfirmation);
            await _context.SaveChangesAsync();
            return reservationConfirmation;
        }

        public async Task EditReservationConfirmation(ReservationConfirmation reservationConfirmation)
        {
            ReservationConfirmation oldReservationConfirmation = await GetReservationConfirmationById(reservationConfirmation.ReservationConfirmationID);
            if(reservationConfirmation.DeclinedDate != oldReservationConfirmation.DeclinedDate && oldReservationConfirmation.DeclinedDate == null)
            {
                oldReservationConfirmation.Reservation.Status = Status.Declined;
                _context.ReservationConfirmations.Update(oldReservationConfirmation);
                await _context.SaveChangesAsync();
            }else if(reservationConfirmation.ConfirmationDate != oldReservationConfirmation.ConfirmationDate && oldReservationConfirmation.ConfirmationDate == null)
            {
                oldReservationConfirmation.Reservation.Status = Status.Accepted;
                _context.ReservationConfirmations.Update(oldReservationConfirmation);
                await _context.SaveChangesAsync();
            }else if (reservationConfirmation.ConfirmationDate == null && oldReservationConfirmation.DeclinedDate == null)
            {
                oldReservationConfirmation.Reservation.Status = Status.InProgress;
                _context.ReservationConfirmations.Update(oldReservationConfirmation);
                await _context.SaveChangesAsync();
            }
            State(oldReservationConfirmation, EntityState.Detached);
            await UpdateAsync(reservationConfirmation);
        }

        public async Task DeleteReservationConfirmation(ReservationConfirmation reservationConfirmation)
        {

            if (reservationConfirmation == null) return;
            if (reservationConfirmation.Reservation != null)
            {
                reservationConfirmation.Reservation.ReservationConfirmationID = null;
                _context.ReservationConfirmations.Update(reservationConfirmation);
                await _context.SaveChangesAsync();
            }
            await DeleteAsync(reservationConfirmation);

        }

        public async Task<List<ReservationConfirmation>> ReservationConfirmationsWithAll()
        {
            string[] includes = { "Employee", "Reservation" };
            List<ReservationConfirmation> reservationConfirmation = await (await Include(includes)).ToListAsync();
            return reservationConfirmation;
        }

        public async Task<ReservationConfirmation> GetReservationConfirmationById(int? id)
        {
            string[] includes = { "Employee", "Reservation" };
            return await (await Include(includes)).FirstOrDefaultAsync(c => c.ReservationConfirmationID == id);
        }

        public async Task<DbSet<Employee>> GetEmployees() => _context.Employees;

        public async Task<DbSet<Reservation>> GetReservations() => _context.Reservations;


    }
}

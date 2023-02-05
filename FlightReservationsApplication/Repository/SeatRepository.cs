using FlightReservationsApplication.Context;
using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Models;
using FlightReservationsApplication.Repository;
using FlightReservationsApplication.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Principal;

namespace FlightReservationsApplication.Repository
{
    public class SeatRepository : Repository<Seat>, ISeatRepository
    {
        public SeatRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Seat> CreateSeat(Seat seat)
        {
            await _context.Seats.AddAsync(seat);
            await _context.SaveChangesAsync();
            return seat;
        }

        public async Task EditSeat(Seat seat)
        {
            await UpdateAsync(seat);
        }

        public async Task DeleteSeat(Seat seat)
        {
            if (seat == null) return;
            await DeleteAsync(seat);
        }

        public async Task<Seat> GetSeatById(int? id)
        {
            string[] includes = { "Class", "Flight" };
            return await (await Include(includes)).FirstOrDefaultAsync(c => c.SeatID == id);
        }

        public async Task<SeatIndexModel> SeatsWithAllData(int pageNumber, int pageSize)
        {
            string[] includes = { "Class", "Flight" };
            List<Seat> seats;
            seats = await (await Include(includes)).Skip((pageNumber - 1) * pageSize)
            .Take(pageSize).ToListAsync();
            var pagingInfo = new PaginationInfo
            {
                TotalItems = (await Include(includes)).Count(),
                ItemsPerPage = pageSize,
                CurrentPage = pageNumber
            };

            var pageNumbers = PaginationHelper.GetPageNumbers(pageNumber, pagingInfo.TotalPages);
            SeatIndexModel viewModel = new SeatIndexModel
            {
                Seats = seats,
                PagingInfo = pagingInfo,
                PageNumbers = pageNumbers
            };
            return viewModel;
        }
        public async Task<DbSet<Flight>> GetFlights()
        {
            return _context.Flights;
        }

        public async Task<DbSet<Class>> GetClasses()
        {
            return _context.Classes;
        }

    }
}

using FlightReservationsApplication.Models;
using FlightReservationsApplication.Utils;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationsApplication.Interfaces
{

        public interface ISeatRepository : IRepository<Seat>
        {
            Task<Seat> CreateSeat(Seat seat);
            Task EditSeat(Seat seat);
            Task DeleteSeat(Seat seat);
            Task<Seat> GetSeatById(int? id);
            Task<SeatIndexModel> SeatsWithAllData(int pageNumber, int pageSize);
            Task<DbSet<Flight>> GetFlights();
            Task<DbSet<Class>> GetClasses();
        }
    
}

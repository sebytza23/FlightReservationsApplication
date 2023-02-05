using FlightReservationsApplication.Context;
using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Models;
using FlightReservationsApplication.Repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace FlightReservationsApplication.Repository
{
    public class AircraftRepository : Repository<Aircraft>, IAircraftRepository
    {
        public AircraftRepository(ApplicationDbContext context) : base(context)
        {
        }
        
        public async Task<Aircraft> CreateAircraft(Aircraft aircraft)
        {
            await _context.Aircrafts.AddAsync(aircraft);
            await _context.SaveChangesAsync();
            return aircraft;
        }
    }
}

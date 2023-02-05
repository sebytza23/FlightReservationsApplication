using FlightReservationsApplication.Context;
using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Models;
using FlightReservationsApplication.Repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace FlightReservationsApplication.Repository
{
    public class AirlineRepository : Repository<Airline>, IAirlineRepository
    {
        public AirlineRepository(ApplicationDbContext context) : base(context)
        {
        }
        
        public async Task<Airline> CreateAirline(Airline airline)
        {
            await _context.Airlines.AddAsync(airline);
            await _context.SaveChangesAsync();
            return airline;
        }

    }
}

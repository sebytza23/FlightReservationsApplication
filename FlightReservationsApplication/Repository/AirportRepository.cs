using FlightReservationsApplication.Context;
using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Models;
using FlightReservationsApplication.Repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace FlightReservationsApplication.Repository
{
    public class AirportRepository : Repository<Airport>, IAirportRepository
    {
        public AirportRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Airport> CreateAirport(Airport airport)
        {
            await _context.Airports.AddAsync(airport);
            await _context.SaveChangesAsync();
            return airport;
        }

        public async Task<List<string>> GetAirportsLocations()
        {
            return await _context.Airports.Select(a => a.Location).Distinct().ToListAsync();
        }
    }
}

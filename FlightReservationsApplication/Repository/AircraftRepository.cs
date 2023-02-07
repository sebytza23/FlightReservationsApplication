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

        public async Task DeleteAircraft(Aircraft aircraft)
        {
            Aircraft currAircraft = await GetAircraftsById(aircraft.AircraftID);
            if (currAircraft.Flights == null)
            {
                _context.Aircrafts.Remove(currAircraft);
                await _context.SaveChangesAsync();
                return;
            }
            foreach (Flight flight in currAircraft.Flights)
            {
                if(flight.Seats == null)
                {
                    _context.Flights.Remove(flight);
                    await _context.SaveChangesAsync();
                    continue;
                }
                foreach (Seat seat in flight.Seats)
                {
                    _context.Seats.Remove(seat);
                }
                _context.Flights.Remove(flight);
                await _context.SaveChangesAsync();
            }
            _context.Aircrafts.Remove(currAircraft);
            await _context.SaveChangesAsync();
        }
        public async Task<Aircraft> GetAircraftsById(int? id)
        {
            string[] includes = { "Flights", "Flights.Seats" };
            return await (await Include(includes)).FirstOrDefaultAsync(c => c.AircraftID == id);
        }
    }
}

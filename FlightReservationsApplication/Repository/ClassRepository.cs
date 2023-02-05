using FlightReservationsApplication.Context;
using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Models;
using FlightReservationsApplication.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Principal;

namespace FlightReservationsApplication.Repository
{
    public class ClassRepository : Repository<Class>, IClassRepository
    {
        public ClassRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Class> CreateClass(Class _class)
        {
            await _context.Classes.AddAsync(_class);
            await _context.SaveChangesAsync();
            return _class;
        }

        public async Task EditClass(Class _class)
        {
            await UpdateAsync(_class);
        }

        public async Task DeleteClass(Class _class)
        {
            if (_class == null) return;
            await DeleteAsync(_class);
        }

        public async Task<DbSet<Airline>> GetAirlines()
        {
            return _context.Airlines;
        }
        public async Task<DbSet<Airport>> GetAirports()
        {
            return _context.Airports;
        }

        public async Task<Airline> FindAirline(Class _class) => await (await GetAirlines()).FirstOrDefaultAsync(a => a.AirlineID == _class.AirlineID);
        public async Task<Airport> FindAirport(Class _class) => await (await GetAirports()).FirstOrDefaultAsync(a => a.AirportID == _class.AirportID);

        public async Task<List<Class>> ClassWithAllData()
        {
            string[] includes = { "Airline", "Airport" };
            List<Class> _class = await (await Include(includes)).ToListAsync();
            return _class;
        }

        public async Task<Class> GetClassById(int? id)
        {
            string[] includes = { "Airline", "Airport" };
            return await (await Include(includes)).FirstOrDefaultAsync(c => c.ClassID == id);
        }

    }
}

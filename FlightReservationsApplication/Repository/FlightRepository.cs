using FlightReservationsApplication.Context;
using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Models;
using FlightReservationsApplication.Repository;
using FlightReservationsApplication.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Principal;
using Xunit.Abstractions;

namespace FlightReservationsApplication.Repository
{
    public class FlightRepository : Repository<Flight>, IFlightRepository
    {
        public FlightRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Flight> CreateFlight(Flight flight)
        {
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
            return flight;
        }

        public async Task EditFlight(Flight flight)
        {
            await UpdateAsync(flight);
        }

        public async Task DeleteFlight(Flight flight)
        {

            if (flight == null) return;
            await DeleteAsync(flight);

        }

        public async Task<FlightIndexModel> FlightsWithAllData(int pageNumber, int pageSize)
        {
            string[] includes = { "Aircraft", "Airline", "DepartureAirport", "ArrivalAirport" };
            List<Flight> flights;
            flights = await (await Include(includes)).Skip((pageNumber - 1) * pageSize)
            .Take(pageSize).ToListAsync();
            var pagingInfo = new PaginationInfo
            {
                TotalItems = (await Include(includes)).Count(),
                ItemsPerPage = pageSize,
                CurrentPage = pageNumber
            };

            var pageNumbers = PaginationHelper.GetPageNumbers(pageNumber, pagingInfo.TotalPages);
            FlightIndexModel viewModel = new FlightIndexModel {
                Flights = flights,
                PagingInfo = pagingInfo,
                PageNumbers = pageNumbers
            };
            return viewModel;
        }
        

        public async Task<Flight> GetFlightById(int? id)
        {
            string[] includes = { "Aircraft", "Airline", "DepartureAirport", "ArrivalAirport" };
            return await (await Include(includes)).FirstOrDefaultAsync(c => c.FlightID == id);
        }

        public async Task<DbSet<Aircraft>> GetAircrafts()
        {
            return _context.Aircrafts;
        }
        
        public async Task<DbSet<Airline>> GetAirlines()
        {
            return _context.Airlines;
        }

        public async Task<DbSet<Airport>> GetAirports()
        {
            return _context.Airports;
        }

    }
}

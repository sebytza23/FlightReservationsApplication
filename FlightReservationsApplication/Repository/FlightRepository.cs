using FlightReservationsApplication.Context;
using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Models;
using FlightReservationsApplication.Repository;
using FlightReservationsApplication.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Drawing.Printing;
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

        public async Task<List<Flight>> Paginate(IQueryable<Flight> flight, int pageNumber, int pageSize)
        {
            return await flight.Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToListAsync();
        }

        public async Task<FlightIndexModel> GetFlightsByLocationAsync(string? DepartureLocation, string? ArrivalLocation, DateTime? DepartureTime, int pageNumber, int pageSize)
        {
            string[] includes = { "Aircraft", "Airline", "DepartureAirport", "ArrivalAirport", "Seats" };
            IQueryable<Flight> flights;
            if (DepartureLocation != null && ArrivalLocation != null && DepartureTime != null)
                flights = (await Include(includes))
                    .Where(c => c.DepartureAirport.Location == DepartureLocation && c.ArrivalAirport.Location == ArrivalLocation && c.DepartureTime >= DepartureTime && (c.Seats.Count > 0 && c.Seats.Where(s => s.IsAvailable == true).Count() > 0));
            else if (DepartureLocation != null && ArrivalLocation != null)
                flights = (await Include(includes))
                    .Where(c => c.DepartureAirport.Location == DepartureLocation && c.ArrivalAirport.Location == ArrivalLocation && (c.Seats.Count > 0 && c.Seats.Where(s => s.IsAvailable == true).Count() > 0));
            else if (DepartureLocation != null && DepartureTime != null)
                flights = (await Include(includes))
                    .Where(c => c.DepartureAirport.Location == DepartureLocation && c.DepartureTime >= DepartureTime && (c.Seats.Count > 0 && c.Seats.Where(s => s.IsAvailable == true).Count() > 0));
            else if (ArrivalLocation != null && DepartureTime != null)
                flights = (await Include(includes))
                    .Where(c => c.ArrivalAirport.Location == ArrivalLocation && c.DepartureTime >= DepartureTime && (c.Seats.Count > 0 && c.Seats.Where(s => s.IsAvailable == true).Count() > 0));
            else if (DepartureLocation != null)
                flights = (await Include(includes))
                    .Where(c => c.DepartureAirport.Location == DepartureLocation && (c.Seats.Count > 0 && c.Seats.Where(s => s.IsAvailable == true).Count() > 0));
            else if (ArrivalLocation != null)
                flights = (await Include(includes))
                    .Where(c => c.ArrivalAirport.Location == ArrivalLocation && (c.Seats.Count > 0 && c.Seats.Where(s => s.IsAvailable == true).Count() > 0));
            else if (DepartureTime != null)
                flights = (await Include(includes))
                    .Where(c => c.DepartureTime >= DepartureTime && (c.Seats.Count > 0 && c.Seats.Where(s => s.IsAvailable == true).Count() > 0));
            else
                flights = (await Include(includes))
                    .Where(c => c.DepartureTime >= DateTime.Now && (c.Seats.Count > 0 && c.Seats.Where(s => s.IsAvailable == true).Count() > 0));
            var pagingInfo = new PaginationInfo
            {
                TotalItems = flights.Count(),
                ItemsPerPage = pageSize,
                CurrentPage = pageNumber
            };
            if (pagingInfo.TotalPages < pageNumber)
            {
                pageNumber = pagingInfo.TotalPages;
                pagingInfo.CurrentPage = pageNumber;
            }

            var pageNumbers = PaginationHelper.GetPageNumbers(pageNumber, pagingInfo.TotalPages);
            FlightIndexModel viewModel = new FlightIndexModel
            {
                Flights = await Paginate(flights, pageNumber, pageSize),
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
        public async Task<List<Seat>> GetSeats(){
            return await _context.Seats.ToListAsync();
        }
        public async Task<List<Class>> GetClasses()
        {
            return await _context.Classes.ToListAsync();
        }
    }
}

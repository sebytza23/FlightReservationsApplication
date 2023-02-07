using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightReservationsApplication.Context;
using FlightReservationsApplication.Models;
using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Repository;
using System.Configuration;

namespace FlightReservationsApplication.Controllers
{
    public class FlightsController : Controller
    {
        private readonly IFlightRepository _flightRepository;
        
        public FlightsController(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        // GET: Flights
        public async Task<IActionResult> Index(int? pageNumber, int? pageSize)
        {
            return View(await _flightRepository.FlightsWithAllData(pageNumber ?? 1, pageSize ?? 50));
        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (await _flightRepository.Exist(id))
            {
                return NotFound();
            }

            var flight = await _flightRepository.GetFlightById(id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        public async Task<IActionResult> CreateAsync()
        {
            await GenerateViewData(false, null);
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightID,AircraftID,AirlineID,DepartureAirportID,ArrivalAirportID,DepartureTime,ArrivalTime,FirstClassCapacity,EconomyClassCapacity")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                flight = await _flightRepository.CreateFlight(flight);
                return RedirectToAction(nameof(Index));
            }
            await GenerateViewData(true, flight);
            return View(flight);
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (await _flightRepository.Exist(id))
            {
                return NotFound();
            }

            var flight = await _flightRepository.Details(id);
            if (flight == null)
            {
                return NotFound();
            }
            await GenerateViewData(true, flight);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlightID,AircraftID,AirlineID,DepartureAirportID,ArrivalAirportID,DepartureTime,ArrivalTime,FirstClassCapacity,EconomyClassCapacity")] Flight flight)
        {
            if (id != flight.FlightID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _flightRepository.EditFlight(flight);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _flightRepository.ExistEntity(flight.FlightID)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            await GenerateViewData(true, flight);
            return View(flight);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (await _flightRepository.Exist(id))
            {
                return NotFound();
            }

            var flight = await _flightRepository.GetFlightById(id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _flightRepository.GetByIdAsync(id) == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Flights'  is null.");
            }
            var flight = await _flightRepository.Details(id);
            await _flightRepository.DeleteFlight(flight);
            return RedirectToAction(nameof(Index));
        }
        public async Task GenerateViewData(bool hasSelected, Flight? flight)
        {
            ViewData["AircraftID"] = new SelectList((await _flightRepository.GetAircrafts()), "AircraftID", "Model", (hasSelected) ? flight.AircraftID : "");
            ViewData["AirlineID"] = new SelectList((await _flightRepository.GetAirlines()), "AirlineID", "Name", (hasSelected) ? flight.AirlineID : "");
            ViewData["ArrivalAirportID"] = new SelectList((await _flightRepository.GetAirports()), "AirportID", "Name", (hasSelected) ? flight.ArrivalAirportID : "");
            ViewData["DepartureAirportID"] = new SelectList((await _flightRepository.GetAirports()), "AirportID", "Name", (hasSelected) ? flight.DepartureAirportID : "");
        }
    }
}

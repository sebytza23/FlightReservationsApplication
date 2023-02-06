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
using System.Drawing.Printing;

namespace FlightReservationsApplication.Controllers
{
    public class SeatsController : Controller
    {
        private readonly ISeatRepository _seatRepository;

        public SeatsController(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        // GET: Seats
        public async Task<IActionResult> Index(int? pageNumber, int? pageSize)
        {
            return View(await _seatRepository.SeatsWithAllData(pageNumber ?? 1, pageSize ?? 50));
        }

        // GET: Seats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (await _seatRepository.Exist(id))
            {
                return NotFound();
            }

            var flight = await _seatRepository.GetSeatById(id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Seats/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["FlightID"] = new SelectList((await _seatRepository.GetFlights()), "FlightID", "FlightID");
            ViewData["ClassID"] = new SelectList((await _seatRepository.GetClasses()), "ClassID", "ClassID");
            return View();
        }

        // POST: Seats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SeatID,SeatNumber,ClassID,FlightID,IsAvailable")] Seat seat)
        {
            if (ModelState.IsValid)
            {
                seat = await _seatRepository.CreateSeat(seat);
                return RedirectToAction(nameof(Index));

            }
            ViewData["FlightID"] = new SelectList((await _seatRepository.GetFlights()), "FlightID", "FlightID", seat.FlightID);
            ViewData["ClassID"] = new SelectList((await _seatRepository.GetClasses()), "ClassID", "ClassID", seat.ClassID);
            return View(seat);
        }

        // GET: Seats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (await _seatRepository.Exist(id))
            {
                return NotFound();
            }

            var seat = await _seatRepository.Details(id);
            if (seat == null)
            {
                return NotFound();
            }
            ViewData["FlightID"] = new SelectList((await _seatRepository.GetFlights()), "FlightID", "FlightID", seat.FlightID);
            ViewData["ClassID"] = new SelectList((await _seatRepository.GetClasses()), "ClassID", "ClassID", seat.ClassID);
            return View(seat);
        }

        // POST: Seats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SeatID,SeatNumber,ClassID,FlightID,IsAvailable")] Seat seat)
        {
            if (id != seat.SeatID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _seatRepository.EditSeat(seat);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _seatRepository.ExistEntity(seat.SeatID)))
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
            ViewData["FlightID"] = new SelectList((await _seatRepository.GetFlights()), "FlightID", "FlightID", seat.FlightID);
            ViewData["ClassID"] = new SelectList((await _seatRepository.GetClasses()), "ClassID", "Price Number", seat.ClassID);
            return View(seat);
        }

        // GET: Seats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (await _seatRepository.Exist(id))
            {
                return NotFound();
            }

            var seat = await _seatRepository.GetSeatById(id);
            if (seat == null)
            {
                return NotFound();
            }

            
            return View(seat);
        }

        // POST: Seats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _seatRepository.GetByIdAsync(id) == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Seats'  is null.");
            }
            var flight = await _seatRepository.Details(id);
            await _seatRepository.DeleteSeat(flight);
            return RedirectToAction(nameof(Index));
        }
    }
}

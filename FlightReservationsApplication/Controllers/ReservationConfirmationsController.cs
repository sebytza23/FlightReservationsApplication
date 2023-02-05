using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightReservationsApplication.Context;
using FlightReservationsApplication.Models;

namespace FlightReservationsApplication.Controllers
{
    public class ReservationConfirmationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationConfirmationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReservationConfirmations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReservationConfirmations.Include(r => r.Employee).Include(r => r.Reservation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ReservationConfirmations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReservationConfirmations == null)
            {
                return NotFound();
            }

            var reservationConfirmation = await _context.ReservationConfirmations
                .Include(r => r.Employee)
                .Include(r => r.Reservation)
                .FirstOrDefaultAsync(m => m.ReservationConfirmationID == id);
            if (reservationConfirmation == null)
            {
                return NotFound();
            }

            return View(reservationConfirmation);
        }

        // GET: ReservationConfirmations/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeID");
            ViewData["ReservationID"] = new SelectList(_context.Reservations, "ReservationID", "ReservationID");
            return View();
        }

        // POST: ReservationConfirmations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationConfirmationID,ReservationID,EmployeeID,ConfirmationDate,DeclinedDate,Notes")] ReservationConfirmation reservationConfirmation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservationConfirmation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeID", reservationConfirmation.EmployeeID);
            ViewData["ReservationID"] = new SelectList(_context.Reservations, "ReservationID", "ReservationID", reservationConfirmation.ReservationID);
            return View(reservationConfirmation);
        }

        // GET: ReservationConfirmations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReservationConfirmations == null)
            {
                return NotFound();
            }

            var reservationConfirmation = await _context.ReservationConfirmations.FindAsync(id);
            if (reservationConfirmation == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeID", reservationConfirmation.EmployeeID);
            ViewData["ReservationID"] = new SelectList(_context.Reservations, "ReservationID", "ReservationID", reservationConfirmation.ReservationID);
            return View(reservationConfirmation);
        }

        // POST: ReservationConfirmations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationConfirmationID,ReservationID,EmployeeID,ConfirmationDate,DeclinedDate,Notes")] ReservationConfirmation reservationConfirmation)
        {
            if (id != reservationConfirmation.ReservationConfirmationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservationConfirmation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationConfirmationExists(reservationConfirmation.ReservationConfirmationID))
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
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeID", reservationConfirmation.EmployeeID);
            ViewData["ReservationID"] = new SelectList(_context.Reservations, "ReservationID", "ReservationID", reservationConfirmation.ReservationID);
            return View(reservationConfirmation);
        }

        // GET: ReservationConfirmations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReservationConfirmations == null)
            {
                return NotFound();
            }

            var reservationConfirmation = await _context.ReservationConfirmations
                .Include(r => r.Employee)
                .Include(r => r.Reservation)
                .FirstOrDefaultAsync(m => m.ReservationConfirmationID == id);
            if (reservationConfirmation == null)
            {
                return NotFound();
            }

            return View(reservationConfirmation);
        }

        // POST: ReservationConfirmations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReservationConfirmations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ReservationConfirmations'  is null.");
            }
            var reservationConfirmation = await _context.ReservationConfirmations.FindAsync(id);
            if (reservationConfirmation != null)
            {
                _context.ReservationConfirmations.Remove(reservationConfirmation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationConfirmationExists(int id)
        {
          return _context.ReservationConfirmations.Any(e => e.ReservationConfirmationID == id);
        }
    }
}

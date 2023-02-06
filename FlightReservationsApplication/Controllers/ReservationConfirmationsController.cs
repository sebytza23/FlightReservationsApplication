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

namespace FlightReservationsApplication.Controllers
{
    public class ReservationConfirmationsController : Controller
    {
        private readonly IReservationConfirmationRepository _reservationConfirmationRepository;

        public ReservationConfirmationsController(IReservationConfirmationRepository reservationConfirmationRepository)
        {
            _reservationConfirmationRepository = reservationConfirmationRepository;
        }

        // GET: ReservationConfirmations
        public async Task<IActionResult> Index()
        {
            return View(await _reservationConfirmationRepository.ReservationConfirmationsWithAll());
        }

        // GET: ReservationConfirmations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (await _reservationConfirmationRepository.Exist(id))
            {
                return NotFound();
            }

            var reservationConfirmation = await _reservationConfirmationRepository.GetReservationConfirmationById(id);

            if (reservationConfirmation == null)
            {
                return NotFound();
            }

            return View(reservationConfirmation);
        }

        // GET: ReservationConfirmations/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["EmployeeID"] = new SelectList((await _reservationConfirmationRepository.GetEmployees()), "EmployeeID", "EmployeeID");
            ViewData["ReservationID"] = new SelectList((await _reservationConfirmationRepository.GetReservations()), "ReservationID", "ReservationID");
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
                if (reservationConfirmation.ConfirmationDate != null && reservationConfirmation.DeclinedDate != null)
                {
                    ViewData["EmployeeID"] = new SelectList((await _reservationConfirmationRepository.GetEmployees()), "EmployeeID", "EmployeeID", reservationConfirmation.EmployeeID);
                    ViewData["ReservationID"] = new SelectList((await _reservationConfirmationRepository.GetReservations()), "ReservationID", "ReservationID", reservationConfirmation.ReservationID);
                    return View(reservationConfirmation);
                }
                await _reservationConfirmationRepository.CreateReservationConfirmation(reservationConfirmation);
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList((await _reservationConfirmationRepository.GetEmployees()), "EmployeeID", "EmployeeID", reservationConfirmation.EmployeeID);
            ViewData["ReservationID"] = new SelectList((await _reservationConfirmationRepository.GetReservations()), "ReservationID", "ReservationID", reservationConfirmation.ReservationID);
            return View(reservationConfirmation);
        }

        // GET: ReservationConfirmations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (await _reservationConfirmationRepository.Exist(id))
            {
                return NotFound();
            }

            var reservationConfirmation = await _reservationConfirmationRepository.Details(id);
            if (reservationConfirmation == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList((await _reservationConfirmationRepository.GetEmployees()), "EmployeeID", "EmployeeID", reservationConfirmation.EmployeeID);
            ViewData["ReservationID"] = new SelectList((await _reservationConfirmationRepository.GetReservations()), "ReservationID", "ReservationID", reservationConfirmation.ReservationID);
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
                    if (reservationConfirmation.ConfirmationDate != null && reservationConfirmation.DeclinedDate != null)
                    {
                        ViewData["EmployeeID"] = new SelectList((await _reservationConfirmationRepository.GetEmployees()), "EmployeeID", "EmployeeID", reservationConfirmation.EmployeeID);
                        ViewData["ReservationID"] = new SelectList((await _reservationConfirmationRepository.GetReservations()), "ReservationID", "ReservationID", reservationConfirmation.ReservationID);
                        return View(reservationConfirmation);
                    }
                    await _reservationConfirmationRepository.EditReservationConfirmation(reservationConfirmation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _reservationConfirmationRepository.ExistEntity(reservationConfirmation.ReservationConfirmationID)))
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
            ViewData["EmployeeID"] = new SelectList((await _reservationConfirmationRepository.GetEmployees()), "EmployeeID", "EmployeeID", reservationConfirmation.EmployeeID);
            ViewData["ReservationID"] = new SelectList((await _reservationConfirmationRepository.GetReservations()), "ReservationID", "ReservationID", reservationConfirmation.ReservationID);
            return View(reservationConfirmation);
        }

        // GET: ReservationConfirmations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (await _reservationConfirmationRepository.Exist(id))
            {
                return NotFound();
            }

            var reservationConfirmation = await _reservationConfirmationRepository.GetReservationConfirmationById(id);

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
            if (await _reservationConfirmationRepository.GetByIdAsync(id) == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ReservationConfirmations'  is null.");
            }
            var reservationConfirmation = await _reservationConfirmationRepository.GetReservationConfirmationById(id);
            await _reservationConfirmationRepository.DeleteReservationConfirmation(reservationConfirmation);
            return RedirectToAction(nameof(Index));
        }
    }
}

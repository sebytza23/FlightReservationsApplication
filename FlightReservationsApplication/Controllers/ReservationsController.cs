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
    public class ReservationsController : Controller
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationsController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            return View(await _reservationRepository.ReservationWithData());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (await _reservationRepository.Exist(id))
            {
                return NotFound();
            }

            var reservation = await _reservationRepository.GetReservationById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["CustomerID"] = new SelectList((await _reservationRepository.GetCustomers()), "CustomerID", "CustomerID");
            ViewData["SeatID"] = new SelectList((await _reservationRepository.GetSeats()), "SeatID", "SeatID");
            ViewData["ReservationConfirmationID"] = new SelectList((await _reservationRepository.GetReservationConfirmations()), "ReservationConfirmationID", "ReservationConfirmationID");
            ViewData["StatusID"] = new SelectList(_reservationRepository.GetStatuses(), "ID", "Status");
            ViewData["FlightID"] = new SelectList((await _reservationRepository.GetFlights()), "FlightID", "FlightID");

            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationID,CustomerID,ReservationConfirmationID,SeatID,Status, FlightID")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                await _reservationRepository.CreateReservation(reservation);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList((await _reservationRepository.GetCustomers()), "CustomerID", "CustomerID", reservation.CustomerID);
            ViewData["SeatID"] = new SelectList((await _reservationRepository.GetSeats()), "SeatID", "SeatID", reservation.SeatID);
            ViewData["ReservationConfirmationID"] = new SelectList((await _reservationRepository.GetReservationConfirmations()), "ReservationConfirmationID", "ReservationConfirmationID", reservation.ReservationConfirmationID);
            ViewData["StatusID"] = new SelectList(_reservationRepository.GetStatuses(), "ID", "Status", reservation.Status);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (await _reservationRepository.Exist(id))
            {
                return NotFound();
            }

            var reservation = await _reservationRepository.Details(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList((await _reservationRepository.GetCustomers()), "CustomerID", "CustomerID", reservation.CustomerID);
            ViewData["SeatID"] = new SelectList((await _reservationRepository.GetSeats()), "SeatID", "SeatID", reservation.SeatID);
            ViewData["ReservationConfirmationID"] = new SelectList((await _reservationRepository.GetReservationConfirmations()), "ReservationConfirmationID", "ReservationConfirmationID", reservation.ReservationConfirmationID);
            ViewData["StatusID"] = new SelectList(_reservationRepository.GetStatuses(), "ID", "Status", reservation.Status);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationID,CustomerID,ReservationConfirmationID,SeatID,Status, FlightID")] Reservation reservation)
        {
            if (id != reservation.ReservationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _reservationRepository.EditReservation(reservation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _reservationRepository.ExistEntity(reservation.ReservationID)))
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
            ViewData["CustomerID"] = new SelectList((await _reservationRepository.GetCustomers()), "CustomerID", "CustomerID", reservation.CustomerID);
            ViewData["SeatID"] = new SelectList((await _reservationRepository.GetSeats()), "SeatID", "SeatID", reservation.SeatID);
            ViewData["ReservationConfirmationID"] = new SelectList((await _reservationRepository.GetReservationConfirmations()), "ReservationConfirmationID", "ReservationConfirmationID", reservation.ReservationConfirmationID);
            ViewData["StatusID"] = new SelectList(_reservationRepository.GetStatuses(), "ID", "Status", reservation.Status);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (await _reservationRepository.Exist(id))
            {
                return NotFound();
            }

            var reservation = await _reservationRepository.GetReservationWithDataById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _reservationRepository.GetByIdAsync(id) == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reservations'  is null.");
            }
            var reservation = await _reservationRepository.GetReservationWithDataById(id);
            
            await _reservationRepository.DeleteReservation(reservation);
            return RedirectToAction(nameof(Index));
        }
    }
}

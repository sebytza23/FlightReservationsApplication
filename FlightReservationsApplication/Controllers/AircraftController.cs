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
using System.Security.Principal;

namespace FlightReservationsApplication.Controllers
{
    public class AircraftController : Controller
    {
        private readonly IAircraftRepository _aircraftRepository;

        public AircraftController(IAircraftRepository aircraftRepository)
        {
            _aircraftRepository = aircraftRepository;
        }

        // GET: Aircraft
        public async Task<IActionResult> Index()
        {
            return View(await _aircraftRepository.GetAllAsync());
        }

        // GET: Aircraft/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (await _aircraftRepository.Exist(id))
            {
                return NotFound();
            }

            var aircraft = await _aircraftRepository.Details(id);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        // GET: Aircraft/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aircraft/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Model,Capacity")] Aircraft aircraft)
        {
            if (ModelState.IsValid)
            {
                aircraft = await _aircraftRepository.CreateAircraft(aircraft);
                return RedirectToAction(nameof(Index));
            }
            return View(aircraft);
        }

        // GET: Aircraft/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (await _aircraftRepository.Exist(id))
            {
                return NotFound();
            }

            var aircraft = await _aircraftRepository.Details(id);
            if (aircraft == null)
            {
                return NotFound();
            }
            return View(aircraft);
        }

        // POST: Aircraft/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AircraftID,Model,Capacity")] Aircraft aircraft)
        {
            if (id != aircraft.AircraftID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _aircraftRepository.UpdateAsync(aircraft);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _aircraftRepository.ExistEntity(aircraft.AircraftID)))
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
            return View(aircraft);
        }

        // GET: Aircraft/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (await _aircraftRepository.Exist(id))
            {
                return NotFound();
            }

            var aircraft = await _aircraftRepository.Details(id);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        // POST: Aircraft/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _aircraftRepository.GetByIdAsync(id) == null)
            {
                return Problem("Entitatea 'ApplicationDbContext.Aircraft'  are valoarea null.");
            }
            var aircraft = await _aircraftRepository.Details(id);
            if (aircraft != null)
            {
                await _aircraftRepository.DeleteAsync(aircraft);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

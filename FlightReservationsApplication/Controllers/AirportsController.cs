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

namespace FlightReservationsApplication.Controllers
{
    public class AirportsController : Controller
    {
        private readonly IAirportRepository _airportRepository;

        public AirportsController(IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        // GET: Airport
        public async Task<IActionResult> Index()
        {
            return View(await _airportRepository.GetAllAsync());
        }

        // GET: Airport/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (await _airportRepository.Exist(id))
            {
                return NotFound();
            }

            var airport = await _airportRepository.Details(id);
            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        // GET: Airport/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Airport/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Location")] Airport airport)
        {
            if (ModelState.IsValid)
            {
                airport = await _airportRepository.CreateAirport(airport);
                return RedirectToAction(nameof(Index));
            }
            return View(airport);
        }

        // GET: Airport/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (await _airportRepository.Exist(id))
            {
                return NotFound();
            }

            var airport = await _airportRepository.Details(id);
            if (airport == null)
            {
                return NotFound();
            }
            return View(airport);
        }

        // POST: Airport/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AirportID, Name, Location")] Airport airport)
        {
            if (id != airport.AirportID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _airportRepository.UpdateAsync(airport);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _airportRepository.ExistEntity(airport.AirportID)))
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
            return View(airport);
        }

        // GET: Airport/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (await _airportRepository.Exist(id))
            {
                return NotFound();
            }

            var airport = await _airportRepository.Details(id);
            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        // POST: Airport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _airportRepository.GetByIdAsync(id) == null)
            {
                return Problem("Entitatea 'ApplicationDbContext.Airports'  are valoarea null.");
            }
            var airport = await _airportRepository.Details(id);
            if (airport != null)
            {
                await _airportRepository.DeleteAsync(airport);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

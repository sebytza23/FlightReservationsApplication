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
    public class AirlinesController : Controller
    {
        private readonly IAirlineRepository _airlineRepository;

        public AirlinesController(IAirlineRepository airlineRepository)
        {
            _airlineRepository = airlineRepository;
        }

        // GET: Airline
        public async Task<IActionResult> Index()
        {
            return View(await _airlineRepository.GetAllAsync());
        }

        // GET: Airline/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (await _airlineRepository.Exist(id))
            {
                return NotFound();
            }

            var airline = await _airlineRepository.Details(id);
            if (airline == null)
            {
                return NotFound();
            }

            return View(airline);
        }

        // GET: Airline/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Airline/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Airline airline)
        {
            if (ModelState.IsValid)
            {
                airline = await _airlineRepository.CreateAirline(airline);
                return RedirectToAction(nameof(Index));
            }
            return View(airline);
        }

        // GET: Airline/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (await _airlineRepository.Exist(id))
            {
                return NotFound();
            }

            var airline = await _airlineRepository.Details(id);
            if (airline == null)
            {
                return NotFound();
            }
            return View(airline);
        }

        // POST: Airline/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AirlineID,Name")] Airline airline)
        {
            if (id != airline.AirlineID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _airlineRepository.UpdateAsync(airline);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _airlineRepository.ExistEntity(airline.AirlineID)))
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
            return View(airline);
        }

        // GET: Airline/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (await _airlineRepository.Exist(id))
            {
                return NotFound();
            }

            var airline = await _airlineRepository.Details(id);
            if (airline == null)
            {
                return NotFound();
            }

            return View(airline);
        }

        // POST: Airline/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _airlineRepository.GetByIdAsync(id) == null)
            {
                return Problem("Entitatea 'ApplicationDbContext.Airlines'  are valoarea null.");
            }
            var airline = await _airlineRepository.Details(id);
            if (airline != null)
            {
                await _airlineRepository.DeleteAsync(airline);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

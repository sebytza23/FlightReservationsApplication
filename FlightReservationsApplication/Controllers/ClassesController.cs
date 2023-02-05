using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightReservationsApplication.Models;
using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Repository;
using System.Configuration;

namespace FlightReservationsApplication.Controllers
{
    public class ClassesController : Controller
    {
        private readonly IClassRepository _classRepository;

        public ClassesController(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }
        // GET: Classes
        public async Task<IActionResult> Index()
        {
            return View(await _classRepository.ClassWithAllData());
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (await _classRepository.Exist(id))
            {
                return NotFound();
            }

            var _class = await _classRepository.GetClassById(id);
            if (_class == null)
            {
                return NotFound();
            }

            return View(_class);
        }

        // GET: Classes/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AirlineID"] = new SelectList((await _classRepository.GetAirlines()), "AirlineID", "Name");
            ViewData["AirportID"] = new SelectList((await _classRepository.GetAirports()), "AirportID", "Name");
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassID,Number,AirportID,AirlineID,Price")] Class _class)
        {
            if (ModelState.IsValid)
            {
                _class = await _classRepository.CreateClass(_class);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AirlineID"] = new SelectList((await _classRepository.GetAirlines()), "AirlineID", "Name", _class.AirlineID);
            ViewData["AirportID"] = new SelectList((await _classRepository.GetAirports()), "AirportID", "Name", _class.AirportID);
            return View(_class);
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (await _classRepository.Exist(id))
            {
                return NotFound();
            }

            var _class = await _classRepository.Details(id);
            if (_class == null)
            {
                return NotFound();
            }
            ViewData["AirlineID"] = new SelectList((await _classRepository.GetAirlines()), "AirlineID", "Name", _class.AirlineID);
            ViewData["AirportID"] = new SelectList((await _classRepository.GetAirports()), "AirportID", "Name", _class.AirportID);
            return View(_class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassID,Number,AirportID,AirlineID,Price")] Class _class)
        {
            if (id != _class.ClassID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _classRepository.EditClass(_class);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _classRepository.ExistEntity(_class.ClassID)))
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
            ViewData["AirlineID"] = new SelectList((await _classRepository.GetAirlines()), "AirlineID", "Name", _class.AirlineID);
            ViewData["AirportID"] = new SelectList((await _classRepository.GetAirports()), "AirportID", "Name", _class.AirportID);
            return View(_class);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (await _classRepository.Exist(id))
            {
                return NotFound();
            }

            var _class = await _classRepository.ClassWithAllData();
            if (_class == null)
            {
                return NotFound();
            }

            return View(_class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _classRepository.GetByIdAsync(id) == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Classes'  is null.");
            }
            var contactInformation = await _classRepository.Details(id);
            await _classRepository.DeleteClass(contactInformation);
  
            return RedirectToAction(nameof(Index));
        }
    }
}

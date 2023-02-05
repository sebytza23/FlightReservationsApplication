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
    public class SalaryHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalaryHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SalaryHistories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SalaryHistories.Include(s => s.Employee).Include(s => s.NextSalaryHistory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SalaryHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SalaryHistories == null)
            {
                return NotFound();
            }

            var salaryHistory = await _context.SalaryHistories
                .Include(s => s.Employee)
                .Include(s => s.NextSalaryHistory)
                .FirstOrDefaultAsync(m => m.SalaryHistoryID == id);
            if (salaryHistory == null)
            {
                return NotFound();
            }

            return View(salaryHistory);
        }

        // GET: SalaryHistories/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeID");
            ViewData["NextSalaryHistoryID"] = new SelectList(_context.SalaryHistories, "SalaryHistoryID", "SalaryHistoryID");
            return View();
        }

        // POST: SalaryHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalaryHistoryID,EmployeeID,PreviousSalaryHistoryID,NextSalaryHistoryID,EffectiveDate,Amount")] SalaryHistory salaryHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salaryHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeID", salaryHistory.EmployeeID);
            ViewData["NextSalaryHistoryID"] = new SelectList(_context.SalaryHistories, "SalaryHistoryID", "SalaryHistoryID", salaryHistory.NextSalaryHistoryID);
            return View(salaryHistory);
        }

        // GET: SalaryHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SalaryHistories == null)
            {
                return NotFound();
            }

            var salaryHistory = await _context.SalaryHistories.FindAsync(id);
            if (salaryHistory == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeID", salaryHistory.EmployeeID);
            ViewData["NextSalaryHistoryID"] = new SelectList(_context.SalaryHistories, "SalaryHistoryID", "SalaryHistoryID", salaryHistory.NextSalaryHistoryID);
            return View(salaryHistory);
        }

        // POST: SalaryHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalaryHistoryID,EmployeeID,PreviousSalaryHistoryID,NextSalaryHistoryID,EffectiveDate,Amount")] SalaryHistory salaryHistory)
        {
            if (id != salaryHistory.SalaryHistoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salaryHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaryHistoryExists(salaryHistory.SalaryHistoryID))
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
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeID", salaryHistory.EmployeeID);
            ViewData["NextSalaryHistoryID"] = new SelectList(_context.SalaryHistories, "SalaryHistoryID", "SalaryHistoryID", salaryHistory.NextSalaryHistoryID);
            return View(salaryHistory);
        }

        // GET: SalaryHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SalaryHistories == null)
            {
                return NotFound();
            }

            var salaryHistory = await _context.SalaryHistories
                .Include(s => s.Employee)
                .Include(s => s.NextSalaryHistory)
                .FirstOrDefaultAsync(m => m.SalaryHistoryID == id);
            if (salaryHistory == null)
            {
                return NotFound();
            }

            return View(salaryHistory);
        }

        // POST: SalaryHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SalaryHistories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SalaryHistories'  is null.");
            }
            var salaryHistory = await _context.SalaryHistories.FindAsync(id);
            if (salaryHistory != null)
            {
                _context.SalaryHistories.Remove(salaryHistory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaryHistoryExists(int id)
        {
          return _context.SalaryHistories.Any(e => e.SalaryHistoryID == id);
        }
    }
}

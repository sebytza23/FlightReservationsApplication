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
    public class SalaryHistoriesController : Controller
    {
        private readonly ISalaryHistoryRepository _salaryHistoryRepository;

        public SalaryHistoriesController(ISalaryHistoryRepository salaryHistoryRepository)
        {
            _salaryHistoryRepository = salaryHistoryRepository;
        }


        // GET: SalaryHistories
        public async Task<IActionResult> Index()
        {
            return View(await _salaryHistoryRepository.SalaryWithEmployee());
        }

        // GET: SalaryHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (await _salaryHistoryRepository.Exist(id))
            {
                return NotFound();
            }

            var salaryHistory = await _salaryHistoryRepository.GetSalaryHistoryById(id);
            if (salaryHistory == null)
            {
                return NotFound();
            }

            return View(salaryHistory);
        }

        // GET: SalaryHistories/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["EmployeeID"] = new SelectList((await _salaryHistoryRepository.GetEmployees()), "EmployeeID", "EmployeeID");
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
                salaryHistory = await _salaryHistoryRepository.CreateSalaryHistory(salaryHistory);
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList((await _salaryHistoryRepository.GetEmployees()), "EmployeeID", "EmployeeID", salaryHistory.EmployeeID);
            return View(salaryHistory);
        }

        // GET: SalaryHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (await _salaryHistoryRepository.Exist(id))
            {
                return NotFound();
            }

            var salaryHistory = await _salaryHistoryRepository.Details(id);
            if (salaryHistory == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList((await _salaryHistoryRepository.GetEmployees()), "EmployeeID", "EmployeeID", salaryHistory.EmployeeID);
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
                    await _salaryHistoryRepository.UpdateAsync(salaryHistory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _salaryHistoryRepository.ExistEntity(salaryHistory.SalaryHistoryID)))
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
            ViewData["EmployeeID"] = new SelectList((await _salaryHistoryRepository.GetEmployees()), "EmployeeID", "EmployeeID", salaryHistory.EmployeeID);
            return View(salaryHistory);
        }

        // GET: SalaryHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (await _salaryHistoryRepository.Exist(id))
            {
                return NotFound();
            }

            var salaryHistory = await _salaryHistoryRepository.GetSalaryHistoryById(id);
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
            if (await _salaryHistoryRepository.GetSalaryHistoryById(id) == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SalaryHistories'  is null.");
            }
            var salaryHistory = await _salaryHistoryRepository.GetSalaryWithAll(id); ;
            await _salaryHistoryRepository.DeleteSalaryHistory(salaryHistory);

            return RedirectToAction(nameof(Index));
        }
    }
}

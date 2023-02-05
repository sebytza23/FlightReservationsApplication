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
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _employeeRepository.EmployeesWithAccount());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (await _employeeRepository.Exist(id))
            {
                return NotFound();
            }

            var employee = await _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AccountID"] = new SelectList((await _employeeRepository.GetAccounts()), "AccountID", "Email");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeID,AccountID,FirstName,LastName,IsAdmin")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee = await _employeeRepository.CreateEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountID"] = new SelectList((await _employeeRepository.GetAccounts()), "AccountID", "Email", employee.AccountID);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (await _employeeRepository.Exist(id))
            {
                return NotFound();
            }

            var employee = await _employeeRepository.Details(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["AccountID"] = new SelectList((await _employeeRepository.GetAccounts()), "AccountID", "Email", employee.AccountID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeID,AccountID,FirstName,LastName,IsAdmin")] Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _employeeRepository.EditEmployee(employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _employeeRepository.ExistEntity(employee.EmployeeID)))
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
            ViewData["AccountID"] = new SelectList((await _employeeRepository.GetAccounts()), "AccountID", "Email", employee.AccountID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (await _employeeRepository.Exist(id))
            {
                return NotFound();
            }

            var employee = await _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _employeeRepository.GetByIdAsync(id) == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Employees'  is null.");
            }
            var employee = await _employeeRepository.Details(id);

            await _employeeRepository.DeleteEmployee(employee);
            return RedirectToAction(nameof(Index));
        }
    }
}

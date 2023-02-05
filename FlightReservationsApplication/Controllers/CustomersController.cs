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
using System.Configuration;

namespace FlightReservationsApplication.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _customerRepository.CustomersWithAccount());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (await _customerRepository.Exist(id))
            {
                return NotFound();
            }

            var customer = await _customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["AccountID"] = new SelectList((await _customerRepository.GetAccounts()), "AccountID", "Email");
            ViewData["CreditCardID"] = new SelectList((await _customerRepository.GetCreditCards()), "CreditCardID", "CardNumber");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,AccountID,CreditCardID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer = await _customerRepository.CreateCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountID"] = new SelectList((await _customerRepository.GetAccounts()), "AccountID", "Email", customer.AccountID);
            ViewData["CreditCardID"] = new SelectList((await _customerRepository.GetCreditCards()), "CreditCardID", "CardNumber", customer.CreditCardID); 
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (await _customerRepository.Exist(id))
            {
                return NotFound();
            }

            var customer = await _customerRepository.Details(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["AccountID"] = new SelectList((await _customerRepository.GetAccounts()), "AccountID", "Email", customer.AccountID);
            ViewData["CreditCardID"] = new SelectList((await _customerRepository.GetCreditCards()), "CreditCardID", "CardNumber", customer.CreditCardID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerID,AccountID,CreditCardID")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _customerRepository.EditCustomer(customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _customerRepository.ExistEntity(customer.CustomerID)))
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
            ViewData["AccountID"] = new SelectList((await _customerRepository.GetAccounts()), "AccountID", "Email", customer.AccountID);
            ViewData["CreditCardID"] = new SelectList((await _customerRepository.GetCreditCards()), "CreditCardID", "CardNumber", customer.CreditCardID);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (await _customerRepository.Exist(id))
            {
                return NotFound();
            }

            var customer = await _customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _customerRepository.GetByIdAsync(id) == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Customers'  is null.");
            }
            var customer = await _customerRepository.Details(id);
            await _customerRepository.DeleteCustomer(customer);
            return RedirectToAction(nameof(Index));
        }
    }
}

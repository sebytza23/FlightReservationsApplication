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
    public class CreditCardsController : Controller
    {
        private readonly ICreditCardRepository _creditCardRepository;

        public CreditCardsController(ICreditCardRepository creditCardRepository)
        {
            _creditCardRepository = creditCardRepository;
        }

        // GET: CreditCards
        public async Task<IActionResult> Index()
        {
            return View(await _creditCardRepository.CardsWithCustomers());
        }

        // GET: CreditCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (await _creditCardRepository.Exist(id))
            {
                return NotFound();
            }

            var creditCard = await _creditCardRepository.GetCreditCardById(id);
            if (creditCard == null)
            {
                return NotFound();
            }

            return View(creditCard);
        }

        // GET: CreditCards/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["CustomerID"] = new SelectList((await _creditCardRepository.GetCustomers()), "CustomerID", "Account.Email");
            return View();
        }

        // POST: CreditCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,CardNumber,CardHolderName,ExpirationDate,SecurityCode,IsPrimary")] CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                creditCard = await _creditCardRepository.CreateCreditCard(creditCard);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList((await _creditCardRepository.GetCustomers()), "CustomerID", "Account.Email",creditCard.CustomerID);
            return View(creditCard);
        }

        // GET: CreditCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (await _creditCardRepository.Exist(id))
            {
                return NotFound();
            }

            var creditCard = await _creditCardRepository.Details(id);
            if (creditCard == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList((await _creditCardRepository.GetCustomers()), "CustomerID", "Account.Email", creditCard.CustomerID);
            return View(creditCard);
        }

        // POST: CreditCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CreditCardID,CustomerID,CardNumber,CardHolderName,ExpirationDate,SecurityCode,IsPrimary")] CreditCard creditCard)
        {
            if (id != creditCard.CreditCardID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _creditCardRepository.EditCreditCard(creditCard);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _creditCardRepository.ExistEntity(creditCard.CreditCardID)))
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
            ViewData["CustomerID"] = new SelectList((await _creditCardRepository.GetCustomers()), "CustomerID", "Account.Email", creditCard.CustomerID);
            return View(creditCard);
        }

        // GET: CreditCards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (await _creditCardRepository.Exist(id))
            {
                return NotFound();
            }

            var creditCard = await _creditCardRepository.GetCreditCardById(id);

            if (creditCard == null)
            {
                return NotFound();
            }

            return View(creditCard);
        }

        // POST: CreditCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _creditCardRepository.GetByIdAsync(id) == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CreditCards'  is null.");
            }
            var creditCard = await _creditCardRepository.Details(id);
            await _creditCardRepository.DeleteCreditCard(creditCard);
            return RedirectToAction(nameof(Index));
        }
    }
}

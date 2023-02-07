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
using System.Security.Principal;
using FlightReservationsApplication.Attributes;

namespace FlightReservationsApplication.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] Account account)
        {
            LoginController login = new LoginController(_accountRepository);
            var result = await login.Login(account);
            if (result is OkResult)
            {
                return RedirectToAction("Home", "Index");
            }
            else if (result is NotFoundResult)
            {
                ViewData["Error"] = "Acest cont nu exista.";
            }
            else if (result is UnauthorizedResult)
            {
                ViewData["Error"] = "Parola introdusa este gresita.";
            }
            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Password,FirstName,LastName,IsEmployee")] Account account)
        {
            if (ModelState.IsValid)
            {


                account = await _accountRepository.CreateAccount(account);
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts
       public async Task<IActionResult> Index()
        {
            return View(await _accountRepository.GetAllAsync());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (await _accountRepository.Exist(id))
            {
                return NotFound();
            }

            var account = await _accountRepository.Details(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }


        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (await _accountRepository.Exist(id))
            {
                return NotFound();
            }

            var account = await _accountRepository.Details(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["ContactInformationID"] = new SelectList(await _accountRepository.GetContactInformation(account.AccountID), "ContactInformationID", "AddressLine1");
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ContactInformationID,AccountID,Email,Password,FirstName,LastName,IsEmployee, CustomerID, EmployeeID")] Account account)
        {
            if (id != account.AccountID)
            {
                return NotFound();
            }

            Account oldAccount = await _accountRepository.Details(id);

            if (oldAccount.Password != account.Password)
            {
                TempData["passwordError"] = "Parola introdusa este gresita!";
                return View(account);
            }
            _accountRepository.State(oldAccount, EntityState.Detached);
            if (ModelState.IsValid)
            {
                try
                {
                    await _accountRepository.UpdateAsync(account);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _accountRepository.ExistEntity(account.AccountID)))
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
            ViewData["ContactInformationID"] = new SelectList(await _accountRepository.GetContactInformation(account.AccountID), "ContactInformationID", "AddressLine1");
            return View(account);
        }
 
        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (await _accountRepository.Exist(id))
            {
                return NotFound();
            }

            var account = await _accountRepository.Details(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (await _accountRepository.GetByIdAsync(id) == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Accounts'  is null.");
            }
            var account = await _accountRepository.Details(id);
            if (account != null)
            {
                await _accountRepository.DeleteAsync(account);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

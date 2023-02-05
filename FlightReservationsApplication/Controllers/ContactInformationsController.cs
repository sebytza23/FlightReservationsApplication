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
    public class ContactInformationsController : Controller
    {
        private readonly IContactInformationRepository _contactInformationRepository;

        public ContactInformationsController(IContactInformationRepository contactInformationRepository)
        {
            _contactInformationRepository = contactInformationRepository;
        }

        // GET: ContactInformations
        public async Task<IActionResult> Index()
        {
            return View(await _contactInformationRepository.InformationsWithAccounts());
        }

        // GET: ContactInformations/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (await _contactInformationRepository.Exist(id))
            {
                return NotFound();
            }

            var contactInformation = await _contactInformationRepository.GetContactInformationById(id);
            if (contactInformation == null)
            {
                return NotFound();
            }

            return View(contactInformation);
        }

        // GET: ContactInformations/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["AccountID"] = new SelectList((await _contactInformationRepository.GetAccounts()), "AccountID", "Email");
            return View();
        }

        // POST: ContactInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountID,AddressLine1,AddressLine2,City,State,ZipCode,PhoneNumber, IsPrimary")] ContactInformation contactInformation)
        {
            if (ModelState.IsValid)
            {
                contactInformation = await _contactInformationRepository.CreateContactInformation(contactInformation);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountID"] = new SelectList((await _contactInformationRepository.GetAccounts()), "AccountID", "Email", contactInformation.AccountID);
            return View(contactInformation);
        }

        // GET: ContactInformations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (await _contactInformationRepository.Exist(id))
            {
                return NotFound();
            }

            var contactInformation = await _contactInformationRepository.Details(id);
            if (contactInformation == null)
            {
                return NotFound();
            }
            ViewData["AccountID"] = new SelectList(await _contactInformationRepository.GetAccounts(), "AccountID", "Email", contactInformation.AccountID);
            return View(contactInformation);
        }

        // POST: ContactInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactInformationID,AccountID,AddressLine1,AddressLine2,City,State,ZipCode,PhoneNumber, IsPrimary")] ContactInformation contactInformation)
        {
            if (id != contactInformation.ContactInformationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _contactInformationRepository.EditContactInformation(contactInformation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _contactInformationRepository.ExistEntity(contactInformation.ContactInformationID)))
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
            ViewData["AccountID"] = new SelectList(await _contactInformationRepository.GetAccounts(), "AccountID", "Email", contactInformation.AccountID);
            return View(contactInformation);
        }

        // GET: ContactInformations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (await _contactInformationRepository.Exist(id))
            {
                return NotFound();
            }

            var contactInformation = await _contactInformationRepository.GetContactInformationById(id);
            if (contactInformation == null)
            {
                return NotFound();
            }

            return View(contactInformation);
        }

        // POST: ContactInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _contactInformationRepository.GetByIdAsync(id) == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ContactInformations'  is null.");
            }
            var contactInformation = await _contactInformationRepository.Details(id);
            
            await _contactInformationRepository.DeleteContactInformation(contactInformation);

            return RedirectToAction(nameof(Index));
        }

    }
}

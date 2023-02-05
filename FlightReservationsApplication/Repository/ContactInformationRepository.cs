using FlightReservationsApplication.Context;
using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Models;
using FlightReservationsApplication.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Principal;

namespace FlightReservationsApplication.Repository
{
    public class ContactInformationRepository : Repository<ContactInformation>, IContactInformationRepository
    {
        public ContactInformationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ContactInformation> CreateContactInformation(ContactInformation contactInformation)
        {
            await _context.ContactInformations.AddAsync(contactInformation);
            await _context.SaveChangesAsync();
            await UpdateAccount(contactInformation);
            return contactInformation;
        }

        public async Task EditContactInformation(ContactInformation contactInformation)
        {
            await UpdateAsync(contactInformation);
            await UpdateAccount(contactInformation);
        }

        public async Task DeleteContactInformation(ContactInformation contactInformation) {

            if (contactInformation == null) return;
            contactInformation.Account = await FindAccount(contactInformation);
            if (contactInformation.Account.ContactInformationID == contactInformation.ContactInformationID)
            {
                Account account = contactInformation.Account;
                account.ContactInformationID = null;
                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();
            }
            await DeleteAsync(contactInformation);
            
        }

        public async Task<List<ContactInformation>> InformationsWithAccounts()
        {
            string[] includes = { "Account" };
            List<ContactInformation> contactInformation = await (await Include(includes)).ToListAsync();
            return contactInformation;
        }

        public async Task<ContactInformation> GetContactInformationById(int? id)
        {
            string[] includes = { "Account" };
            return await (await Include(includes)).FirstOrDefaultAsync(c => c.ContactInformationID == id);
        }

        public async Task UpdateAccount(ContactInformation contactInformation)
        {
            Account account = await FindAccount(contactInformation);

            if (contactInformation.IsPrimary || account.ContactInformationID == null)
            {
                account.ContactInformationID = contactInformation.ContactInformationID;
            }
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }
        public async Task<DbSet<Account>> GetAccounts()
        {
            return _context.Accounts;
        }

        public async Task<Account> FindAccount(ContactInformation contactInformation) => await (await GetAccounts()).FirstOrDefaultAsync(a => a.AccountID == contactInformation.AccountID);
        

    }
}

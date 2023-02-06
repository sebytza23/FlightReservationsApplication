using FlightReservationsApplication.Context;
using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace FlightReservationsApplication.Repository
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Accounts.AnyAsync(a => a.Email == email);
        }

        public async Task<Account> GetByEmailAsync(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<Account> CreateAccount(Account account)
        {
            account.AccountID = Guid.NewGuid();
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            if (account.IsEmployee)
            {
                Employee employee = new Employee
                {
                    AccountID = account.AccountID,
                    IsAdmin = false,
                };
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                account.EmployeeID = employee.EmployeeID;
            }
            else
            {
                Customer customer = new Customer
                {
                    AccountID = account.AccountID,
                };
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                account.CustomerID = customer.CustomerID;
            }
            await UpdateAsync(account);
            return account;
        }

        public async Task<IQueryable<ContactInformation>> GetContactInformation(Guid id){
            var result = await _context.ContactInformations.Where(c => c.AccountID == id).ToListAsync();
            return result.AsQueryable();
        }

    }
}

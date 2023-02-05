using FlightReservationsApplication.Context;
using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Models;
using FlightReservationsApplication.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Principal;

namespace FlightReservationsApplication.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task EditCustomer(Customer customer)
        {
            await UpdateAsync(customer);
        }

        public async Task DeleteCustomer(Customer customer)
        {

            if (customer == null) return;
            
            await DeleteAsync(customer);

        }

        public async Task<List<Customer>> CustomersWithAccount()
        {
            string[] includes = { "Account" };
            List<Customer> customers = await (await Include(includes)).ToListAsync();
            return customers;
        }

        public async Task<Customer> GetCustomerById(int? id)
        {
            string[] includes = { "Account" };
            return await (await Include(includes)).FirstOrDefaultAsync(c => c.CustomerID == id);
        }

        public async Task<List<Account>> GetAccounts()
        {
            return await _context.Accounts.Where(c => c.IsEmployee == false).ToListAsync();
        }
        public async Task<List<CreditCard>> GetCreditCards()
        {
            return await _context.CreditCards.ToListAsync();
        }



    }
}

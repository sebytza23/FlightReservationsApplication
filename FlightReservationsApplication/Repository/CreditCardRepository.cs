using FlightReservationsApplication.Context;
using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Models;
using FlightReservationsApplication.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Principal;

namespace FlightReservationsApplication.Repository
{
    public class CreditCardRepository : Repository<CreditCard>, ICreditCardRepository
    {
        public CreditCardRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<CreditCard> CreateCreditCard(CreditCard creditCard)
        {
            await _context.CreditCards.AddAsync(creditCard);
            await _context.SaveChangesAsync();
            await UpdateCustomer(creditCard);
            return creditCard;
        }

        public async Task EditCreditCard(CreditCard creditCard)
        {
            await UpdateAsync(creditCard);
            await UpdateCustomer(creditCard);
        }

        public async Task DeleteCreditCard(CreditCard creditCard)
        {
            if (creditCard == null) return;
            creditCard.Customer = await FindCustomer(creditCard);
            if (creditCard.Customer.CreditCardID == creditCard.CreditCardID)
            {
                Customer customer = creditCard.Customer;
                customer.CreditCardID = null;
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
            }
            await DeleteAsync(creditCard);
        }

        public async Task<List<CreditCard>> CreditCardsWithCustomers()
        {
            return await _context.CreditCards.Include(c => c.Customer).ToListAsync();
        }

        public async Task UpdateCustomer(CreditCard creditCard)
        {
            Customer customer = await FindCustomer(creditCard);

            if (creditCard.IsPrimary || customer.CreditCardID == null)
            {
                customer.CreditCardID = creditCard.CreditCardID;
            }
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<DbSet<Customer>> GetCustomers()
        {
            return _context.Customers;
        }
        
        public async Task<Customer> FindCustomer(CreditCard creditCard) => await (await GetCustomers()).FirstOrDefaultAsync(a => a.CustomerID == creditCard.CustomerID);
        public async Task<List<CreditCard>> CardsWithCustomers()
        {
            string[] includes = { "Customer" };
            List<CreditCard> creditCards = await (await Include(includes)).ToListAsync();
            return creditCards;
        }

        public async Task<CreditCard> GetCreditCardById(int? id)
        {
            string[] includes = { "Customer" };
            return await (await Include(includes)).FirstOrDefaultAsync(c => c.CreditCardID == id);
        }

    }
}

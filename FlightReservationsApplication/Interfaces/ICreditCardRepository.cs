using FlightReservationsApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationsApplication.Interfaces
{
    public interface ICreditCardRepository : IRepository<CreditCard>
    {
        Task<CreditCard> CreateCreditCard(CreditCard creditCard);
        Task<Customer> FindCustomer(CreditCard creditCard);
        Task<List<CreditCard>> CardsWithCustomers();
        Task<List<CreditCard>> CreditCardsWithCustomers();
        Task DeleteCreditCard(CreditCard creditCard);
        Task<CreditCard> GetCreditCardById(int? id);
        Task UpdateCustomer(CreditCard creditCard);
        Task EditCreditCard(CreditCard creditCard);
        Task<DbSet<Customer>> GetCustomers();
    }
}

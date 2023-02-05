using FlightReservationsApplication.Models;

namespace FlightReservationsApplication.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> CreateCustomer(Customer customer);
        Task EditCustomer(Customer customer);
        Task DeleteCustomer(Customer customer); 
        Task<List<Customer>> CustomersWithAccount();
        Task<Customer> GetCustomerById(int? id);
        Task<List<Account>> GetAccounts();
        Task<List<CreditCard>> GetCreditCards();
    }
}

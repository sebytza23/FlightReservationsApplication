using FlightReservationsApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationsApplication.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
        Task<Account> CreateAccount(Account account);
        Task<IQueryable<ContactInformation>> GetContactInformation(Guid id);
    }
}

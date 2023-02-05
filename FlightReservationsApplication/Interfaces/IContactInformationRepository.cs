using FlightReservationsApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationsApplication.Interfaces
{
    public interface IContactInformationRepository : IRepository<ContactInformation>
    {
        Task<ContactInformation> CreateContactInformation(ContactInformation contactInformation);
        Task<List<ContactInformation>> InformationsWithAccounts();
        Task<ContactInformation> GetContactInformationById(int? id);
        Task<DbSet<Account>> GetAccounts();
        Task DeleteContactInformation(ContactInformation contactInformation);
        Task EditContactInformation(ContactInformation contactInformation);
        Task<Account> FindAccount(ContactInformation contactInformation);
        Task UpdateAccount(ContactInformation contactInformation);
    }
}

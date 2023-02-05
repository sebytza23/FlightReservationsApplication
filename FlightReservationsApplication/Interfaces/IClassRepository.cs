using FlightReservationsApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationsApplication.Interfaces
{
    public interface IClassRepository : IRepository<Class>
    {
        Task<Class> CreateClass(Class _class);
        Task<List<Class>> ClassWithAllData();
        Task DeleteClass(Class _class);
        Task<Class> GetClassById(int? id);
        Task EditClass(Class _class);
        Task<DbSet<Airline>> GetAirlines();
        Task<DbSet<Airport>> GetAirports();
    }
}

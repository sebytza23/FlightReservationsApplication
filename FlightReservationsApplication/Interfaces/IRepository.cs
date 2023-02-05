using Microsoft.EntityFrameworkCore;

namespace FlightReservationsApplication.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(dynamic id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        void State(T entity, EntityState State);
        Task<bool> Exist(dynamic id);
        Task<T> Details(dynamic id);
        Task<bool> ExistEntity(dynamic id);
        Task<IQueryable<T>> Include(string[] includes);
    }
}

using FlightReservationsApplication.Context;
using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace FlightReservationsApplication.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T> GetByIdAsync(dynamic id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual void State(T entity, EntityState State)
        {
            _context.Entry(entity).State = State;
        }

        public async Task<bool> Exist(dynamic id)
        {
            return id == null || (await GetAllAsync()).ToArray().Length == 0;
        }

        public async Task<T> Details(dynamic id)
        {
            T entity = await GetByIdAsync(id);
            return entity;
        }

        public async Task<bool> ExistEntity(dynamic id)
        {
            return (await Details(id)) != null;
        }

        public async Task<IQueryable<T>> Include(string[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (string include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }
    }
}

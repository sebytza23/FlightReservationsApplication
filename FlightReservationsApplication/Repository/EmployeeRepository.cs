using FlightReservationsApplication.Context;
using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Models;
using FlightReservationsApplication.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Principal;

namespace FlightReservationsApplication.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task EditEmployee(Employee employee)
        {
            await UpdateAsync(employee);
        }

        public async Task DeleteEmployee(Employee employee)
        {

            if (employee == null) return;

            await DeleteAsync(employee);

        }

        public async Task<List<Employee>> EmployeesWithAccount()
        {
            string[] includes = { "Account" };
            List<Employee> employees = await (await Include(includes)).ToListAsync();
            return employees;
        }

        public async Task<Employee> GetEmployeeById(int? id)
        {
            string[] includes = { "Account" };
            return await (await Include(includes)).FirstOrDefaultAsync(c => c.EmployeeID == id);
        }

        public async Task<List<Account>> GetAccounts()
        {
            return await _context.Accounts.Where(c => c.IsEmployee == true).ToListAsync();
        }
    }
}

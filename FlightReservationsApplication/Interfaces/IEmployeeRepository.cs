using FlightReservationsApplication.Models;

namespace FlightReservationsApplication.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> CreateEmployee(Employee customer);
        Task EditEmployee(Employee customer);
        Task DeleteEmployee(Employee customer);
        Task<List<Employee>> EmployeesWithAccount();
        Task<Employee> GetEmployeeById(int? id);
        Task<List<Account>> GetAccounts();
    }
}

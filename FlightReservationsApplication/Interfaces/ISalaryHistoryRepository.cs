using FlightReservationsApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationsApplication.Interfaces
{
    public interface ISalaryHistoryRepository : IRepository<SalaryHistory>
    {
        Task<SalaryHistory> CreateSalaryHistory(SalaryHistory salaryHistory);
        Task EditSalaryHistory(SalaryHistory salaryHistory);
        Task DeleteSalaryHistory(SalaryHistory salaryHistory);
        Task<SalaryHistory> GetSalaryHistoryById(int? id);
        Task<List<SalaryHistory>> SalaryWithEmployee();
        Task<SalaryHistory> GetSalaryWithAll (int? id);
        Task<List<Employee>> GetEmployees();

    }
}

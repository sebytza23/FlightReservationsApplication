using FlightReservationsApplication.Models;

namespace FlightReservationsApplication.Interfaces
{
    public interface ISalaryHistoryRepository : IRepository<SalaryHistory>
    {
        Task<SalaryHistory> CreateSalaryHistory(SalaryHistory salaryHistory);
    }
}

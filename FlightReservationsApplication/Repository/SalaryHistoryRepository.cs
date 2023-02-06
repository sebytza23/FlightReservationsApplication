using FlightReservationsApplication.Context;
using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Models;
using FlightReservationsApplication.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Principal;

namespace FlightReservationsApplication.Repository
{
    public class SalaryHistoryRepository : Repository<SalaryHistory>, ISalaryHistoryRepository
    {
        public SalaryHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<SalaryHistory> CreateSalaryHistory(SalaryHistory salaryHistory)
        {
            SalaryHistory lastSalaryHistory = (await _context.SalaryHistories.Where(s => s.EmployeeID == salaryHistory.EmployeeID).ToListAsync()).LastOrDefault();
            await _context.SalaryHistories.AddAsync(salaryHistory);
            await _context.SaveChangesAsync();
            if (lastSalaryHistory != null)
            {
                lastSalaryHistory.NextSalaryHistoryID = salaryHistory.SalaryHistoryID;
                _context.SalaryHistories.Update(lastSalaryHistory);
                await _context.SaveChangesAsync();
                salaryHistory.PreviousSalaryHistoryID = lastSalaryHistory.SalaryHistoryID;
                _context.SalaryHistories.Update(lastSalaryHistory);
                await _context.SaveChangesAsync();
            }
            return salaryHistory;
        }

        public async Task EditSalaryHistory(SalaryHistory salaryHistory)
        {
            await UpdateAsync(salaryHistory);
        }

        public async Task DeleteSalaryHistory(SalaryHistory salaryHistory)
        {
            if (salaryHistory == null) return;
            if (salaryHistory.PreviousSalaryHistoryID != null)
            {
                salaryHistory.PreviousSalaryHistory.NextSalaryHistoryID = salaryHistory.NextSalaryHistoryID;
                if(salaryHistory.NextSalaryHistoryID != null)
                {
                    salaryHistory.NextSalaryHistory.PreviousSalaryHistoryID = salaryHistory.PreviousSalaryHistory.SalaryHistoryID;
                    _context.SalaryHistories.Update(salaryHistory);
                    await _context.SaveChangesAsync();
                }
            }else if(salaryHistory.NextSalaryHistoryID != null)
            {
                salaryHistory.NextSalaryHistory.PreviousSalaryHistoryID = null;
                _context.SalaryHistories.Update(salaryHistory);
                await _context.SaveChangesAsync();
            }
            await DeleteAsync(salaryHistory);

        }

        public async Task<List<SalaryHistory>> SalaryWithEmployee()
        {
            string[] includes = { "Employee" };
            List<SalaryHistory> salaryHistory = await (await Include(includes)).ToListAsync();
            return salaryHistory;
        }

        public async Task<SalaryHistory> GetSalaryWithAll(int? id)
        {
            string[] includes = { "Employee","NextSalaryHistory","PreviousSalaryHistory" };
            return await (await Include(includes)).FirstOrDefaultAsync(c => c.SalaryHistoryID == id);
        }

        public async Task<SalaryHistory> GetSalaryHistoryById(int? id)
        {
            string[] includes = { "Employee" };
            return await (await Include(includes)).FirstOrDefaultAsync(c => c.SalaryHistoryID == id);
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        

    }
}

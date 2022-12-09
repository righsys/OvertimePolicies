using OvertimePolicies.Domain.DbViews;
using OvertimePolicies.Domain.Entities;
using OvertimePolicies.Services.DTOs;
using OvertimePolicies.SharedKernel.Interfaces;

namespace OvertimePolicies.Services.Interfaces.DapperRepositories
{
    public interface IDapperEmployeeSalaryRepository : IDapperRepository<EmployeeSalary>
    {
        Task<IEnumerable<EmployeeSalaryDbView>> GetALlEmployeeSalariesByEmployeeId(int employeeId);
        Task<EmployeeSalaryDbView> GetEmployeeSalaryByMonth(int employeeId,int year, int monthNumber);
        Task<IEnumerable<EmployeeSalaryDbView>> GetAllEmployeeSalariesByDateRange(int employeeId, int startYear, int endYear, int startMonth, int endMonth);
    }
}

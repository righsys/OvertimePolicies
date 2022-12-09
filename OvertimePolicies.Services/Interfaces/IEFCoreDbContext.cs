using Microsoft.EntityFrameworkCore;
using OvertimePolicies.Domain.Entities;

namespace OvertimePolicies.Services.Interfaces
{
    public interface IEFCoreDbContext
    {
        DbSet<Employee> Employees { get; set; }
        DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
    }
}

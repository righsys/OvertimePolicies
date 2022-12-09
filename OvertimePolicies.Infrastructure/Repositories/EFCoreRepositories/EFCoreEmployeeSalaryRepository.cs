using Microsoft.EntityFrameworkCore;
using OvertimePolicies.Domain.Entities;
using OvertimePolicies.Infrastructure.DbContexts;
using OvertimePolicies.Services.Interfaces.EFCoreRepositories;

namespace OvertimePolicies.Infrastructure.Repositories.EFCoreRepositories
{
    public class EFCoreEmployeeSalaryRepository : EFCoreRepositoryBase<EmployeeSalary, int>, IEFCoreEmployeeSalaryRepository
    {
        private readonly EFCoreDbContext _employeeDbContext;

        public EFCoreEmployeeSalaryRepository(EFCoreDbContext employeeDbContext) : base(employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task DeleteEmployeeSalary(int salaryId)
        {
            string cmd = $"delete from EmployeeSalary where EmployeeSalaryId={salaryId}";
            await _employeeDbContext.Database.ExecuteSqlRawAsync(cmd);
        }
        
        public async Task<EmployeeSalary?> GetEmployeeSalaryByData(int employeeId, int year, int month)
        {
            try
            {
                EmployeeSalary salary = await _employeeDbContext.EmployeeSalaries.FirstAsync(x => x.EmployeeId == employeeId && x.Year == year && x.Month == month);
                return salary;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public override Task<IQueryable<EmployeeSalary>> GetObjectSet() => Task.Run(() => _employeeDbContext.EmployeeSalaries.AsQueryable());
    }
}
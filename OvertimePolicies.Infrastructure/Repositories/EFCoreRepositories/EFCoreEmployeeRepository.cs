using Microsoft.EntityFrameworkCore;
using OvertimePolicies.Domain.Entities;
using OvertimePolicies.Infrastructure.DbContexts;
using OvertimePolicies.Services.Interfaces.EFCoreRepositories;
using OvertimePolicies.WebApp.Common.DatetimeHelper;

namespace OvertimePolicies.Infrastructure.Repositories.EFCoreRepositories
{
    public class EFCoreEmployeeRepository : EFCoreRepositoryBase<Employee, int>, IEFCoreEmployeeRepository
    {
        private readonly EFCoreDbContext _employeeDbContext;
        

        public EFCoreEmployeeRepository(EFCoreDbContext employeeDbContext, IDateTimeHelper dateTimeHelper) : base(employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<Employee?> GetEmployeeByName(string firstname, string lastname)
        {
            try
            {
                Employee employee = await _employeeDbContext.Employees.FirstAsync(x => x.FirstName == firstname && x.LastName == lastname);
                return employee;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public override async Task<IQueryable<Employee>> GetObjectSet() => await Task.Run(() => _employeeDbContext.Employees.AsQueryable());
    }
}
using OvertimePolicies.Domain.Entities;
using OvertimePolicies.Services.DTOs;
using OvertimePolicies.SharedKernel.Interfaces;

namespace OvertimePolicies.Services.Interfaces.DapperRepositories
{
    public interface IDapperEmployeeRepository : IDapperRepository<Employee>
    {
        Task<IQueryable<Employee>> GetAllEmployees();
    }
}

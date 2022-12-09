using OvertimePolicies.Domain.Entities;
using OvertimePolicies.Services.DTOs;
using OvertimePolicies.SharedKernel.Interfaces;

namespace OvertimePolicies.Services.Interfaces.EFCoreRepositories
{
    public interface IEFCoreEmployeeRepository : IEFCoreRepository<Employee, int>
    {
        Task<Employee> GetEmployeeByName(string firstname, string lastname);
    }
}

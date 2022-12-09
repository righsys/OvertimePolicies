using OvertimePolicies.Domain.Entities;
using OvertimePolicies.Services.DTOs;
using OvertimePolicies.SharedKernel.Interfaces;

namespace OvertimePolicies.Services.Interfaces.EFCoreRepositories
{
    public interface IEFCoreEmployeeSalaryRepository : IEFCoreRepository<EmployeeSalary, int>
    {        
        Task DeleteEmployeeSalary(int salaryId);
    }
}

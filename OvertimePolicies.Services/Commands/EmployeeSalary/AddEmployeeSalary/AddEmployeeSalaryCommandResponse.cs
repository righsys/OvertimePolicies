using OvertimePolicies.Services.Common;
using OvertimePolicies.Services.DTOs;

namespace OvertimePolicies.Services.Commands.EmployeeSalary.AddEmployeeSalary
{
    public class AddEmployeeSalaryCommandResponse : CommandQueryResponseBase
    {
        public EmployeeSalaryDto JustCreatedEmployeeSalary { get; set; }
    }
}
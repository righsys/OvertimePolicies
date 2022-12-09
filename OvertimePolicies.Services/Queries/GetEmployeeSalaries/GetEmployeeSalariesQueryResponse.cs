using OvertimePolicies.Services.Common;
using OvertimePolicies.Services.DTOs;

namespace OvertimePolicies.Services.Queries.GetEmployeeSalaries
{
    public class GetEmployeeSalariesQueryResponse : CommandQueryResponseBase
    {
        public List<EmployeeSalaryDto> EmployeeSalaries { get; set; }
    }
}
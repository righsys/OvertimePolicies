using OvertimePolicies.Services.Common;
using OvertimePolicies.Services.DTOs;

namespace OvertimePolicies.Services.Queries.GetEmployeeSalaryByMonthRange
{
    public class GetEmployeeSalaryByMonthRangeQueryResponse : CommandQueryResponseBase
    {
        public List<EmployeeSalaryDto> EmployeeSalaries { get; set; }
    }
}
using OvertimePolicies.Services.Common;
using OvertimePolicies.Services.DTOs;

namespace OvertimePolicies.Services.Queries.GetEmployeeSalaryByMonth
{
    public class GetEmployeeSalaryByMonthQueryResponse : CommandQueryResponseBase
    {
        public EmployeeSalaryDto EmployeeSalary { get; set; }
    }
}
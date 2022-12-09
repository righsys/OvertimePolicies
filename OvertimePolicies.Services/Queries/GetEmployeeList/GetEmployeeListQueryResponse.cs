using OvertimePolicies.Services.Common;
using OvertimePolicies.Services.DTOs;

namespace OvertimePolicies.Services.Queries.GetEmployeeList
{
    public class GetEmployeeListQueryResponse : CommandQueryResponseBase
    {
        public List<EmployeeDto> Employees { get; set; }
    }
}
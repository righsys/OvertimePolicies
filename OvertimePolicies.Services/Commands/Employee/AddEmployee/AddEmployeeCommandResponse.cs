using OvertimePolicies.Services.Common;
using OvertimePolicies.Services.DTOs;

namespace OvertimePolicies.Services.Commands.Employee.AddEmployee
{
    public class AddEmployeeCommandResponse : CommandQueryResponseBase
    {
        public EmployeeDto Employee { get; set; }
    }
}
using MediatR;

namespace OvertimePolicies.Services.Commands.Employee.AddEmployee
{
    public class AddEmployeeCommand : IRequest<AddEmployeeCommandResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EmploymentDate { get; set; }
    }
}
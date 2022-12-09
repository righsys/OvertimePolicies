using MediatR;

namespace OvertimePolicies.Services.Commands.EmployeeSalary.DeleteEmployeeSalary
{
    public class DeleteEmployeeSalaryCommand : IRequest<DeleteEmployeeSalaryCommandResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
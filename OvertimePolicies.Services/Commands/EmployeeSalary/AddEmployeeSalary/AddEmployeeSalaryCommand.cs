using MediatR;

namespace OvertimePolicies.Services.Commands.EmployeeSalary.AddEmployeeSalary
{
    public class AddEmployeeSalaryCommand : IRequest<AddEmployeeSalaryCommandResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Salary { get; set; }
        public int BasicSalary { get; set; }
        public int Allowance { get; set; }
        public int Transportation { get; set; }
        public int OverTime { get; set; }
        public int Tax { get; set; }
    }
}
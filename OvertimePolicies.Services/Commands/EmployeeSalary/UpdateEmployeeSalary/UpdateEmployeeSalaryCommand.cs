using MediatR;

namespace OvertimePolicies.Services.Commands.EmployeeSalary.UpdateEmployeeSalary
{
    public class UpdateEmployeeSalaryCommand : IRequest<UpdateEmployeeSalaryCommandResponse>
    {
        public int EmployeeSalaryId { get; set; }
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
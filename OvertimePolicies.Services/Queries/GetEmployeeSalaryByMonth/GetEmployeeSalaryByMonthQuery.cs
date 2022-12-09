using MediatR;

namespace OvertimePolicies.Services.Queries.GetEmployeeSalaryByMonth
{
    public class GetEmployeeSalaryByMonthQuery : IRequest<GetEmployeeSalaryByMonthQueryResponse>
    {
        public int EmployeeId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
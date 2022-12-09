using MediatR;

namespace OvertimePolicies.Services.Queries.GetEmployeeSalaryByMonthRange
{
    public class GetEmployeeSalaryByMonthRangeQuery : IRequest<GetEmployeeSalaryByMonthRangeQueryResponse>
    {
        public int EmployeeId { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int StartMonth { get; set; }
        public int EndMonth { get; set; }
    }
}
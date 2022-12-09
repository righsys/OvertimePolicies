using MediatR;

namespace OvertimePolicies.Services.Queries.GetEmployeeSalaries
{
    public class GetEmployeeSalariesQuery : IRequest<GetEmployeeSalariesQueryResponse>
    {
        public int EmployeeId { get; set; }
    }
}
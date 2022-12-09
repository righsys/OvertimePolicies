using MediatR;
using OvertimePolicies.Services.Common.Exceptions;
using OvertimePolicies.Services.Interfaces.DapperRepositories;
using OvertimePolicies.Services.Mappers;
using Serilog;

namespace OvertimePolicies.Services.Queries.GetEmployeeList
{
    public class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, GetEmployeeListQueryResponse>
    {
        private readonly IDapperEmployeeRepository _dapperEmployeeRepository;

        public GetEmployeeListQueryHandler(IDapperEmployeeRepository dapperEmployeeRepository)
        {
            _dapperEmployeeRepository = dapperEmployeeRepository;
        }

        public async Task<GetEmployeeListQueryResponse> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            GetEmployeeListQueryResponse response = new GetEmployeeListQueryResponse();
            try
            {
                IQueryable<Domain.Entities.Employee> employees = await _dapperEmployeeRepository.GetAllEmployees();

                response.Employees = employees.ConvertToEmployeeDtos();
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ExceptionMessage = ex.Message;
                response.CustomErrorMessage = ExceptionMessages.GeneralError;

                //
                // Logging
                //
                Log.Error(ex, $"GetEmployeeListQuery");

                return response;
            }
        }
    }
}
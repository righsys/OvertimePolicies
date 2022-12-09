using MediatR;
using OvertimePolicies.Services.Common.Exceptions;
using OvertimePolicies.Services.Interfaces.DapperRepositories;
using OvertimePolicies.Services.Mappers;
using Serilog;

namespace OvertimePolicies.Services.Queries.GetEmployeeSalaryByMonth
{
    public class GetEmployeeSalaryByMonthQueryHandler : IRequestHandler<GetEmployeeSalaryByMonthQuery, GetEmployeeSalaryByMonthQueryResponse>
    {
        private readonly IDapperEmployeeSalaryRepository _dapperEmployeeSalaryRepository;

        public GetEmployeeSalaryByMonthQueryHandler(IDapperEmployeeSalaryRepository dapperEmployeeSalaryRepository)
        {
            _dapperEmployeeSalaryRepository = dapperEmployeeSalaryRepository;
        }

        public async Task<GetEmployeeSalaryByMonthQueryResponse> Handle(GetEmployeeSalaryByMonthQuery request, CancellationToken cancellationToken)
        {
            GetEmployeeSalaryByMonthQueryResponse response = new GetEmployeeSalaryByMonthQueryResponse();
            try
            {
                var employeeSalary = await _dapperEmployeeSalaryRepository.GetEmployeeSalaryByMonth(request.EmployeeId, request.Year, request.Month);
                if (employeeSalary == null) 
                {
                    response.Success = false;
                    response.CustomErrorMessage = ExceptionMessages.EntityNotFoundError;
                    return response;
                }
                response.Success = true;
                response.EmployeeSalary = employeeSalary.ConvertToEmployeeSalaryDto();
                return response;
            }
            catch (Exception ex)
            {
                response.CustomErrorMessage = ExceptionMessages.GeneralError;
                response.ExceptionMessage = ex.Message;
                response.Success = false;

                //
                // Logging
                //
                Log.Error(ex, $"GetEmployeeSalaryByMonthQuery : Emp Id: {request.EmployeeId} Year: {request.Year} Month: {request.Month}");

                return response;
            }
        }
    }
}
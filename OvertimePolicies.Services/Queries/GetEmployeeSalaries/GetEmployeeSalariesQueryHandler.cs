using MediatR;
using Microsoft.EntityFrameworkCore;
using OvertimePolicies.Services.Common.Exceptions;
using OvertimePolicies.Services.Interfaces.DapperRepositories;
using OvertimePolicies.Services.Mappers;
using Serilog;

namespace OvertimePolicies.Services.Queries.GetEmployeeSalaries
{
    public class GetEmployeeSalariesQueryHandler : IRequestHandler<GetEmployeeSalariesQuery, GetEmployeeSalariesQueryResponse>
    {
        private readonly IDapperEmployeeSalaryRepository _dapperEmployeeSalaryRepository;

        public GetEmployeeSalariesQueryHandler(IDapperEmployeeSalaryRepository dapperEmployeeSalaryRepository)
        {
            _dapperEmployeeSalaryRepository = dapperEmployeeSalaryRepository;
        }

        public async Task<GetEmployeeSalariesQueryResponse> Handle(GetEmployeeSalariesQuery request, CancellationToken cancellationToken)
        {
            GetEmployeeSalariesQueryResponse response = new GetEmployeeSalariesQueryResponse();
            try
            {
                var employeeSalaries = await _dapperEmployeeSalaryRepository.GetALlEmployeeSalariesByEmployeeId(request.EmployeeId);
                response.EmployeeSalaries = employeeSalaries.ConvertToEmployeeSalaryDtos();
                response.Success = true;
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
                Log.Error(ex, $"GetEmployeeSalariesQuery : Emp Id: {request.EmployeeId}");

                return response;
            }
        }
    }
}
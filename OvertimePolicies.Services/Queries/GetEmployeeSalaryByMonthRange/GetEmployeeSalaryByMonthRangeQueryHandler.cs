using MediatR;
using Microsoft.EntityFrameworkCore;
using OvertimePolicies.Services.Common.Exceptions;
using OvertimePolicies.Services.Interfaces.DapperRepositories;
using OvertimePolicies.Services.Mappers;
using OvertimePolicies.WebApp.Common.DatetimeHelper;
using Serilog;

namespace OvertimePolicies.Services.Queries.GetEmployeeSalaryByMonthRange
{
    public class GetEmployeeSalaryByMonthRangeQueryHandler : IRequestHandler<GetEmployeeSalaryByMonthRangeQuery,
        GetEmployeeSalaryByMonthRangeQueryResponse>
    {
        private readonly IDapperEmployeeSalaryRepository _dapperEmployeeSalaryRepository;
        private readonly IDateTimeHelper _dateTimeHelper;

        public GetEmployeeSalaryByMonthRangeQueryHandler(IDapperEmployeeSalaryRepository dapperEmployeeSalaryRepository, IDateTimeHelper dateTimeHelper)
        {
            _dapperEmployeeSalaryRepository = dapperEmployeeSalaryRepository;
            _dateTimeHelper = dateTimeHelper;
        }

        public async Task<GetEmployeeSalaryByMonthRangeQueryResponse> Handle(GetEmployeeSalaryByMonthRangeQuery request,
            CancellationToken cancellationToken)
        {
            GetEmployeeSalaryByMonthRangeQueryResponse response = new GetEmployeeSalaryByMonthRangeQueryResponse();
            try
            {
                var employeeSalaries = await _dapperEmployeeSalaryRepository
                    .GetAllEmployeeSalariesByDateRange(request.EmployeeId, request.StartYear, request.EndYear, request.StartMonth, request.EndMonth);

                response.Success = true;
                response.EmployeeSalaries = employeeSalaries.ConvertToEmployeeSalaryDtos();
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
                Log.Error(ex, $"GetEmployeeSalaryByMonthRangeQuery : Emp Id {request.EmployeeId} Start Year : {request.StartYear} End Year: {request.EndYear} Start Month : {request.StartMonth} End Month : {request.EndMonth}");

                return response;
            }
        }
    }
}
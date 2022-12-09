using MediatR;
using Microsoft.Extensions.Logging;
using OvertimePolicies.Services.Commands.Employee.AddEmployee;
using OvertimePolicies.Services.Common.Exceptions;
using OvertimePolicies.Services.DTOs;
using OvertimePolicies.Services.Interfaces;
using OvertimePolicies.Services.Interfaces.DapperRepositories;
using OvertimePolicies.Services.Interfaces.EFCoreRepositories;
using OvertimePolicies.Services.Mappers;
using OvertimePolicies.WebApp.Common.DatetimeHelper;
using Serilog;

namespace OvertimePolicies.Services.Commands.EmployeeSalary.AddEmployeeSalary
{
    public class AddEmployeeSalaryCommandHandler : IRequestHandler<AddEmployeeSalaryCommand, AddEmployeeSalaryCommandResponse>
    {
        private readonly IEFCoreEmployeeSalaryRepository _employeeSalaryRepository;
        private readonly IEFCoreEmployeeRepository _employeeRepository;
        private readonly IDapperEmployeeSalaryRepository _dapperEmployeeSalaryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly ILogger<AddEmployeeSalaryCommand> _logger;

        public AddEmployeeSalaryCommandHandler(IEFCoreEmployeeSalaryRepository employeeSalaryRepository,
            ICurrentUserService currentUserService,
            IDateTimeHelper dateTimeHelper,
            IEFCoreEmployeeRepository employeeRepository,
            IDapperEmployeeSalaryRepository dapperEmployeeSalaryRepository,
            ILogger<AddEmployeeSalaryCommand> logger)
        {
            _employeeSalaryRepository = employeeSalaryRepository;
            _currentUserService = currentUserService;
            _dateTimeHelper = dateTimeHelper;
            _employeeRepository = employeeRepository;
            _dapperEmployeeSalaryRepository = dapperEmployeeSalaryRepository;
            _logger = logger;
        }

        public async Task<AddEmployeeSalaryCommandResponse> Handle(AddEmployeeSalaryCommand request, CancellationToken cancellationToken)
        {
            AddEmployeeSalaryCommandResponse response = new AddEmployeeSalaryCommandResponse();
            try
            {
                Domain.Entities.Employee employee = await _employeeRepository.GetEmployeeByName(request.FirstName, request.LastName);
                if (employee is null)
                {
                    response.Success = false;
                    response.CustomErrorMessage = ExceptionMessages.EmployeeNotExist;
                    return response;
                }
                var salary = await _dapperEmployeeSalaryRepository
                    .GetEmployeeSalaryByMonth(employee.EmployeeId, request.Year, request.Month);

                if (salary is not null)
                {
                    response.Success = false;
                    response.CustomErrorMessage = ExceptionMessages.DuplicateSalaryInsert;
                    return response;
                }

                Domain.Entities.EmployeeSalary Salary = new Domain.Entities.EmployeeSalary()
                {
                    EmployeeId = employee.EmployeeId,
                    Tax = request.Tax,
                    Year = request.Year,
                    Month = request.Month,
                    Allowance = request.Allowance,
                    BasicSalary = request.BasicSalary,
                    Salary = request.Salary,
                    Transportation = request.Transportation,
                    Overtime = request.OverTime,
                    //
                    // Audiable entity
                    //
                    CreatedBy = _currentUserService.Username,
                    CreationTime = _dateTimeHelper.GetLocalDateTime()
                };
                await _employeeSalaryRepository.AddAsync(Salary);

                //
                // Dispatchh assotiated domain event
                //

                response.Success = true;
                response.JustCreatedEmployeeSalary = Salary.ConvertToEmployeeSalaryDto();
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.CustomErrorMessage = ExceptionMessages.GeneralError;
                response.ExceptionMessage = ex.Message;

                //
                // Logging
                //
                _logger.LogError(ex, $"AddEmployeeSalaryCommand : First Name: {request.FirstName} Last Name: {request.LastName}");

                return response;
            }
        }
    }
}
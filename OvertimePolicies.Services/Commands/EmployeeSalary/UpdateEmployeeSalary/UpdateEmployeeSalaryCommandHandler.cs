using MediatR;
using Microsoft.Extensions.Logging;
using OvertimePolicies.Services.Common.Exceptions;
using OvertimePolicies.Services.Interfaces;
using OvertimePolicies.Services.Interfaces.DapperRepositories;
using OvertimePolicies.Services.Interfaces.EFCoreRepositories;
using OvertimePolicies.Services.Mappers;
using OvertimePolicies.WebApp.Common.DatetimeHelper;

namespace OvertimePolicies.Services.Commands.EmployeeSalary.UpdateEmployeeSalary
{
    public class UpdateEmployeeSalaryCommandHandler : IRequestHandler<UpdateEmployeeSalaryCommand, UpdateEmployeeSalaryCommandResponse>
    {
        private readonly IEFCoreEmployeeSalaryRepository _employeeSalaryRepository;
        private readonly IEFCoreEmployeeRepository _employeeRepository;
        private readonly IDapperEmployeeSalaryRepository _dapperEmployeeSalaryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly ILogger<UpdateEmployeeSalaryCommand> _logger;

        public UpdateEmployeeSalaryCommandHandler(IEFCoreEmployeeSalaryRepository employeeSalaryRepository,
            ICurrentUserService currentUserService,
            IDateTimeHelper dateTimeHelper,
            IEFCoreEmployeeRepository employeeRepository,
            IDapperEmployeeSalaryRepository dapperEmployeeSalaryRepository,
            ILogger<UpdateEmployeeSalaryCommand> logger)
        {
            _employeeSalaryRepository = employeeSalaryRepository;
            _currentUserService = currentUserService;
            _dateTimeHelper = dateTimeHelper;
            _employeeRepository = employeeRepository;
            _dapperEmployeeSalaryRepository = dapperEmployeeSalaryRepository;
            _logger = logger;
        }

        public async Task<UpdateEmployeeSalaryCommandResponse> Handle(UpdateEmployeeSalaryCommand request, CancellationToken cancellationToken)
        {
            UpdateEmployeeSalaryCommandResponse response = new UpdateEmployeeSalaryCommandResponse();
            try
            {
                Domain.Entities.Employee employee = await _employeeRepository.GetEmployeeByName(request.FirstName, request.LastName);
                if (employee == null)
                {
                    response.Success = false;
                    response.CustomErrorMessage = ExceptionMessages.EmployeeNotExist;
                    return response;
                }
                var salary = await _dapperEmployeeSalaryRepository
                    .GetEmployeeSalaryByMonth(employee.EmployeeId, request.Year, request.Month);

                if (salary is null)
                {
                    response.Success = false;
                    response.CustomErrorMessage = ExceptionMessages.SalaryNotExistToUpdate;
                    return response;
                }

                Domain.Entities.EmployeeSalary salaryForUpdate = salary.ConvertToEmployeeSalary();
                salaryForUpdate.Year = request.Year;
                salaryForUpdate.Month = request.Month;
                salaryForUpdate.Salary = request.Salary;
                salaryForUpdate.BasicSalary = request.BasicSalary;
                salaryForUpdate.Allowance = request.Allowance;
                salaryForUpdate.Transportation = request.Transportation;
                salaryForUpdate.Overtime = request.OverTime;
                //
                // Change tracking
                //
                salaryForUpdate.LastModificationTime = _dateTimeHelper.GetLocalDateTime();
                salaryForUpdate.LastModifiedBy = _currentUserService.Username;

                //
                await _employeeSalaryRepository.UpdateAsync(salaryForUpdate);
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.CustomErrorMessage = ExceptionMessages.UpdateEntityError;
                response.ExceptionMessage = ex.Message;
                //
                // Logging
                //
                _logger.LogError(ex, $"UpdateEmployeeSalaryCommand : First Name: {request.FirstName} Last Name: {request.LastName} Year: {request.Year} Month : {request.Month}");

                return response;
            }
            throw new NotImplementedException();
        }
    }
}
using MediatR;
using Microsoft.Extensions.Logging;
using OvertimePolicies.Services.Common.Exceptions;
using OvertimePolicies.Services.DTOs;
using OvertimePolicies.Services.Interfaces.DapperRepositories;
using OvertimePolicies.Services.Interfaces.EFCoreRepositories;
using OvertimePolicies.Services.Mappers;

namespace OvertimePolicies.Services.Commands.EmployeeSalary.DeleteEmployeeSalary
{
    public class DeleteEmployeeSalaryCommandHandler : IRequestHandler<DeleteEmployeeSalaryCommand, DeleteEmployeeSalaryCommandResponse>
    {
        private readonly IEFCoreEmployeeSalaryRepository _employeeSalaryRepository;
        private readonly IEFCoreEmployeeRepository _employeeRepository;
        private readonly IDapperEmployeeSalaryRepository _dapperEmployeeSalaryRepository;
        private readonly ILogger<DeleteEmployeeSalaryCommand> _logger;

        public DeleteEmployeeSalaryCommandHandler(IEFCoreEmployeeSalaryRepository employeeSalaryRepository,
            IEFCoreEmployeeRepository employeeRepository, 
            IDapperEmployeeSalaryRepository dapperEmployeeSalaryRepository,
            ILogger<DeleteEmployeeSalaryCommand> logger)
        {
            _employeeSalaryRepository = employeeSalaryRepository;
            _employeeRepository = employeeRepository;
            _dapperEmployeeSalaryRepository = dapperEmployeeSalaryRepository;
            _logger = logger;
        }

        public async Task<DeleteEmployeeSalaryCommandResponse> Handle(DeleteEmployeeSalaryCommand request, CancellationToken cancellationToken)
        {
            DeleteEmployeeSalaryCommandResponse response = new DeleteEmployeeSalaryCommandResponse();
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

                //salary.Id = salary.EmployeeSalaryId;
                await _employeeSalaryRepository.DeleteEmployeeSalary(salary.EmployeeSalaryId);
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.CustomErrorMessage = ExceptionMessages.DeleteEntityError;
                response.ExceptionMessage = ex.Message;
                //
                // Logging
                //
                _logger.LogError(ex, $"DeleteEmployeeSalaryCommand : First Name: {request.FirstName} Last Name: {request.LastName}");

                return response;
            }
        }
    }
}
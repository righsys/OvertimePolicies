using MediatR;
using OvertimePolicies.Services.Common.Exceptions;
using OvertimePolicies.Services.Interfaces.EFCoreRepositories;
using OvertimePolicies.Services.Interfaces;
using OvertimePolicies.Services.Mappers;
using OvertimePolicies.WebApp.Common.DatetimeHelper;
using OvertimePolicies.Domain.Events;
using Microsoft.Extensions.Logging;

namespace OvertimePolicies.Services.Commands.Employee.AddEmployee
{
    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, AddEmployeeCommandResponse>
    {
        private readonly IEFCoreEmployeeRepository _employeeRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly ILogger<AddEmployeeCommand> _logger;

        public AddEmployeeCommandHandler(IEFCoreEmployeeRepository employeeRepository,
            ICurrentUserService currentUserService,
            IDateTimeHelper dateTimeHelper,
            ILogger<AddEmployeeCommand> logger)
        {
            _employeeRepository = employeeRepository;
            _currentUserService = currentUserService;
            _dateTimeHelper = dateTimeHelper;
            _logger = logger;
        }

        public async Task<AddEmployeeCommandResponse> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            AddEmployeeCommandResponse response = new AddEmployeeCommandResponse();
            try
            {
                Domain.Entities.Employee employee = new Domain.Entities.Employee()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    EmploymentDate = request.EmploymentDate,
                    //
                    // Audiable entity
                    //
                    CreatedBy = _currentUserService.Username,
                    CreationTime = _dateTimeHelper.GetLocalDateTime()
                };

                EmployeeAddedEvent newEmployeeAddedEvent = new EmployeeAddedEvent(employee);
                employee.RegisterDomainEvent(newEmployeeAddedEvent);

                await _employeeRepository.AddAsync(employee);

                response.Success = true;
                response.Employee = employee.ConvertToEmployeeDto();


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
                _logger.LogError(ex, $"AddEmployeeCommand : {request.FirstName} {request.LastName}");

                return response;
            }
        }
    }
}
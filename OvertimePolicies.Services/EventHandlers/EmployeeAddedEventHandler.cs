using MediatR;
using Microsoft.Extensions.Logging;
using OvertimePolicies.Domain.Events;
using OvertimePolicies.WebApp.Common.Email;

namespace OvertimePolicies.Services.EventHandlers
{
    public class EmployeeAddedEventHandler : INotificationHandler<EmployeeAddedEvent>
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<EmployeeAddedEvent> _logger;

        public EmployeeAddedEventHandler(IEmailService emailService, ILogger<EmployeeAddedEvent> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        public Task Handle(EmployeeAddedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{notification.Employee.FirstName} {notification.Employee.LastName} was created.");
            return  _emailService.SendMailAsync("test@test.com", "test@test.com", $"{notification.Employee.FirstName} {notification.Employee.LastName} was created.", notification.Employee.ToString());
        }
    }
}
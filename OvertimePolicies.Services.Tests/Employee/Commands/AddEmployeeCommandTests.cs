using OvertimePolicies.Services.Tests.Common;
using Xunit;
using Moq;
using MediatR;
using OvertimePolicies.Services.Commands.Employee.AddEmployee;
using System.Threading;
using OvertimePolicies.Services.Interfaces.EFCoreRepositories;
using OvertimePolicies.Services.Interfaces;
using OvertimePolicies.WebApp.Common.DatetimeHelper;
using Microsoft.Extensions.Logging;

namespace OvertimePolicies.Services.Tests.Employee.Commands
{
    public class AddEmployeeCommandTests:CommandTestBase<AddEmployeeCommand>
    {
        public AddEmployeeCommandTests(IEFCoreEmployeeRepository employeeRepository, 
            ICurrentUserService currentUserService, 
            IDateTimeHelper dateTimeHelper, ILogger<AddEmployeeCommand> logger) : base(employeeRepository, currentUserService, dateTimeHelper,logger)
        {

        }

        [Fact]
        public void Handle_GivenValidRequest_ShouldRaiseAddEmployeeNotification() 
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sut = new AddEmployeeCommandHandler(_employeeRepository, _currentUserService, _dateTimeHelper,_logger);

            // Act

            // Assert
        }
    }
}
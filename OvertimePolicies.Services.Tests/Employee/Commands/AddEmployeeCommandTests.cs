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
using OvertimePolicies.Domain.Events;
using System.Diagnostics.SymbolStore;

namespace OvertimePolicies.Services.Tests.Employee.Commands
{
    public class AddEmployeeCommandTests : CommandTestBase
    {
        public AddEmployeeCommandTests() { }

        [Fact]
        public void Handle_GivenValidRequest_ShouldRaiseAddEmployeeNotification()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var currentUserServoceMock = new Mock<ICurrentUserService>();
            var employeeRepositoryMock = new Mock<IEFCoreEmployeeRepository>();
            var dateTimeHelperMock = new Mock<IDateTimeHelper>();
            var iLoggerMock = new Mock<ILogger<AddEmployeeCommand>>();

            var sut = new AddEmployeeCommandHandler(employeeRepositoryMock.Object,
                    currentUserServoceMock.Object,
                    dateTimeHelperMock.Object,
                    iLoggerMock.Object);

            var employee = new Domain.Entities.Employee
            {
                EmployeeId = 1234,
                FirstName = "Morteza",
                LastName = "Hasani",
                EmploymentDate = DateTime.Now
            };
            var command = new AddEmployeeCommand()
            {
                FirstName = "Morteza",
                LastName = "Hasani",
                EmploymentDate = DateTime.Now
            };

            // Act            
            var result = sut.Handle(command, CancellationToken.None);

            // Assert        
            mediatorMock.Setup(m => m.Publish(It.Is<EmployeeAddedEvent>(x => x.Employee == employee), It.IsAny<CancellationToken>()))
                        .Callback<EmployeeAddedEvent, CancellationToken>(async (notification, cToken) => await sut.Handle(command, cToken));
            mediatorMock.Verify();
        }
    }
}
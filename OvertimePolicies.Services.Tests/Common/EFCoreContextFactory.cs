using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using OvertimePolicies.Domain.Entities;
using OvertimePolicies.Infrastructure.DbContexts;
using OvertimePolicies.Services.Commands.Employee.AddEmployee;
using OvertimePolicies.Services.Interfaces;
using OvertimePolicies.Services.Interfaces.EFCoreRepositories;
using OvertimePolicies.SharedKernel.Interfaces;
using OvertimePolicies.WebApp.Common.DatetimeHelper;

namespace OvertimePolicies.Services.Tests.Common
{
    public class EFCoreContextFactory
    {
        //private static IDateTimeHelper _dateTimeHelper;
        //private static IDomainEventDispatcher _domainEventDispatcher;
        //private static ICurrentUserService _currentUserService;

        //public EFCoreContextFactory(ICurrentUserService currentUserService, 
        //    IDomainEventDispatcher domainEventDispatcher, 
        //    IDateTimeHelper dateTimeHelper)
        //{
        //    _currentUserService = currentUserService;
        //    _domainEventDispatcher = domainEventDispatcher;
        //    _dateTimeHelper = dateTimeHelper;
        //}

        public static EFCoreDbContext CreateEFDbContext()
        {
            var currentUserServiceMock = new Mock<ICurrentUserService>();
            var domainEventDispatcherMock = new Mock<IDomainEventDispatcher>();
            var dateTimeHelperMock = new Mock<IDateTimeHelper>();
            var iLoggerMock = new Mock<ILogger<AddEmployeeCommand>>();

            var options=new DbContextOptionsBuilder<EFCoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new EFCoreDbContext(options, dateTimeHelperMock.Object, currentUserServiceMock.Object, domainEventDispatcherMock.Object);
            context.Database.EnsureCreated();

            Domain.Entities.Employee employee = new Domain.Entities.Employee
            {
                FirstName = "moreza",
                LastName = "hasani",
                EmploymentDate = dateTimeHelperMock.Object.GetLocalDateTime(),
                CreatedBy = currentUserServiceMock.Object.Username,
                CreationTime = dateTimeHelperMock.Object.GetLocalDateTime()
            };
            context.Employees.Add(employee);
            context.SaveChanges();

            context.EmployeeSalaries.Add(new EmployeeSalary() {
                Allowance = 1000,
                BasicSalary = 1000,
                CreatedBy = currentUserServiceMock.Object.Username,
                CreationTime = dateTimeHelperMock.Object.GetLocalDateTime(),
                Month = 1,
                Year = 1401,
                Overtime = 0,
                Salary = 0,
                Tax = 0,
                Transportation = 1000,
                EmployeeId=employee.EmployeeId,
            });
            context.SaveChanges();

            return context;
        }

        public static void Destroy(EFCoreDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
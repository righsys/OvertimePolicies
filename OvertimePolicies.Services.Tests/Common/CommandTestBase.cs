using OvertimePolicies.Infrastructure.DbContexts;
using OvertimePolicies.Services.Interfaces.EFCoreRepositories;
using OvertimePolicies.Services.Interfaces;
using OvertimePolicies.WebApp.Common.DatetimeHelper;
using Microsoft.Extensions.Logging;

namespace OvertimePolicies.Services.Tests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly EFCoreDbContext _context;
        //protected readonly IEFCoreEmployeeRepository _employeeRepository;
        //protected readonly ICurrentUserService _currentUserService;
        //protected readonly IDateTimeHelper _dateTimeHelper;
        //protected readonly ILogger<TCommand> _logger;

        public CommandTestBase()
        {
            _context = EFCoreContextFactory.CreateEFDbContext();
            //_employeeRepository = employeeRepository;
            //_currentUserService = currentUserService;
            //_dateTimeHelper = dateTimeHelper;
            //_logger = logger;
        }

        public void Dispose()
        {
            EFCoreContextFactory.Destroy(_context);
        }
    }
}
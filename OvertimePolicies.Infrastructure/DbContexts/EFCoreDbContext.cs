using Microsoft.EntityFrameworkCore;
using OvertimePolicies.Domain.Entities;
using OvertimePolicies.Services.Interfaces;
using OvertimePolicies.SharedKernel;
using OvertimePolicies.SharedKernel.Interfaces;
using OvertimePolicies.WebApp.Common.DatetimeHelper;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace OvertimePolicies.Infrastructure.DbContexts
{
    public class EFCoreDbContext : DbContext, IEFCoreDbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeHelper _dateTimeHelper;

        public EFCoreDbContext(DbContextOptions<EFCoreDbContext> options,
            IDateTimeHelper dateTimeHelper,
            ICurrentUserService currentUserService,
            IDomainEventDispatcher dispatcher) : base(options)
        {
            _dateTimeHelper = dateTimeHelper;
            _currentUserService = currentUserService;
            _dispatcher = dispatcher;            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFCoreDbContext).Assembly);

            modelBuilder.Entity<Employee>()
                //.HasQueryFilter(x => x.IsDeleted == false)
                .ToTable("Employee")
                .HasKey(x => x.EmployeeId);
            modelBuilder.Entity<EmployeeSalary>()
                //.HasQueryFilter(x => x.IsDeleted == false)
                .ToTable("EmployeeSalary")
                .HasKey(x => x.EmployeeSalaryId);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {            
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (_dispatcher == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<EntityBase<int>>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToArray();

            await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);
            return result;
        }
        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
    }
}
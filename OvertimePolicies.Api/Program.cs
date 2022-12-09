using Microsoft.EntityFrameworkCore;
using OvertimePolicies.Api.Services;
using OvertimePolicies.Infrastructure.DbContexts;
using OvertimePolicies.Infrastructure.Repositories.DapperRepositories;
using OvertimePolicies.Infrastructure.Repositories.EFCoreRepositories;
using OvertimePolicies.Services;
using OvertimePolicies.Services.Implementations;
using OvertimePolicies.Services.Interfaces;
using OvertimePolicies.Services.Interfaces.DapperRepositories;
using OvertimePolicies.Services.Interfaces.EFCoreRepositories;
using OvertimePolicies.SharedKernel;
using OvertimePolicies.SharedKernel.Interfaces;
using OvertimePolicies.WebApp.Common.DatetimeHelper;
using OvertimePolicies.WebApp.Common.Email;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//
// Using actual database
//
builder.Services.AddDbContext<EFCoreDbContext>(
    options => options
        .UseSqlServer(builder.Configuration.GetConnectionString("OvertimePoliciesDBConnection"))
        .EnableDetailedErrors());
//
//
//
builder.Services.AddScoped<IEFCoreDbContext, EFCoreDbContext>();
builder.Services.AddScoped<DapperDbContext>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IDateTimeHelper, DateTimeHelper>();
builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
builder.Services.AddTransient<IEmailService, EmailService>();
//
// Services
//
builder.Services.AddScoped<IDapperEmployeeRepository, DapperEmployeeRepository>();
builder.Services.AddScoped<IDapperEmployeeSalaryRepository, DapperEmployeeSalaryRepository>();
builder.Services.AddScoped<IEFCoreEmployeeRepository, EFCoreEmployeeRepository>();
builder.Services.AddScoped<IEFCoreEmployeeSalaryRepository, EFCoreEmployeeSalaryRepository>();
builder.Services.AddScoped<IOvertimeCalculatorMethods, OvertimeCalculatorMethods>();
builder.Services.AddApplication();
//
// Add serilog
//
builder.Services.AddSingleton(Log.Logger);
var _logger = new LoggerConfiguration()
    .WriteTo
    .File("D:\\LogOvertimeCalculator\\Logs\\alllogs-.log", rollingInterval: RollingInterval.Day)
    .WriteTo
    .File("D:\\LogOvertimeCalculator\\Logs\\warninganduplogs-.log", rollingInterval: RollingInterval.Day,restrictedToMinimumLevel:Serilog.Events.LogEventLevel.Warning)    
    .CreateLogger();
builder.Logging.AddSerilog(_logger);
//
// Other
//
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

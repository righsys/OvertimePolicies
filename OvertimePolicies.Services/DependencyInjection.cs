using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OvertimePolicies.Services.Common.Behaviours;
using System.Reflection;
using Serilog;
using Microsoft.AspNetCore.Http;

namespace OvertimePolicies.Services
{
    public static class DependencyInjection
    {        
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));            
            return services;
        }
    }
}
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using OvertimePolicies.Services.Interfaces;
using System.Reflection;

namespace OvertimePolicies.Services.Common.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly ICurrentUserService _currentUserService;
        public RequestLogger(ICurrentUserService currentUserService, ILogger<TRequest> logger)
        {
            _currentUserService = currentUserService;
            _logger = logger;
        }
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;
            _logger.LogInformation("OvertimePolicies Request: Username: {Name} {@UserId} {@Request}", name, _currentUserService.Username, request);           
            return Task.CompletedTask;
        }
    }
}
using MediatR;
using Microsoft.Extensions.Logging;
using OvertimePolicies.Services.Interfaces;
using Serilog;
using System.Diagnostics;

namespace OvertimePolicies.Services.Common.Behaviours
{
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        private readonly ICurrentUserService _currentUserService;

        public RequestPerformanceBehaviour(ICurrentUserService currentUserService, ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 500)
            {
                var name = typeof(TRequest).Name;
                _logger.LogWarning("OvertimePolicies Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}",
                    name, _timer.ElapsedMilliseconds, _currentUserService.Username, request);
            }
            return response;
        }
    }
}
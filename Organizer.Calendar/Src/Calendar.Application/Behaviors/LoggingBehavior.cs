using MediatR;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Calendar.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        #region 

        private readonly ILogger _logger;

        #endregion

        #region 

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        #endregion

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // track request execution time
            var startTime = Stopwatch.GetTimestamp();

            var response = await next();

            var elapsed = Stopwatch.GetElapsedTime(startTime);
            var ms = elapsed.Milliseconds;

            if (ms > 1000)
            {
                _logger.LogWarning("Executed Request: {requestName} ({requestDuration}ms)", typeof(TRequest).Name, ms);
            }
            else
            {
                _logger.LogInformation("Executed Request: {requestName} ({requestDuration}ms)", typeof(TRequest).Name, ms);
            }

            return response;
        }
    }
}
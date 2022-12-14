using MediatR;
using System.Diagnostics;

namespace Calendar.Application.Behavior
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // track request execution time
            var startTime = Stopwatch.GetTimestamp();

            var response = await next();

            var elapsed = Stopwatch.GetElapsedTime(startTime);
            var ms = elapsed.Milliseconds;

            return response;
        }
    }
}
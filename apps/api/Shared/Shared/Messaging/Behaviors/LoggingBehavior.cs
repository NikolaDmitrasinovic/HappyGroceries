using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Shared.Messaging.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger = logger;
    private const long SlowRequestThresholdMiliseconds = 300;

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var requestName = typeof(TRequest).Name;
        var stopwatch = Stopwatch.StartNew();

        using (_logger.BeginScope("Request {RequestName}", requestName))
        {
            _logger.LogInformation("Handling request");

            try
            {
                var response = await next();

                stopwatch.Stop();

                if (stopwatch.ElapsedMilliseconds > SlowRequestThresholdMiliseconds)
                    _logger.LogWarning("Handled slow request {RequestName} in {ElapsedMilliseconds} ms", requestName, stopwatch.ElapsedMilliseconds);
                else
                    _logger.LogInformation("Handled request in {ElapsedMilliseconds} ms", stopwatch.ElapsedMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                _logger.LogError(ex, "Request failed after {ElapsedMilliseconds} ms", stopwatch.ElapsedMilliseconds);

                throw;
            }
        }
    }
}

using Microsoft.Extensions.Logging;
using Shared.Messaging;
using System.Diagnostics;

namespace Shared.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger = logger;

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var requestName = typeof(TRequest).Name;
        var stopwatch = Stopwatch.StartNew();

        using (_logger.BeginScope("Request {RwquestName}", requestName))
        {
            try
            {
                var response = await next();

                stopwatch.Stop();

                _logger.LogInformation("Handled request in {ElapsedMilliseconds} ms", stopwatch.ElapsedMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                _logger.LogError(ex, "Request failed after {ElapseMilliseconds} ms", stopwatch.ElapsedMilliseconds);

                throw;
            }
        }        
    }
}

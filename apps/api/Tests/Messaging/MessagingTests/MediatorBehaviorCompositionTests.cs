using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Messaging;
using Shared.Messaging.Behaviors;
using Shared.Validation;

namespace MessagingTests;

public class MediatorBehaviorCompositionTests
{
    [Fact]
    public async Task Send_WithLoggingAndValidation_WhenValidationFails_LogsFailure_AndDoesNotInvokeHandler()
    {
        // Arrange
        TestRequestHandler.Reset();

        var services = new ServiceCollection();

        var logger = new TestLogger<LoggingBehavior<TestRequest, string>>();

        services.AddScoped<IMediator, Mediator>();

        services.AddSingleton<ILogger<LoggingBehavior<TestRequest, string>>>(logger);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<IRequestHandler<TestRequest, string>, TestRequestHandler>();
        services.AddScoped<IRequestValidator<TestRequest>, InvalidTestRequestValidator>();

        using var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        // Act
        var exception = await Assert.ThrowsAsync<RequestValidationException>(
            () => mediator.Send(new TestRequest(string.Empty)));

        // Assert
        Assert.Single(exception.Errors);
        Assert.Equal(0, TestRequestHandler.HandleCallCount);

        var errorLog = Assert.Single(logger.Entries, e => e.Level == LogLevel.Error);
        Assert.NotNull(errorLog.Exception);
        Assert.IsType<RequestValidationException>(errorLog.Exception);
        Assert.Contains("Request failed after", errorLog.Message);
    }

    public sealed record TestRequest(string Name) : IRequest<string>;

    private sealed class TestRequestHandler : IRequestHandler<TestRequest, string>
    {
        public static int HandleCallCount { get; private set; }

        public static void Reset()
        {
            HandleCallCount = 0;
        }

        public Task<string> Handle(TestRequest request, CancellationToken cancellationToken)
        {
            HandleCallCount++;
            return Task.FromResult("ok");
        }
    }

    private sealed class InvalidTestRequestValidator : IRequestValidator<TestRequest>
    {
        public IReadOnlyCollection<ValidationFailure> Validate(TestRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return [
                    new ValidationFailure(nameof(TestRequest.Name), "Name is required.")
                    ];

            return [];
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Shared.Messaging;

namespace MessagingTests;

public class MediatorPipelineBehaviorTests
{
    [Fact]
    public async Task Send_WithoutBehaviors_Invokes_Handler()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddScoped<IMediator, Mediator>();
        services.AddScoped<IRequestHandler<TestRequest, string>, TestRequestHandler>();

        using var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        // Act
        var result = await mediator.Send(new TestRequest());

        // Assert
        Assert.Equal("handler-response", result);
        Assert.Equal(1, TestRequestHandler.HandleCallCount);

        TestExecutionLog.Clear();
        TestRequestHandler.Reset();
    }

    [Fact]
    public async Task Send_WithMultipleBehaviors_Executes_in_Registration_Order()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddScoped<IMediator, Mediator>();
        services.AddScoped<IRequestHandler<TestRequest, string>, TestRequestHandler>();

        services.AddScoped<IPipelineBehavior<TestRequest, string>>(
            _ => new RecordingBehavior<TestRequest, string>("Behavior1"));

        services.AddScoped<IPipelineBehavior<TestRequest, string>>(
            _ => new RecordingBehavior<TestRequest, string>("Behavior2"));

        using var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        // Act
        var result = await mediator.Send(new TestRequest());

        // Assert
        Assert.Equal("handler-response", result);

        Assert.Equal(
            [
                "Behavior1 before",
                "Behavior2 before",
                "handler",
                "Behavior2 after",
                "Behavior1 after"
            ],
            TestExecutionLog.Entries);

        TestExecutionLog.Clear();
        TestRequestHandler.Reset();
    }

    [Fact]
    public async Task Send_WhenBehaviorSortCircuts_DoesNotInvoke_Handler()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddScoped<IMediator, Mediator>();
        services.AddScoped<IRequestHandler<TestRequest, string>, TestRequestHandler>();
        services.AddScoped<IPipelineBehavior<TestRequest, string>, ShortCircuitBehavior>();

        using var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        // Act
        var result = await mediator.Send(new TestRequest());

        // Assert
        Assert.Equal("short-circuited", result);
        Assert.Equal(0, TestRequestHandler.HandleCallCount);

        TestExecutionLog.Clear();
        TestRequestHandler.Reset();
    }

    private sealed record TestRequest : IRequest<string>;
    private sealed class TestRequestHandler : IRequestHandler<TestRequest, string>
    {
        public static int HandleCallCount { get; private set; }

        public static void Reset()
        {
            HandleCallCount = 0;
            TestExecutionLog.Clear();
        }

        public Task<string> Handle(TestRequest request, CancellationToken cancellationToken)
        {
            HandleCallCount++;
            TestExecutionLog.Add("handler");

            return Task.FromResult("handler-response");
        }
    }

    private static class TestExecutionLog
    {
        private static readonly List<string> _entries = [];

        internal static IReadOnlyList<string> Entries => _entries;

        internal static void Add(string entry) => _entries.Add(entry);

        internal static void Clear() => _entries.Clear();
    }

    private class RecordingBehavior<TRequest, TResponse>(string name) :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly string _name = name;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TestExecutionLog.Add($"{_name} before");

            var response = await next();

            TestExecutionLog.Add($"{_name} after");

            return response;
        }
    }

    private class ShortCircuitBehavior : IPipelineBehavior<TestRequest, string>
    {
        public Task<string> Handle(TestRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<string> next)
        {
            return Task.FromResult("short-circuited");
        }
    }
}

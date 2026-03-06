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
}

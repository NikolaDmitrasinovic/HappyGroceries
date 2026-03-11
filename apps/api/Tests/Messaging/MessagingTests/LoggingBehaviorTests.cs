using Microsoft.Extensions.Logging;
using Shared.Messaging;
using Shared.Messaging.Behaviors;

namespace MessagingTests;

public class LoggingBehaviorTests
{
    [Fact]
    public async Task Handle_CallsNext_AndReturnsResponse() // Happy Path
    {
        // Arrange
        var logger = new TestLogger<LoggingBehavior<TestRequest, string>>();
        var behavior = new LoggingBehavior<TestRequest, string>(logger);

        var nextCalled = false;

        Task<string> Next()
        {
            nextCalled = true;
            return Task.FromResult("ok");
        }

        // Act
        var result = await behavior.Handle(new TestRequest(), CancellationToken.None, Next);

        // Assert
        Assert.True(nextCalled);
        Assert.Equal("ok", result);
    }

    [Fact]
    public async Task Handle_LogsInformation_OnSuccess()
    {
        // Arrange
        var logger = new TestLogger<LoggingBehavior<TestRequest, string>>();
        var behavior = new LoggingBehavior<TestRequest, string>(logger);

        Task<string> Next() => Task.FromResult("ok");

        // Act
        var result = await behavior.Handle(new TestRequest(), CancellationToken.None, Next);

        // Assert
        Assert.Equal("ok", result);
        Assert.Contains(logger.Entries, e => e.Level == LogLevel.Information && e.Message.Contains("Handling request"));
        Assert.Contains(logger.Entries, e => e.Level == LogLevel.Information && e.Message.Contains("Handled request in"));
    }

    [Fact]
    public async Task Handle_LogsError_AndRethrows_OnFailure()
    {
        // Arrange
        var logger = new TestLogger<LoggingBehavior<TestRequest, string>>();
        var behavior = new LoggingBehavior<TestRequest, string>(logger);

        Task<string> Next() => throw new InvalidOperationException("boom");

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => behavior.Handle(new TestRequest(), CancellationToken.None, Next));

        // Assert
        Assert.Equal("boom", exception.Message);

        var errorLog = Assert.Single(logger.Entries, e => e.Level == LogLevel.Error);
        Assert.Equal("boom", errorLog.Exception?.Message);
        Assert.Contains("Request failed after", errorLog.Message);
    }

    private sealed record TestRequest : IRequest<string>;
}

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

    private sealed record TestRequest : IRequest<string>;
}

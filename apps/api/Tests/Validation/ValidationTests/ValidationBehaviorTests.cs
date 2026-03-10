using Shared.Messaging;
using Shared.Validation;

namespace ValidationTests;

public class ValidationBehaviorTests
{
    [Fact]
    public async Task Handle_WhenNoValidatorsExist_CallsNext()
    {
        // Arrange
        var validators = Enumerable.Empty<IRequestValidator<TestRequest>>();
        var behavior = new ValidationBehavior<TestRequest, string>(validators);

        var nextCalled = false;

        Task<string> Next()
        {
            nextCalled = true;
            return Task.FromResult("ok");
        }

        // Act
        var result = await behavior.Handle(new TestRequest("milk", 2), CancellationToken.None, Next);

        // Assert
        Assert.True(nextCalled);
        Assert.Equal("ok", result);
    }

    [Fact]
    public async Task Handle_WhenValidatorsReturnNoFailures_CallsNext()
    {
        // Arrange
        var validators = new IRequestValidator<TestRequest>[]
        {
            new ValidTestRequestValidator()
        };

        var behavior = new ValidationBehavior<TestRequest, string>(validators);

        var nextCalled = false;

        Task<string> Next()
        {
            nextCalled = true;
            return Task.FromResult("ok");
        }

        // Act
        var result = await behavior.Handle(new TestRequest("milk", 2), CancellationToken.None, Next);

        // Assert
        Assert.True(nextCalled);
        Assert.Equal("ok", result);
    }
        
    private sealed record TestRequest(string Name, int Quantity) : IRequest<string>;

    private class ValidTestRequestValidator : IRequestValidator<TestRequest>
    {
        public IReadOnlyCollection<ValidationFailure> Validate(TestRequest request)
        {
            return [];
        }
    }
}

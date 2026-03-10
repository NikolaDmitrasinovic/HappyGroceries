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

    [Fact]
    public async Task Handle_WhenValidatonFails_ThrowsRequestValidationException()
    {
        // Arrange
        var validators = new IRequestValidator<TestRequest>[]
        {
            new InvalidTestRequestValidator()
        };

        var behavior = new ValidationBehavior<TestRequest, string>(validators);

        var nextCalled = false;

        Task<string> Next()
        {
            nextCalled = true;
            return Task.FromResult("ok");
        }

        // Act
        var exception = await Assert.ThrowsAsync<RequestValidationException>(
            () => behavior.Handle(new TestRequest("", 0), CancellationToken.None, Next));

        // Assert
        Assert.False(nextCalled);
        Assert.Single(exception.Errors);
        Assert.Equal(nameof(TestRequest.Name), exception.Errors.First().Property);
    }
        
    private sealed record TestRequest(string Name, int Quantity) : IRequest<string>;

    private class ValidTestRequestValidator : IRequestValidator<TestRequest>
    {
        public IReadOnlyCollection<ValidationFailure> Validate(TestRequest request)
        {
            return [];
        }
    }

    private class InvalidTestRequestValidator : IRequestValidator<TestRequest>
    {
        public IReadOnlyCollection<ValidationFailure> Validate(TestRequest request)
        {
            return [
                new ValidationFailure(nameof(TestRequest.Name), "Name is required.")
                ];
        }
    }
}

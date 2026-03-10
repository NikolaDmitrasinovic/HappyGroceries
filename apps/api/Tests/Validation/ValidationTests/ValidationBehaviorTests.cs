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

    [Fact]
    public async Task Handle_WhenMultipleValidatorsFail_AggregatesFailures()
    {
        // Arrange
        var validators = new IRequestValidator<TestRequest>[]
        {
            new NameRequiredValidator(),
            new QuantityGreaterThanZeroValidator()
        };

        var behavior = new ValidationBehavior<TestRequest, string>(validators);

        Task<string> Next() => throw new Exception("Next should not be called");

        // Act
        var exception = await Assert.ThrowsAsync<RequestValidationException>(
            () => behavior.Handle(new TestRequest("", 0), CancellationToken.None, Next));

        // Assert
        Assert.Equal(2, exception.Errors.Count);
        Assert.Contains(exception.Errors, e => e.Property == nameof(TestRequest.Name));
        Assert.Contains(exception.Errors, e => e.Property == nameof(TestRequest.Quantity));
    }

    private sealed record TestRequest(string Name, int Quantity) : IRequest<string>;

    private sealed class ValidTestRequestValidator : IRequestValidator<TestRequest>
    {
        public IReadOnlyCollection<ValidationFailure> Validate(TestRequest request)
        {
            return [];
        }
    }

    private sealed class InvalidTestRequestValidator : IRequestValidator<TestRequest>
    {
        public IReadOnlyCollection<ValidationFailure> Validate(TestRequest request)
        {
            return [
                new ValidationFailure(nameof(TestRequest.Name), "Name is required.")
                ];
        }
    }

    private sealed class NameRequiredValidator : IRequestValidator<TestRequest>
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

    private sealed class QuantityGreaterThanZeroValidator : IRequestValidator<TestRequest>
    {
        public IReadOnlyCollection<ValidationFailure> Validate(TestRequest request)
        {
            if (request.Quantity <= 0)
                return [
                    new ValidationFailure(nameof(TestRequest.Quantity), "Quantity must be greater than 0.")
                    ];

            return [];
        }
    }
}

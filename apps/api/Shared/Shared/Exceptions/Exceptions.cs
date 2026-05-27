namespace Shared.Exceptions;

public class NotFoundException(string message) : Exception(message);

public class InsufficientResourcesException(string message) : Exception(message);

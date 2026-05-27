namespace Shared.Exceptions;

public class NotFoundException(string message) : Exception(message);

public class InsufficientResourceException(string message) : Exception(message);

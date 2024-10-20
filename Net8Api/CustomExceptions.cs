namespace Net8Api;

public class NotFoundException(string message) : Exception(message);

public class UnauthorizedException(string message) : Exception(message);
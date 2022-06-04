namespace Gracker.ServiceShell.Exceptions;

public class InvalidMessagingConfigurationException : Exception
{
    const string DEFAULT_MESSAGE = "Messaging configuration was either not set or is invalid.";

    public InvalidMessagingConfigurationException(string message) : base(message) { }

    public InvalidMessagingConfigurationException(string message, Exception innerException) 
        : base(message, innerException) { }

    public InvalidMessagingConfigurationException( Exception innerException) 
        : base(DEFAULT_MESSAGE, innerException) { }

    public InvalidMessagingConfigurationException() : this(DEFAULT_MESSAGE) { }
}

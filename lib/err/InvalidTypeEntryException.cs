using Web_Framework.logger;

namespace Web_Framework.lib.err;

public class InvalidTypeEntryException : Exception
{
    public InvalidTypeEntryException(string message) : base(message) {}
}
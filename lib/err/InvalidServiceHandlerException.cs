using Web_Framework.http;
using Web_Framework.logger;

namespace Web_Framework.lib.err;

public class InvalidServiceHandlerException : Exception
{
    public InvalidServiceHandlerException(Func<StatusCode> service, int exitCode = 2)
    {
        Logger.GetLogger().Log(Logger.LogLevel.Error, $"And error occurred whilst trying to invoke service {service.Method.Name}\n{service.Method.DeclaringType.FullName}.{service.Method.Name} does not have an associated path to reach its handler.");
        Environment.Exit(exitCode);
    }
}
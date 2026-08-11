using Web_Framework.logger;

namespace Web_Framework.lib.err;

public class CommandAlreadyImplementedException : Exception
{
    public CommandAlreadyImplementedException(string name)
    {
        Logger.GetLogger().Log(Logger.LogLevel.Warning, $"The command with the name: {name} has already implemented.\nThe duplicate command has been ignored and won't be executed when {name} is run");
    }
}
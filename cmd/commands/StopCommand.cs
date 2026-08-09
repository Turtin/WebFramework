using Web_Framework.cmd;
using Web_Framework.logger;

namespace Web_Framework.cmd.commands;

public class StopCommand: Command
{
    private Logger logger = Logger.GetLogger();
    
    public void Run(string[] args, string command)
    {
        logger.Log(Logger.LogLevel.Info, "Shutting down server...");

        Program.server.Stop();
        CommandSystem.StopListener();

        logger.Log(Logger.LogLevel.Info, "Server shutdown completed");
    }
}
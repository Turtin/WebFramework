using Web_Framework.cmd;
using Web_Framework.logger;

namespace Web_Framework.cmd.commands;

public class StopCommand: Command
{
    private Logger logger = Logger.GetLogger();
    
    public void Run(string[] args, string command)
    {
        logger.Log(Logger.LogLevel.Info, "Stopping server");
        
        Program.server.Stop();
        CommandListener.StopListener();

        logger.Log(Logger.LogLevel.Info, "Stopped server");
    }
}
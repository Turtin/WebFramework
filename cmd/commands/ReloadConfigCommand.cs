using Web_Framework.file;
using Web_Framework.logger;

namespace Web_Framework.cmd.commands;

public class ReloadConfigCommand : Command
{
    public void Run(string[] args, string command)
    {
        Logger logger = Logger.GetLogger();
        
        logger.Log(Logger.LogLevel.Info, "Reloading config...");
        
        ConfigManager.GetManager().RefreshConfig();
        
        logger.Log(Logger.LogLevel.Info, "Config reloaded");
    }
}
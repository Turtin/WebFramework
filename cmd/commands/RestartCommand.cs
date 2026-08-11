namespace Web_Framework.cmd.commands;

public class RestartCommand : Command
{
    public void Run(string[] args, string command) // Needs reworking to be done properly, causes lag after use
    {
        // Stop the current instance
        new StopCommand().Run(args, command);
        
        // start the new instance
        CommandSystem.UnregisterCommands(); // Clear the commands so that they don't cause errors when being registered later.
        Program.StartupProccess();
    }
}
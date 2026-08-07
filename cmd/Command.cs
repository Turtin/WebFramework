namespace Web_Framework.cmd;

public interface Command
{
    /// <summary>
    /// Implement this method to make a command and then register it using the register method in CommandListener
    /// </summary>
    /// <param name="args">The arguments passed after the name of the command</param>
    /// <param name="command">The full command string</param>
    public void Run(string[] args, string command);
}
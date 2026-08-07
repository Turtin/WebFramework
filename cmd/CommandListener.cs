using System.Text;
using Web_Framework.logger;

namespace Web_Framework.cmd;

public static class CommandListener
{
    private static Thread _thread;
    private static bool _running;
    private static Dictionary<string, Command> _commands = new Dictionary<string, Command>();
    
    /// <summary>
    /// This should be run is a separate thread to allow commands to be handled alongside the rest of the server functions
    /// This method handles the user input manually allowing for entering commands and executing them
    /// </summary>
    private static void CommandEntry()
    {
        StringBuilder charbuffer = new StringBuilder();
        
        while (_running)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (charbuffer.Length == 0) continue;
                    
                    Console.Write("\n");
                    
                    string commandText = charbuffer.ToString();
                    string[] commandComponents = commandText.Split(" ");
                    
                    if (!_commands.ContainsKey(commandText))
                    {
                        Logger.GetLogger().Log(Logger.LogLevel.Info, "Command not found: " + commandComponents[0]);
                        charbuffer.Clear();
                        continue;
                    }

                    string commandName = commandComponents[0];
                    string[] commandArgs = commandComponents.Skip(1).ToArray();

                    _commands.TryGetValue(commandName, out Command command);

                    command.Run(commandArgs, commandText);
                    charbuffer.Clear();
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (charbuffer.Length > 0)
                    {
                        charbuffer.Remove(charbuffer.Length - 1, 1);
                        Console.Write($"\r> {charbuffer} ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    }
                }
                else
                {
                    charbuffer.Append(keyInfo.KeyChar);
                    Console.Write($"\r> {charbuffer}");
                }
            }
        }
    }

    /// <summary>
    /// Registers a command to the registry so that when it is called it can be executed.
    /// </summary>
    /// <param name="name">What in terminal will trigger the command</param>
    /// <param name="command">The code to trigger when then command is executed</param>
    public static void RegisterCommand(string name, Command command)
    {
        _commands.Add(name, command);
    }

    /// <summary>
    /// Makes the server begin listening for commands from the user.
    /// </summary>
    public static void StartListener()
    {
        _thread = new Thread(CommandEntry);
        _thread.Name = "CMD";
        _running = true;
        
        _thread.Start();
    }

    /// <summary>
    /// Stops the server from listening for commands
    /// </summary>
    public static void StopListener()
    {
        _running = false;
        _thread.Join();
    }
}
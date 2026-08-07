namespace Web_Framework.logger;

public class Logger
{
    public enum LogLevel
    {
        Info,
        Warning,
        Error
    }

    private static Logger _instance;
    
    private Logger() {}

    public static Logger GetLogger()
    {
        if (_instance == null)
        {
            _instance = new Logger();
        }
        
        return _instance;
    }

    /// <summary>
    /// Allows for logging into terminal window wiih automatic colour handling and such
    /// </summary>
    /// <param name="level">The type of output that is being outputted</param>
    /// <param name="message">The message to be displayed to the Terminal</param>
    public void Log(LogLevel level, string message)
    {
        switch (level)
        {
         case LogLevel.Info:
             Console.ForegroundColor = ConsoleColor.White;
             break;
         case LogLevel.Error:
             Console.ForegroundColor = ConsoleColor.Red;
             break;
         case LogLevel.Warning:
             Console.ForegroundColor = ConsoleColor.Yellow;
             break;
        }

        string type = level.ToString().ToUpper();
        string currentTime = DateTime.Now.ToString("HH:mm:ss");
        string thread = Thread.CurrentThread.Name == null ? "Unknown" : Thread.CurrentThread.Name;
        
        Console.WriteLine($"[{currentTime}] [{type}] [{thread}]: {message}");
        Console.ResetColor();
    }
}
using System.Runtime.CompilerServices;
using Web_Framework.cmd;
using Web_Framework.cmd.commands;
using Web_Framework.logger;
using Web_Framework.server;

class Program
{
    private static Logger logger = Logger.GetLogger();
    public static HttpServer server;

    public static void StartupProccess()
    {
        logger.Log(Logger.LogLevel.Info, "Initialising startup sequence...");


        // Setup
        Thread.CurrentThread.Name = "Startup";

        // Event Registration:
        try
        {
            // Connection Event
            HttpServer.RegisterConnectionEvent(new ConnectionEventHandler());
        }
        catch (Exception ex)
        {
            logger.Log(Logger.LogLevel.Warning, "A non-fatal error occured during the startup sequence.\n Failed to register an event handler");
            logger.Log(Logger.LogLevel.Warning, ex.Message);
        }
        

        // Command registration
        try
        {
            CommandSystem.RegisterCommand("stop", new StopCommand());
            CommandSystem.RegisterCommand("restart", new RestartCommand());
        }
        catch (Exception ex)
        {
            logger.Log(Logger.LogLevel.Warning, "A non-fatal error occured during the startup sequence.\n Failed to register a command handler");
            logger.Log(Logger.LogLevel.Warning, ex.Message);
        }

        // Server Start:
        // Start Http service
        server = HttpServer.GetServer();
        server.Create();
        server.Start();

        // Enable Commands
        CommandSystem.StartListener();
    }

    public static void Main(string[] args)
    {
        StartupProccess();
    }
}

// var response = new HttpResponse();
// var header = new HttpHeader.HttpHeaderData<object>();
//
// header.CreateHeader("Content-Type", IntegerType.FromObject(2));
// header.SetStringifier();
// response.AddHeader(header);
//
// Console.WriteLine(Encoding.ASCII.GetString(response.GetResponseBytes()));
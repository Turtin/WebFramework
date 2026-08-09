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
        // Connection Event
        HttpServer.RegisterConnectionEvent(new ConnectionEventHandler());
        
        // Command registration
        CommandSystem.RegisterCommand("stop", new StopCommand());
    
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
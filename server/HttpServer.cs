using System.Net;
using System.Net.Sockets;
using Web_Framework.http;
using Web_Framework.logger;

namespace Web_Framework.server;

public class HttpServer
{
    private Logger logger = Logger.GetLogger();
    private static HttpServer _server = null!;
    private static List<IConnectionEvent> _connectionEvents = new List<IConnectionEvent>();
    private Socket _serverSocket;
    private bool _serverRunning = false;
    private Thread _listenerThread;

    private HttpServer() {}

    public interface IConnectionEvent
    {
        public void Handle(Socket client);
    }

    public static void RegisterConnectionEvent(IConnectionEvent eventHandler)
    {
        _connectionEvents.Add(eventHandler);
    }

    public static HttpServer GetServer()
    {
        _server ??= new HttpServer();

        return _server;
    }

    public void Create()
    {
        // Prepare an endpoint
        IPAddress ip = IPAddress.Parse("0.0.0.0");
        IPEndPoint endPoint = new IPEndPoint(ip, 80);
        
        _serverSocket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        _serverSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
        _serverSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
        _serverSocket.Bind(endPoint);
    }

    private void ConnectionListener()
    {
        logger.Log(Logger.LogLevel.Info, "Listening...");   
        while (_serverRunning)
        {
            try
            {
                // Polling rate 2 Hz
                if (_serverSocket.Poll(500000, SelectMode.SelectRead))
                {
                    Socket client = _serverSocket.Accept();

                    Task.Run(() => // Prevent any bad event handling from breaking the server
                    {
                        Thread.CurrentThread.Name = "HTTP"; // Fixes logging name
                        
                        foreach (IConnectionEvent connectionEvent in _connectionEvents)
                        {
                            connectionEvent.Handle(client);
                        }
                    });
                }
            } catch (Exception e) when (e is SocketException || e is ObjectDisposedException) {
                logger.Log(Logger.LogLevel.Error, "Something went wrong...");
                logger.Log(Logger.LogLevel.Error, e.Message);
            }
        }
        logger.Log(Logger.LogLevel.Info, "Stopped connection listener");
    }

    public void Start()
    {
        logger.Log(Logger.LogLevel.Info, $"Starting listener on {_serverSocket.LocalEndPoint}");
        
        _listenerThread = new Thread(new ThreadStart(ConnectionListener));
        _listenerThread.Name = "HTTP";
        _serverRunning  = true;
        _serverSocket.Listen(10);
        _listenerThread.Start();
        
        logger.Log(Logger.LogLevel.Info, "Server started");
    }

    public void Stop()
    {  
        _serverRunning = false;
        _listenerThread?.Join();
    }
}
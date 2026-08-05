using System.Net;
using System.Net.Sockets;
using Web_Framework.http;

namespace Web_Framework.server;

public class HttpServer
{
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
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        IPEndPoint endPoint = new IPEndPoint(ip, 80);
        
        _serverSocket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        _serverSocket.Bind(endPoint);
    }

    private void ConnectionListener()
    {
        Console.WriteLine("Listening...");   
        while (_serverRunning)
        {
            Socket client = _serverSocket.Accept();

            foreach (IConnectionEvent connectionEvent in _connectionEvents)
            {
                connectionEvent.Handle(client);
            }
        }
        Console.WriteLine("Connection listener stopped");
    }

    public void Start()
    {
        Console.WriteLine($"Starting listener on {_serverSocket.LocalEndPoint}");
        
        _listenerThread = new Thread(new ThreadStart(ConnectionListener));
        
        _serverRunning  = true;
        _serverSocket.Listen(10);
        _listenerThread.Start();
        
        Console.WriteLine("Server started");
    }

    public void Stop()
    {
        Console.WriteLine("Stopping server");
        
        _serverRunning = false;
        _listenerThread?.Join();
    }
}
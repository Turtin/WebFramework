using System.Net.Sockets;
using System.Text;
using Web_Framework.http;
using Web_Framework.lib;
using Web_Framework.logger;
using Web_Framework.web;

namespace Web_Framework.server;

public class ConnectionEventHandler : HttpServer.IConnectionEvent
{
    private Logger logger = Logger.GetLogger();
    private WebPaths _paths = WebPaths.GetInstance();
    
    public void Handle(Socket client)
    {
        byte[] bytes = new byte[1024];
        StringBuilder data = new  StringBuilder();

        while (true)
        {
            int numByte = client.Receive(bytes);

            if (numByte <= 0) break;
            
            data.Append(Encoding.ASCII.GetString(bytes, 0, numByte));
            
            if (data.ToString().Contains("\r\n\r\n")) break;
        }
        
        string dataString = data.ToString();
        
        HttpRequest request = HttpRequest.ParseRequest(dataString);
        
        logger.Log(Logger.LogLevel.Info, $"{request.Method.ToString()} {request.Path} {client.RemoteEndPoint}");


        HttpResponse response = new();
        
        Code statusCode = _paths.GetPath(request.Path).Invoke(response);
        response.StatusCode = statusCode;
        response.HttpVersion = "HTTP/1.1";
        
        Logger.GetLogger().Log(Logger.LogLevel.Info, Encoding.ASCII.GetString(response.GetResponseBytes())); // temp
        client.Send(response.GetResponseBytes());
        client.Close();
    }
}
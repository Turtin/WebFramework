using System.Net.Sockets;
using System.Text;
using Web_Framework.http;
using Web_Framework.logger;

namespace Web_Framework.server;

public class ConnectionEventHandler : HttpServer.IConnectionEvent
{
    private Logger logger = Logger.GetLogger();
    
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
        
        int code = StatusCode.Ok.StatusCode;
        client.Send(Encoding.ASCII.GetBytes(code + " Ok"));
        client.Close();
    }
}
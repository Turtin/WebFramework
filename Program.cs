using System.Text;
using Microsoft.VisualBasic.CompilerServices;
using Web_Framework.http;
using Web_Framework.server;

HttpServer.RegisterConnectionEvent(new ConnectionEventHandler());

HttpServer server = HttpServer.GetServer();
server.Create();
server.Start();

var response = new HttpResponse();
var header = new HttpHeader.HttpHeaderData<object>();

header.CreateHeader("Content-Type", IntegerType.FromObject(2));
header.SetStringifier();
response.AddHeader(header);

Console.WriteLine(Encoding.ASCII.GetString(response.GetResponseBytes()));
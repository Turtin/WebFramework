using System.Reflection;
using Web_Framework.http;
using Web_Framework.lib;
using Web_Framework.lib.err;

namespace Web_Framework.web;

public class WebPaths
{
    private Tree<Func<StatusCode>> _paths = new();

    public void AddPath(Func<StatusCode> service)
    {
        Destination serviceInfo = service.Method.GetCustomAttribute<Destination>() ?? throw new InvalidServiceHandlerException(service);
    }
}
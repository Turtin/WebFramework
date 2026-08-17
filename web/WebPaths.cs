using System.Reflection;
using System.Runtime.CompilerServices;
using Web_Framework.http;
using Web_Framework.lib;
using Web_Framework.lib.err;
using Web_Framework.logger;

namespace Web_Framework.web;

public class WebPaths
{
    private Tree<Func<HttpResponse, Code>> _paths = new();
    private static WebPaths _instance;
    
    private WebPaths() {}

    public static WebPaths GetInstance()
    {
        _instance ??= new WebPaths();
        return _instance;
    }

    public void AddPath(Func<HttpResponse, Code> service)
    {
        Destination serviceInfo = service.Method.GetCustomAttribute<Destination>() ?? throw new InvalidServiceHandlerException(service);
        string path = serviceInfo.Path;
        string[] segments = path.Split('/');
        
        // Handle case where the path is at the head (most cases probably)
        if (segments.Length == 1)
        {
            _paths.AddData(service);
            return;
        }

        Tree<Func<HttpResponse, Code>>.Node<Func<HttpResponse, Code>> currentNode = _paths.GetNode(segments[0]);
        
        foreach (string segment in path.Split('/'))
        {
            segments = segments.Skip(1).ToArray();
            
            if (segments.Length == 1)
            {
                currentNode.AddData(service);
                return;
            }

            if (currentNode.Children.ContainsKey(segment))
            {
                currentNode = currentNode.GetNode(segment);
            }
            else
            {
                currentNode.AddNode(segment, service);
                currentNode = currentNode.GetNode(segment);
            }
        }
    }

    public void AddContent(Type contentClass)
    {
        List<MethodInfo> methods = contentClass.GetMethods().ToList();

        foreach (MethodInfo method in methods)
        {
            if (method.GetCustomAttribute<Destination>() != null)
            {
                AddPath((Func<HttpResponse, Code>) Delegate.CreateDelegate(typeof(Func<HttpResponse, Code>), method));
            }
        }
    }
    
    public Func<HttpResponse, Code> GetPath(string path)
    {
        string[]  segments = path.Split('/');

        try
        {
            Tree<Func<HttpResponse, Code>>.Node<Func<HttpResponse, Code>> currentNode = _paths.GetNode(segments[0]);
            
            foreach (string segment in segments)
            {
                segments = segments.Skip(1).ToArray();
                if (segments.Length == 1)
                {
                    return currentNode.Data[0];
                }
                currentNode = currentNode.GetNode(segment);
            }
        }
        catch (Exception ex)
        {
            Logger.GetLogger().Log(Logger.LogLevel.Warning, $"Path not found: {path}");
        }

        return DefaultPage; // temp
    }

    [Destination("*", RequestMethods.GET)]
    private Code DefaultPage(HttpResponse response)
    {
        return StatusCode.NotFound;
    }
}
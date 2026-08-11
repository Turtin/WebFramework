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
        string path = serviceInfo.Path;
        string[] segments = path.Split('/');
        
        // Handle case where the path is at the head (most cases probably)
        if (segments.Length == 1)
        {
            _paths.AddData(service);
            return;
        }

        Tree<Func<StatusCode>>.Node<Func<StatusCode>> currentNode = _paths.GetNode(segments[0]);
        
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
                AddPath((Func<StatusCode>) Delegate.CreateDelegate(typeof(Func<StatusCode>), method));
            }
        }
    }
    
    public Func<StatusCode> GetPath(string path)
    {
        return null; // temp
    }
}
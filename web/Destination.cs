using Web_Framework.http;

namespace Web_Framework.web;

[AttributeUsage(AttributeTargets.Method)]
public class Destination(string path, RequestMethods method) : Attribute
{
    public string Path { get; } = path;
    public RequestMethods Method { get; } = method;
}
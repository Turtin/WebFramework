using Web_Framework.http;

namespace Web_Framework.web;

[AttributeUsage(AttributeTargets.Method)]
public class Destination(string path, RequestMethods method) : Attribute
{
    public string path { get; } = path;
    public RequestMethods method { get; } = method;
}
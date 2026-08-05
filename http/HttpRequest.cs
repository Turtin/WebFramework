namespace Web_Framework.http;

public class HttpRequest(RequestMethods method, string path, string version)
{
    public RequestMethods Method = method;
    public string Path = path;
    public string Version = version;
    private List<HttpHeader.IHttpHeaders<object>> _headers = new List<HttpHeader.IHttpHeaders<object>>();

    /// <summary>
    /// This overloaded contractor provides alternate path for setting up the server side request allow for the header
    /// registration to be handled externally rather than internally
    /// </summary>
    /// <param name="method">The request method from the client</param>
    /// <param name="path">The path that the request is asking for</param>
    /// <param name="version">The version of the http request</param>
    /// <param name="requestHeaders">The remaining generic headers</param>
    public HttpRequest(RequestMethods method, string path, string version, List<HttpHeader.IHttpHeaders<object>> requestHeaders) : this(method, path, version)
    {
        _headers = requestHeaders;
    }

    public void AppendRequestHeader(HttpHeader.IHttpHeaders<object> header)
    {
        _headers.Add(header);
    }

    /// <summary>
    /// Takes the request string and turns it into an object that can be handled by th code
    /// </summary>
    /// <param name="requestHeaders">The header text from the direct request</param>
    /// <returns>The request object</returns>
    public static HttpRequest ParseRequest(string requestHeaders)
    {
        string[] headers = requestHeaders.Split('\n');
        string[] requestLine = headers[0].Split(' ');

        HttpRequest request = new HttpRequest(
            ((RequestMethods) Enum.Parse(typeof(RequestMethods), requestLine[0])),
            requestLine[1],
            requestLine[2]
        );

        foreach (string header in headers.Skip(1).ToArray())
        {
            
        }
        
        
        return request;
    }
}
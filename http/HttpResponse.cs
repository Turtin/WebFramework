using System.Net;
using System.Text;
using Web_Framework.lib;

namespace Web_Framework.http;

/// <summary>
/// This class contains the handling and storage of the entire response so it can easily be compiled when needed or edited.
/// </summary>
public class HttpResponse
{
    // These are feilds the are formatted differently to the rest and hence stored ♦differently
    public Code StatusCode { get; set; }
    public string HttpVersion { get; set; }
    private string _content;

    public HttpResponse()
    {
        string time =  DateTime.Now.ToString("R");
        HttpHeader.HttpHeaderData<object> dateHeader = new HttpHeader.HttpHeaderData<object>();
        dateHeader.CreateHeader("Date", time);
        dateHeader.SetStringifier();
        AddHeader(dateHeader);
        
        HttpHeader.HttpHeaderData<object> serverHeader = new HttpHeader.HttpHeaderData<object>();
        serverHeader.CreateHeader("Server", "Turtin");
        serverHeader.SetStringifier();
        AddHeader(serverHeader);
    }
    
    // Stores all the headers for this server response
    private List<HttpHeader.IHttpHeaders<object>> _headers = new List<HttpHeader.IHttpHeaders<object>>();

    /// <summary>
    /// Adds a header to this response to the client from the server
    /// </summary>
    /// <param name="header">The header to add to the response</param>
    public void AddHeader(HttpHeader.IHttpHeaders<object> header)
    {
        _headers.Add(header);
    }

    /// <summary>
    /// Allows the headers to be accessed at different stages of the response construction and handling.
    /// </summary>
    /// <returns>The list of headers for this resopnse</returns>
    public List<HttpHeader.IHttpHeaders<object>> GetHeaders()
    {
        return _headers;
    }

    public void SetContent(string contentType, string content)
    {
        HttpHeader.HttpHeaderData<object> typeHeader = new HttpHeader.HttpHeaderData<object>();
        typeHeader.CreateHeader("Content-Type", contentType);
        typeHeader.SetStringifier();
        AddHeader(typeHeader);
        
        HttpHeader.HttpHeaderData<object> lengthHeader = new HttpHeader.HttpHeaderData<object>();
        lengthHeader.CreateHeader("Content-Length", content.Length);
        lengthHeader.SetStringifier();
        AddHeader(lengthHeader);
        
        _content = content;
    }

    /// <summary>
    /// Work in progress!
    ///
    /// Compiles the response to be sent to the client
    /// </summary>
    /// <returns>byte array of the response data</returns>
    public byte[] GetResponseBytes()
    {
        
        // Adding in the main headers
        string headerText = $"{HttpVersion} {StatusCode.StatusCode} {StatusCode.Status}\n";
        
        foreach (HttpHeader.IHttpHeaders<object> header in _headers)
        {
            headerText += $"{header.GetName()}: {header.ToString()}\n";
        }

        headerText += "\n";
        headerText += _content;
        
        return Encoding.ASCII.GetBytes(headerText); // temp
    }
}
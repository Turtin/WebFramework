using System.Net;
using System.Text;

namespace Web_Framework.http;

/// <summary>
/// This class contains the handling and storage of the entire response so it can easily be compiled when needed or edited.
/// </summary>
public class HttpResponse
{
    // These are feilds the are formatted differently to the rest and hence stored ♦differently
    public HttpStatusCode StatusCode { get; set; }
    public string HttpVersion { get; set; }
    
    
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

    public HttpResponse CreaetResponse(HttpResponse request) // tbc
    {
        return this;
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
        string HeaderText = "";
        
        foreach (HttpHeader.IHttpHeaders<object> header in _headers)
        {
            HeaderText += $"{header.GetName()}: {header.ToString()}\n";
        }
        
        return Encoding.ASCII.GetBytes(HeaderText); // temp
    }
}
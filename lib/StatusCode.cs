namespace Web_Framework.http;

public readonly record struct Code(int StatusCode, string Status);

// Source https://umbraco.com/knowledge-base/http-status-codes/
public class StatusCode
{
    // Info Response
    public static readonly Code Continue = new(100, "Continue");
    public static readonly Code SwitchingProtocols = new(101, "Switching Protocols");
    public static readonly Code Processing = new(102, "Processing");
    public static readonly Code EarlyHints = new(103, "Early Hints");

    // Success Response
    public static readonly Code Ok = new(200, "OK");
    public static readonly Code Created = new(201, "Created");
    public static readonly Code Accepted = new(202, "Accepted");
    public static readonly Code NonAuthoritative = new(203, "Non-Authoritative Information");
    public static readonly Code NoContent = new(204, "No Content");
    public static readonly Code ResetContent = new(205, "Reset Content");
    public static readonly Code PartialContent = new(206, "Partial Content");
    public static readonly Code MultiStatus = new(207, "Multi-Status");
    public static readonly Code AlreadyReported = new(208, "Already Reported");
    public static readonly Code IMUsed = new(226, "IM Used");

    // Redirection
    public static readonly Code MultipleChoices = new(300, "Multiple Choices");
    public static readonly Code MovedPermanently = new(301, "Moved Permanently");
    public static readonly Code Found = new(302, "Found");
    public static readonly Code SeeOther = new(303, "See Other");
    public static readonly Code NotModified = new(304, "Not Modified");
    public static readonly Code UseProxy = new(305, "Use Proxy");
    public static readonly Code SwitchProxy = new(306, "Switch Proxy");
    public static readonly Code TemporaryRedirect = new(307, "Temporary Redirect");
    public static readonly Code PermanentRedirect = new(308, "Permanent Redirect");

    // Client Error
    public static readonly Code BadRequest = new(400, "Bad Request");
    public static readonly Code Unauthorized = new(401, "Unauthorized");
    public static readonly Code PaymentRequired = new(402, "Payment Required");
    public static readonly Code Forbidden = new(403, "Forbidden");
    public static readonly Code NotFound = new(404, "Not Found");
    public static readonly Code MethodNotAllowed = new(405, "Method Not Allowed");
    public static readonly Code NotAcceptable = new(406, "Not Acceptable");
    public static readonly Code ProxyAuthentication = new(407, "Proxy Authentication Required");
    public static readonly Code RequestTimeout = new(408, "Request Timeout");
    public static readonly Code Conflict = new(409, "Conflict");
    public static readonly Code Gone = new(410, "Gone");
    public static readonly Code LengthRequired = new(411, "Length Required");
    public static readonly Code PreconditionFailed = new(412, "Precondition Failed");
    public static readonly Code PayloadTooLarge = new(413, "Payload Too Large");
    public static readonly Code URITooLong = new(414, "URI Too Long");
    public static readonly Code UnsupportedMediaType = new(415, "Unsupported Media Type");
    public static readonly Code RequestedRangeNotSatisfiable = new(416, "Range Not Satisfiable");
    public static readonly Code ExpectationFailed = new(417, "Expectation Failed");
    public static readonly Code ImATeapot = new(418, "I'm a teapot"); // Hhehehe
    public static readonly Code MisdirectedRequest = new(421, "Misdirected Request");
    public static readonly Code UnprocessableEntity = new(422, "Unprocessable Entity");
    public static readonly Code Locked = new(423, "Locked");
    public static readonly Code FailedDependency = new(424, "Failed Dependency");
    public static readonly Code TooEarly = new(425, "Too Early");
    public static readonly Code UpgradeRequired = new(426, "Upgrade Required");
    public static readonly Code PreconditionRequired = new(428, "Precondition Required");
    public static readonly Code TooManyRequests = new(429, "Too Many Requests");
    public static readonly Code RequestHeaderFieldsTooLarge = new(431, "Request Header Fields Too Large");
    public static readonly Code UnavailableForLegalReasons = new(451, "Unavailable For Legal Reasons");

    // Server Error
    public static readonly Code InternalServerError = new(500, "Internal Server Error");
    public static readonly Code NotImplemented = new(501, "Not Implemented");
    public static readonly Code BadGateway = new(502, "Bad Gateway");
    public static readonly Code ServiceUnavailable = new(503, "Service Unavailable");
    public static readonly Code GatewayTimeout = new(504, "Gateway Timeout");
    public static readonly Code HttpVersionNotSupported = new(505, "HTTP Version Not Supported");
    public static readonly Code VariantAlsoNegotiates = new(506, "Variant Also Negotiates");
    public static readonly Code InsufficientStorage = new(507, "Insufficient Storage");
    public static readonly Code LoopDetected = new(508, "Loop Detected");
    public static readonly Code NotExtended = new(510, "Not Extended");
    public static readonly Code NetworkAuthenticationRequired = new(511, "Network Authentication Required");
}

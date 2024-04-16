using RestSharp;

namespace Apps.Termbase.io.Api;

public class TermbaseRequestParameters
{
    public string Url { get; set; }
    
    public Method Method { get; init; }
}
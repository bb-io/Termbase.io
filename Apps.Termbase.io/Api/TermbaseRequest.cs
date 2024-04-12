using Apps.Termbase.io.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.Termbase.io.Api;

public class TermbaseRequest : RestRequest
{
    public TermbaseRequest(TermbaseRequestParameters requestParameters,
        IEnumerable<AuthenticationCredentialsProvider> creds) : base(requestParameters.Url, requestParameters.Method)
    {
        var token = creds.First(x => x.KeyName == CredsNames.ApiKey).Value;
        this.AddHeader("Authorization", $"Bearer {token}");
    }
}
using RestSharp;
using Apps.Termbase.io.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Blackbird.Applications.Sdk.Common.Exceptions;

namespace Apps.Termbase.io.Api;

public class TermbaseClient : RestClient
{
    public TermbaseClient() : base(new RestClientOptions { BaseUrl = new(Urls.Api) })
    {
    }
    
    public async Task<T> ExecuteWithJson<T>(string endpoint, Method method, object? bodyObj,
        AuthenticationCredentialsProvider[] creds)
    {
        var response = await ExecuteWithJson(endpoint, method, bodyObj, creds);
        return JsonConvert.DeserializeObject<T>(response.Content);
    }
    
    public async Task<T> ExecuteWithJson<T>(RestRequest request)
    {
        var response = await ExecuteWithHandling(request);
        return JsonConvert.DeserializeObject<T>(response.Content);
    }
    
    private async Task<RestResponse> ExecuteWithJson(string endpoint, Method method, object? bodyObj,
        AuthenticationCredentialsProvider[] creds)
    {
        var request = new TermbaseRequest(new()
        {
            Url = Urls.Api + endpoint,
            Method = method,
        }, creds);

        if (bodyObj is not null)
            request.WithJsonBody(bodyObj, new()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                NullValueHandling = NullValueHandling.Ignore
            });

        return await ExecuteWithHandling(request);
    }

    public async Task<RestResponse>ExecuteWithHandling(RestRequest request)
    {
        var response = await ExecuteAsync(request);

        if (response.IsSuccessStatusCode)
            return response;
        
        throw new PluginApplicationException(BuildErrorMessage(response));
    }
    
    private string BuildErrorMessage(RestResponse response)
    {
        return $"Status code: {response.StatusCode}, Content: {response.Content}";
    }
}

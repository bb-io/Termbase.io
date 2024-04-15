using RestSharp;
using Apps.Termbase.io.Api;
using Apps.Termbase.io.Constants;
using Apps.Termbase.io.Webhooks.Models.Request;
using Apps.Termbase.io.Webhooks.Models.Responses;
using Blackbird.Applications.Sdk.Common.Authentication;

namespace Apps.Termbase.io.Services;

public class WebhookService
{
    private TermbaseClient Client { get; }

    private readonly string _logUrl = "https://webhook.site/3966c5a3-dfaf-41e5-abdf-bbf02a5f9823";

    public WebhookService()
    {
        Client = new();
    }

    public async Task SubscribeAsync(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        Dictionary<string, string> values,
        string SubscriptionEvent
    )
    {
        var request = new TermbaseRequest(new TermbaseRequestParameters
        {
            Url = Urls.Api + ApiEndpoints.Webhooks,
            Method = Method.Post
        }, creds);
        request.AddJsonBody(new AddWebhookRequest
        {
            Uuid = Guid.NewGuid().ToString(),
            Url = values["payloadUrl"],
            WebhookAction = GetWebhookActionFromEvent(SubscriptionEvent)
        });

        var response = await Client.ExecuteWithJson<AddWebhookResponse>(request);
        
        await LogAsync(new
        {
            PayloadUrl = values["payloadUrl"],
            WebhookAction = GetWebhookActionFromEvent(SubscriptionEvent),
            Response = response
        });
    }

    public async Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> creds,
        Dictionary<string, string> values,
        string SubscriptionEvent
    )
    {
        var request = new TermbaseRequest(new TermbaseRequestParameters
            {
                Url = Urls.Api + ApiEndpoints.Webhooks,
                Method = Method.Delete
            }, creds)
            .AddJsonBody(new AddWebhookRequest
            {
                Url = values["payloadUrl"],
                WebhookAction = GetWebhookActionFromEvent(SubscriptionEvent)
            });

        var response = await Client.ExecuteWithJson<DeleteWebhookResponse>(request);
        
        await LogAsync(new
        {
            PayloadUrl = values["payloadUrl"],
            WebhookAction = GetWebhookActionFromEvent(SubscriptionEvent),
            Response = response
        });
    }

    private string GetWebhookActionFromEvent(string subscriptionEvent)
    {
        return Urls.ApiBase + ApiEndpoints.WebhookActions + "/" + subscriptionEvent;
    }

    private async Task LogAsync<T>(T obj) 
        where T : class
    {
        var request = new RestRequest(_logUrl, Method.Post)
            .AddJsonBody(obj);
        
        var restClient = new RestClient();
        await restClient.ExecuteAsync(request);
    }
}
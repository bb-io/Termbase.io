using Apps.Termbase.io.Constants;
using Apps.Termbase.io.RestSharp;
using Apps.Termbase.io.Webhooks.Models.Request;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.Termbase.io.Webhooks.Handlers.Base;

/// <summary>
/// Base handler for parameterless webhooks
/// </summary>
public class WebhookService
{
    private AppRestClient Client { get; }
    
    public WebhookService()
    {
        Client = new();
    }

    /// <summary>
    /// Subscribes to a webhook event
    /// </summary>
    public Task SubscribeAsync(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        Dictionary<string, string> values, 
        string SubscriptionEvent
    )
    {
        var request = new AppRestRequest(ApiEndpoints.Webhooks, Method.Post, creds);
        request.AddJsonBody(new AddWebhookRequest
        {
            // Webhook bird url that will receive webhook data
            Uuid = Guid.NewGuid().ToString(),
            Url = values["payloadUrl"],
            WebhookAction = GetWebhookActionFromEvent(SubscriptionEvent)
        });

        return Client.ExecuteWithHandling(request);
    }

    /// <summary>
    /// Unsubscribes from a webhook event
    /// </summary>
    public Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> creds,
        Dictionary<string, string> values,
        string SubscriptionEvent
    )
    {

        var request = new AppRestRequest(ApiEndpoints.Webhooks, Method.Delete, creds);
        request.AddJsonBody(new AddWebhookRequest
        {
            Url = values["payloadUrl"],
            WebhookAction = GetWebhookActionFromEvent(SubscriptionEvent)
        });

        return Client.ExecuteWithHandling(request);
    }

    private string GetWebhookActionFromEvent(string SubscriptionEvent)
    {
        return Urls.ApiBase + ApiEndpoints.WebhookActions + "/" + SubscriptionEvent;
    }
}
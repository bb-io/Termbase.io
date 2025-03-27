using RestSharp;
using Apps.Termbase.io.Api;
using Apps.Termbase.io.Constants;
using Apps.Termbase.io.Webhooks.Models.Request;
using Apps.Termbase.io.Webhooks.Models.Responses;
using Blackbird.Applications.Sdk.Common.Authentication;

namespace Apps.Termbase.io.Services;

public class WebhookService
{   
    private TermbaseClient Client { get; } = new();

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
    }

    private string GetWebhookActionFromEvent(string subscriptionEvent)
    {
        return Urls.ApiBase + ApiEndpoints.WebhookActions + "/" + subscriptionEvent;
    }
}
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Termbase.io.Webhooks.Handlers.Base;

/// <summary>
/// Base handler for parameterless webhooks
/// </summary>
public abstract class ParameterlessWebhookHandler : IWebhookEventHandler
{
    protected abstract string SubscriptionEvent { get; }
    
    protected ParameterlessWebhookHandler()
    {
    }

    /// <summary>
    /// Subscribes to a webhook event
    /// </summary>
    public Task SubscribeAsync(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        Dictionary<string, string> values)
    {

        var webhookService = new WebhookService();

        return webhookService.SubscribeAsync(
            creds,
            values,
            SubscriptionEvent
        );

    }

    /// <summary>
    /// Unsubscribes from a webhook event
    /// </summary>
    public Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> creds,
        Dictionary<string, string> values)
    {

        var webhookService = new WebhookService();

        return webhookService.UnsubscribeAsync(
            creds,
            values,
            SubscriptionEvent
        );
    }

   
}
using Apps.Termbase.io.Webhooks.Handlers.Base;

namespace Apps.Termbase.io.Webhooks.Handlers;

/// <summary>
/// Handler for item.created webhook
/// </summary>
public class TermChangedHandler : ParameterlessWebhookHandler
{
    protected override string SubscriptionEvent => "termChanged";
}
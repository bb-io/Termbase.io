using Apps.Termbase.io.Webhooks.Handlers.Base;

namespace Apps.Termbase.io.Webhooks.Handlers;

public class TermChangedHandler : ParameterlessWebhookHandler
{
    protected override string SubscriptionEvent => "termChanged";
}
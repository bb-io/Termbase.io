using Apps.Termbase.io.Webhooks.Handlers.Base;

namespace Apps.Termbase.io.Webhooks.Handlers;

public class TermbaseTermCreatedHandler : ParameterlessWebhookHandler
{
    protected override string SubscriptionEvent => "termbaseTermCreated";
}
using Apps.Termbase.io.Webhooks.Handlers.Base;

namespace Apps.Termbase.io.Webhooks.Handlers;

public class TermbaseTermDeletedHandler : ParameterlessWebhookHandler
{
    protected override string SubscriptionEvent => "termbaseTermDeleted";
}
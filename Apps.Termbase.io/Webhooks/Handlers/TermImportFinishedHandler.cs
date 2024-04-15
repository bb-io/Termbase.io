using Apps.Termbase.io.Webhooks.Handlers.Base;

namespace Apps.Termbase.io.Webhooks.Handlers;

public class TermImportFinishedHandler : ParameterlessWebhookHandler
{
    protected override string SubscriptionEvent => "termImportFinished";
}
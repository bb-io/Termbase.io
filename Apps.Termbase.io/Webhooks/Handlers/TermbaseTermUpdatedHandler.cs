﻿using Apps.Termbase.io.Webhooks.Handlers.Base;

namespace Apps.Termbase.io.Webhooks.Handlers;

public class TermbaseTermUpdatedHandler : ParameterlessWebhookHandler
{
    protected override string SubscriptionEvent => "termbaseTermUpdated";
}
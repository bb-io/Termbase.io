using Apps.Termbase.io.Webhooks.Handlers;
using Apps.Termbase.io.Webhooks.Models.Payload;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.Termbase.io.Webhooks;

[WebhookList]
public class WebhookList
{
    #region Webhooks

    [Webhook("On term changed", typeof(TermChangedHandler), Description = "Triggered when a term is changed")]
    public Task<WebhookResponse<TermChangedPayload>> OnTermChanged(WebhookRequest webhookRequest)
        => HandlerWebhook<TermChangedPayload>(webhookRequest);

    [Webhook("On term import finished", typeof(TermImportFinishedHandler), Description = "Triggered when a term import is finished")]
    public Task<WebhookResponse<TermImportFinishedPayload>> OnItemCreated(WebhookRequest webhookRequest)
        => HandlerWebhook<TermImportFinishedPayload>(webhookRequest);

    #endregion

    #region Utils

    private Task<WebhookResponse<T>> HandlerWebhook<T>(WebhookRequest webhookRequest) 
        where T : class
    {
        var data = JsonConvert.DeserializeObject<T>(webhookRequest.Body.ToString());

        if (data is null)
            throw new InvalidCastException(nameof(webhookRequest.Body));

        return Task.FromResult(new WebhookResponse<T>
        {
            Result = data
        });
    }

    #endregion
}
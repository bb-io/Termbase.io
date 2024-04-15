using Apps.Termbase.io.Webhooks.Handlers;
using Apps.Termbase.io.Webhooks.Models.Payload;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.Termbase.io.Webhooks;

[WebhookList]
public class WebhookList
{
    #region Webhooks

    [Webhook("On term changed", typeof(TermChangedHandler), Description = "On term changed")]
    public Task<WebhookResponse<TermChangedPayload>> OnTermChanged(WebhookRequest webhookRequest)
        => HandlerWebhook<TermChangedPayload>(webhookRequest);

    [Webhook("On termImport finished", typeof(TermImportFinishedHandler), Description = "On termImport finished")]
    public Task<WebhookResponse<TermChangedPayload>> OnItemCreated(WebhookRequest webhookRequest)
        => HandlerWebhook<TermChangedPayload>(webhookRequest);

    #endregion

    #region Utils

    private Task<WebhookResponse<T>> HandlerWebhook<T>(WebhookRequest webhookRequest) where T : class
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
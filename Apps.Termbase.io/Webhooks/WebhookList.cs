using Apps.Termbase.io.Services;
using Apps.Termbase.io.Webhooks.Handlers;
using Apps.Termbase.io.Webhooks.Models.Payload;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.Termbase.io.Webhooks;

[WebhookList]
public class WebhookList
{
    #region Webhooks

    [Webhook("On termbase term created", typeof(TermbaseTermCreatedHandler), Description = "Triggered when a termbase term is created")]
    public Task<WebhookResponse<TermbaseTermCreatedPayload>> OnTermbaseTermCreated(WebhookRequest webhookRequest)
        => HandlerWebhook<TermbaseTermCreatedPayload>(webhookRequest);

    [Webhook("On termbase term updated", typeof(TermbaseTermUpdatedHandler), Description = "Triggered when a termbase term is updated")]
    public Task<WebhookResponse<TermbaseTermUpdatedPayload>> OnTermbaseTermUpdated(WebhookRequest webhookRequest)
    => HandlerWebhook<TermbaseTermUpdatedPayload>(webhookRequest);

    [Webhook("On termbase term deleted", typeof(TermbaseTermDeletedHandler), Description = "Triggered when a termbase term is deleted")]
    public Task<WebhookResponse<TermbaseTermDeletedPayload>> OnTermbaseTermDeleted(WebhookRequest webhookRequest)
    => HandlerWebhook<TermbaseTermDeletedPayload>(webhookRequest);

    [Webhook("On term import finished", typeof(TermImportFinishedHandler), Description = "Triggered when a term import is finished")]
    public Task<WebhookResponse<TermImportFinishedPayload>> OnItemCreated(WebhookRequest webhookRequest)
        => HandlerWebhook<TermImportFinishedPayload>(webhookRequest);

    #endregion

    #region Utils

    private Task<WebhookResponse<T>> HandlerWebhook<T>(WebhookRequest webhookRequest)
     where T : class
    {
        var bodyJson = webhookRequest.Body.ToString();

        // Check if T is the payload for TermbaseTermUpdated
        if (typeof(T) == typeof(TermbaseTermUpdatedPayload))
        {
            var termbaseTermEntryFieldService = new TermbaseTermEntryFieldService();
            bodyJson = termbaseTermEntryFieldService.ModifyTermJson(bodyJson);
        }

        var data = JsonConvert.DeserializeObject<T>(bodyJson)
            ?? throw new InvalidCastException(nameof(webhookRequest.Body));

        return Task.FromResult(new WebhookResponse<T>
        {
            Result = data
        });
    }


    #endregion
}
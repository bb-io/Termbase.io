namespace Apps.Termbase.io.Webhooks.Models.Request;

public class AddWebhookRequest
{
    public string Uuid { get; set; }
    public string WebhookAction { get; set; }
    public string Url { get; set; }
}
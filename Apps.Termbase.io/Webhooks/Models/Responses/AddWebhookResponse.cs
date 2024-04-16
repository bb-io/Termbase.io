namespace Apps.Termbase.io.Webhooks.Models.Responses;

public class AddWebhookResponse
{
    public string Uuid { get; set; }
    
    public string Url { get; set; }
    
    public WebhookAction WebhookAction { get; set; }
}

public class WebhookAction
{
    public string Uuid { get; set; }
    
    public string Name { get; set; }
}
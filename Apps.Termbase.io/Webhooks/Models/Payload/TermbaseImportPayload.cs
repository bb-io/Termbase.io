using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Webhooks.Models.Payload;

public class TermImportPayload
{
    [Display("Uuid")]
    public string Uuid { get; set; }
    
    [Display("Name")]
    public string Name { get; set; }
}
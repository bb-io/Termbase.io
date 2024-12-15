using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Webhooks.Models.Payload;

public class TermbaseTermEntryPayload
{
    [Display("Uuid")]
    public string Uuid { get; set; }
  
}
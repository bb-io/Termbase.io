using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Webhooks.Models.Payload;

public class TermbaseTermPayload
{
    [Display("Term")]
    public string Term { get; set; }
    
 
}
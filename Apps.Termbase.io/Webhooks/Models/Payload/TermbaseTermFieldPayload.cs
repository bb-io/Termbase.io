using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Webhooks.Models.Payload;

public class TermbaseTermFieldPayload
{
    
    [Display("Name")]
    public string Name { get; set; }

    [Display("Value")]
    public string Value { get; set; }
}
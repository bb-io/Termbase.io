using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Webhooks.Models.Payload;

public class LanguagePayload
{

    [Display("Uuid")]
    public string Uuid { get; set; }

    [Display("Name")]
    public string Name { get; set; }

    [Display("Code")]
    public string Code { get; set; }
}
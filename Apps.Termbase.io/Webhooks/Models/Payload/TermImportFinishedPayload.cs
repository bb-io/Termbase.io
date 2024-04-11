using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Webhooks.Models.Payload;

public class TermImportFinishedPayload
{
    [Display("Data")]
    public TermImportPayload Termimport { get; set; }
}
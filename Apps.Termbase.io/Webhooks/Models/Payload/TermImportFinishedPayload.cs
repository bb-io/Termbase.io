using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Termbase.io.Webhooks.Models.Payload;

public class TermImportFinishedPayload
{
    [Display("Data"), JsonProperty("termImport")]
    public TermImportPayload TermImport { get; set; }
}
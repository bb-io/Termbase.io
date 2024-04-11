using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Webhooks.Models.Payload;

public class TermChangedPayload
{
    [Display("Termbase")]
    public TermbasePayload Termbase { get; set; }

    [Display("PreUpdatedTermbaseTerm")]
    public TermbaseTermPayload PreUpdatedTermbaseTerm { get; set; }

    [Display("UpdatedTermbaseTerm")]
    public TermbaseTermPayload UpdatedTermbaseTerm { get; set; }
}
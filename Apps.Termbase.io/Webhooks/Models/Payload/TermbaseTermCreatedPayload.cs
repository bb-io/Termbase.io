using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Webhooks.Models.Payload;

public class TermbaseTermCreatedPayload
{
    [Display("Termbase")]
    public TermbasePayload Termbase { get; set; }

    [Display("TermbaseIndex")]
    public TermbaseTermEntryPayload TermbaseTermEntry { get; set; }

    [Display("TermbaseTermEntry")]
    public TermbaseIndexPayload TermbaseIndex { get; set; }

    [Display("Created termbase term")]
    public TermbaseTermPayload CreatedTermbaseTerm { get; set; }

}
using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Webhooks.Models.Payload;

public class TermbaseTermUpdatedPayload
{
    [Display("Termbase")]
    public TermbasePayload Termbase { get; set; }

    [Display("Termbase term entry")]
    public TermbaseTermEntryPayload TermbaseTermEntry { get; set; }

    [Display("Termbase index")]
    public TermbaseIndexPayload TermbaseIndex { get; set; }

    [Display("Pre-updated termbase term")]
    public TermbaseTermPayload PreUpdatedTermbaseTerm { get; set; }

    [Display("Updated termbase term")]
    public TermbaseTermPayload UpdatedTermbaseTerm { get; set; }

}
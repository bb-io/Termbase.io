﻿using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Webhooks.Models.Payload;

public class TermChangedPayload
{
    [Display("Termbase")]
    public TermbasePayload Termbase { get; set; }

    [Display("Pre updated termbase term")]
    public TermbaseTermPayload PreUpdatedTermbaseTerm { get; set; }

    [Display("Updated termbase term")]
    public TermbaseTermPayload UpdatedTermbaseTerm { get; set; }
}
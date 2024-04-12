using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Models.Request;

public class GetTermImportRequest
{
    [Display("Term Import UUID")]
    public string TermImportUuid { get; set; }
}
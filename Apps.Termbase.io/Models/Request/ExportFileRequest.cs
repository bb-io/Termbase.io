using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Models.Request;

public class ExportFileRequest
{
    [Display("Format")]
    public string? Format { get; set; }
    
    [Display("Skip empty terms")]
    public bool? SkipEmptyTerms { get; set; }
}
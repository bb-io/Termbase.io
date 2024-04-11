using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Models.Request;

public class CreateTermImportRequest
{
    // This input is now optional
    public string? Uuid { get; set; }
    public string? TermImportType { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    
}
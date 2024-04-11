using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Models.Dto;

/// <summary>
/// Dto class for item entity
/// </summary>
public class TermImport
{
    // Properties must have display attributes
    // which contain user-friendly name and description of the variable
    [Display("Uuid", Description = "Uuid of the Termbase")]
    public string Uuid { get; set; }

    [Display("Name", Description = "Name of the Termbase")]
    public string Name { get; set; }

    public Termbase TermImportTermbase { get; set; }

    public Status Status { get; set; }
}
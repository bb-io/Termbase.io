using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Models.Dto;

/// <summary>
/// Dto class for item entity
/// </summary>
public class File
{
    // Properties must have display attributes
    // which contain user-friendly name and description of the variable
    [Display("Uuid", Description = "Uuid of the file")]
    public string Uuid { get; set; }

    [Display("OriginalName", Description = "OriginalName of the file")]
    public string OriginalName { get; set; }

}
using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Models.Dto;

/// <summary>
/// Dto class for item entity
/// </summary>
public class Language
{
    // Properties must have display attributes
    // which contain user-friendly name and description of the variable
    [Display("Uuid", Description = "Uuid of the language")]
    public string Uuid { get; set; }

    [Display("Name", Description = "Name of the language")]
    public string Name { get; set; }

}
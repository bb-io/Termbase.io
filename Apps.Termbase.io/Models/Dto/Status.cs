using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Models.Dto;

/// <summary>
/// Dto class for item entity
/// </summary>
public class Status
{
    // Properties must have display attributes
    // which contain user-friendly name and description of the variable
    public string Title { get; set; }

    public float Progress { get; set; }

}
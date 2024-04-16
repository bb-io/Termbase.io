using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Models.Dto;

public class Language
{
    [Display("Uuid", Description = "Uuid of the language")]
    public string Uuid { get; set; }

    [Display("Name", Description = "Name of the language")]
    public string Name { get; set; }

}
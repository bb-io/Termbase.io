using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Models.Dto;

public class TermImport
{
    [Display("Uuid", Description = "Uuid of the Termbase")]
    public string Uuid { get; set; }

    [Display("Name", Description = "Name of the Termbase")]
    public string Name { get; set; }

    public Termbase TermImportTermbase { get; set; }

    public Status Status { get; set; }
}
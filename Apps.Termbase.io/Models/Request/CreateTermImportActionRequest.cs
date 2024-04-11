using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Models.Request;

public class CreateTermImportActionRequest
{

    [Display("Name")] public string Name { get; set; }

    [Display("Description")] public string Description { get; set; }

    [Display("Xliff File Path")] public string FileLocalFullPath { get; set; }

}
using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Models.Request;

public class GetImportTermbaseRequest
{
    [Display("Import termbase uuid", Description = "Uuid of the termbase to import into the termbase")]
    public string ImportTermbaseUuid { get; set; }
}
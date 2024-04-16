using Apps.Termbase.io.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Termbase.io.Models.Request;

public class GetImportTermbaseRequest
{
    [Display("Termbase uuid", Description = "Uuid of the termbase to import into the termbase"), DataSource(typeof(TermBaseDataHandler))]
    public string ImportTermbaseUuid { get; set; }
}
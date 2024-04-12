using Apps.Termbase.io.DataSourceHandlers.StaticDataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Termbase.io.Models.Request;

public class GetImportTermbaseRequest
{
    [Display("Import termbase uuid", Description = "Uuid of the termbase to import into the termbase")]
    public string ImportTermbaseUuid { get; set; }

    [Display("Import termbase name"), StaticDataSource(typeof(ExportFormatStaticDataSourceHandler))]
    public string? Format { get; set; }
}
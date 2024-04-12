using Apps.Termbase.io.DataSourceHandlers.StaticDataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Termbase.io.Models.Request;

public class CreateTermImportActionRequest
{
    [Display("Name")] 
    public string Name { get; set; }

    [Display("Description")] 
    public string? Description { get; set; }

    [Display("File")]
    public FileReference File { get; set; }

    [Display("Termbase type", Description = "By default, it is set to Termbase."), StaticDataSource(typeof(TermImportTypeStaticDataSourceHandler))]
    public string? TermbaseType { get; set; }
}
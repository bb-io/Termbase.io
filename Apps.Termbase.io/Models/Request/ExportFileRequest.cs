using Apps.Termbase.io.DataSourceHandlers.StaticDataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Termbase.io.Models.Request;

public class ExportFileRequest
{
    [Display("Format"), StaticDataSource(typeof(ExportFormatStaticDataSourceHandler))]
    public string? Format { get; set; }
    
    [Display("Skip empty terms")]
    public bool? SkipEmptyTerms { get; set; }
}
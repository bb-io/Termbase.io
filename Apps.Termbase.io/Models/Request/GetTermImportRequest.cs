using Apps.Termbase.io.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Termbase.io.Models.Request;

public class GetTermImportRequest
{
    [Display("Term Import UUID"), DataSource(typeof(TermImportDataHandler))]
    public string TermImportUuid { get; set; }
}
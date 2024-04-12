using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Termbase.io.DataSourceHandlers.StaticDataSourceHandlers;

public class ExportFormatStaticDataSourceHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new Dictionary<string, string>
        {
            { "tbx", "TBX" },
            { "csv", "CSV" },
            { "xlsx", "XLSX" },
        };
    }
}
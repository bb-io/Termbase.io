using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Termbase.io.DataSourceHandlers.StaticDataSourceHandlers;

public class TermImportTypeStaticDataSourceHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new Dictionary<string, string>
        {
            { "/api/term_import_types/termbase", "Termbase" },
            { "/api/term_import_types/xliff", "XLIFF" },
        };
    }
}
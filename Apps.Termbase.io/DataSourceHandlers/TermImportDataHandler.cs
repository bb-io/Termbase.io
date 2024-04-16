using RestSharp;
using Apps.Termbase.io.Constants;
using Apps.Termbase.io.Invocables;
using Apps.Termbase.io.Models.Dto;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Termbase.io.DataSourceHandlers;

public class TermImportDataHandler(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var response =
            await Client.ExecuteWithJson<List<TermImport>>(ApiEndpoints.TermImports, Method.Get, null, Creds.ToArray());
        
        return response
            .Take(20)
            .ToDictionary(x => x.Uuid, x => x.Name);
    }
}
using Apps.Termbase.io.Constants;
using Apps.Termbase.io.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Termbase.io.DataSourceHandlers;

public class TermBaseDataHandler(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var response =
            await Client.ExecuteWithJson<List<Models.Dto.Termbase>>(ApiEndpoints.Termbases, Method.Get, null, Creds.ToArray());
        
        return response
            .Take(20)
            .ToDictionary(x => x.Uuid, x => x.Name);
    }
}
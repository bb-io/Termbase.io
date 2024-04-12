using RestSharp;
using Apps.Termbase.io.Constants;
using Apps.Termbase.io.Invocables;
using Apps.Termbase.io.Models.Dto;
using Apps.Termbase.io.Models.Response;
using Apps.Termbase.io.Api;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Termbase.io.Actions;

[ActionList]
public class LanguageActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    #region Actions
    
    [Action("Get languages", Description = "Get languages")]
    public async Task<GetLanguagesResponse> GetLanguages()
    {
        var request = new TermbaseRequest(new TermbaseRequestParameters
        {
            Url = Urls.Api + ApiEndpoints.Languages,
            Method = Method.Get
        }, Creds);
        
        var response = await Client.ExecuteWithJson<List<Language>>(request);
        var languagesResponse = new GetLanguagesResponse { Languages = response };
        return languagesResponse;
    }
    
    #endregion
}
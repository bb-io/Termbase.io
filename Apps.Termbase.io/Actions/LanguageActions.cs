using System.Net.Mime;
using Apps.Termbase.io.Constants;
using Apps.Termbase.io.Invocables;
using Apps.Termbase.io.Models.Dto;
using Apps.Termbase.io.Models.Response;
using Apps.Termbase.io.RestSharp;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Apps.Termbase.io.Actions.LanguageActions;

/// <summary>
/// Contains list of actions
/// </summary>
[ActionList]
public class LanguageActions : AppInvocable
{
    #region Constructors

    public LanguageActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    #endregion

    #region Actions


    /// <summary>
    /// Retrieves list of items, takes no action parameters
    /// </summary>
    /// <returns>List of languages</returns>
    [Action("Get languages", Description = "Get languages")]
    public async Task<GetLanguagesResponse> GetLanguages()
    {
        var request = new AppRestRequest(ApiEndpoints.Languages, Method.Get, Creds);
        var response = await Client.ExecuteWithHandling<List<Language>>(request);
        var languagesResponse = new GetLanguagesResponse { Languages = response };
        return languagesResponse;
    }

    
    #endregion
}
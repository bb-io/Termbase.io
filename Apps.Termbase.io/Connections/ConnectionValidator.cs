using Apps.Termbase.io.Constants;
using Apps.Termbase.io.Models.Dto;
using Apps.Termbase.io.RestSharp;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using RestSharp;
using Apps.Termbase.io.Models.Response;

namespace Apps.Termbase.io.Connections;

/// <summary>
/// Validates app credentials that were provided by the user
/// </summary>
public class ConnectionValidator : IConnectionValidator
{
    private static readonly AppRestClient Client = new();
    
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authProviders, CancellationToken cancellationToken)
    {
        var request = new AppRestRequest(ApiEndpoints.Languages, Method.Get, authProviders);

        try
        {
            await Client.ExecuteWithHandling<List<Language>>(request);
            return new()
            {
                IsValid = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                IsValid = false,
                Message = ex.Message
            };
        }
    }
}
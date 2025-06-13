using Apps.Termbase.io.Constants;
using Apps.Termbase.io.Models.Dto;
using Apps.Termbase.io.Api;
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
    private static readonly TermbaseClient Client = new();
    
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authProviders, CancellationToken cancellationToken)
    {
        var request = new TermbaseRequest(new TermbaseRequestParameters()
        {
            Url = Urls.Api + ApiEndpoints.Languages,
            Method = Method.Get
        }, authProviders);

        try
        {
            var result = await Client.ExecuteWithHandling(request);
            return new()
            {
                IsValid = true
            };
        }
        catch (Exception ex)
        {
            var message = $"Connection validation failed: {ex.Message}";
            
            return new()
            {
                IsValid = false,
                Message = ex.Message
            };
        }
    }
}
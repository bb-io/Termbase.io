using Apps.Termbase.io.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.Termbase.io.Connections;

/// <summary>
/// Describes BlackBird's app connection settings
/// </summary>
public class ConnectionDefinition : IConnectionDefinition
{

    /// <summary>
    /// Defines app's connection types
    /// </summary>
    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>
    {
        // OAuth example
        // new()
        // {
        //     Name = "OAuth2",
        //     AuthenticationType = ConnectionAuthenticationType.OAuth2,
        //     ConnectionUsage = ConnectionUsage.Actions,
        //     ConnectionProperties = new List<ConnectionProperty>()
        // },
        
        // API token auth example
        new()
        {
            Name = "Developer API token",
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionUsage = ConnectionUsage.Actions,
            
            // Specifying properties that we will need for authorization of the app
            ConnectionProperties = new List<ConnectionProperty>
            {
                new(CredsNames.Authorization)
                {
                    // Property user-friendly name that will be displayed on the UI
                    DisplayName = "API token",
                    
                    // Setting this flag to true hides token input, replacing each its character with •
                    Sensitive = true,
                    // Description of the connection property,
                    // perhaps with some guidelines on how to find it in the service
                    Description = "You can create API token in your profile settings, on the API tab"
                }
            }
        }
    };


    /// <summary>
    /// Processes credentials after the authorization is done 
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(
        Dictionary<string, string> values)
    {
        // Processing OAuth credentials
        // var accessToken = values.First(v => v.Key == CredsNames.AccessToken);
        // yield return new AuthenticationCredentialsProvider(
        //     AuthenticationCredentialsRequestLocation.Header,
        //     "Authorization",
        //     $"Bearer {accessToken.Value}"
        // );
        
        // Processing API key credentials
        var apiKey = values.First(v => v.Key == CredsNames.Authorization);
        yield return new AuthenticationCredentialsProvider(
            AuthenticationCredentialsRequestLocation.Header,
            apiKey.Key,
            apiKey.Value
        );
    }
}
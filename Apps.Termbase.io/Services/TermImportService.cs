using Apps.Termbase.io.Constants;
using Apps.Termbase.io.Models.Dto;
using Apps.Termbase.io.Models.Request;
using Apps.Termbase.io.RestSharp;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using Dto_File = Apps.Termbase.io.Models.Dto.File;
using File = Apps.Termbase.io.Models.Dto.File;

namespace Apps.Termbase.io.Webhooks.Handlers.Base;

/// <summary>
/// Base handler for parameterless webhooks
/// </summary>
public class TermImportService
{
    private AppRestClient Client { get; }
    
    public TermImportService()
    {
        Client = new();
    }

    /// <summary>
    /// Create TermImport
    /// </summary>
    /// <param name="creds">Credentials</param>
    /// <param name="Name">Name of the termimport</param>
    /// <param name="Description">Description of the termimport</param>
    public async Task<TermImport> CreateTermImport(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        string Name,
        string Description
    )
    {
        var createTermImportRequest = new CreateTermImportRequest
        {
            Name = Name,
            Description = Description,
            Uuid = Guid.NewGuid().ToString(),
            TermImportType = "/api/term_import_types/xliff",
           
        };

        var request = new AppRestRequest(ApiEndpoints.TermImports, Method.Post, creds);
        request.AddJsonBody(createTermImportRequest);

        return await Client.ExecuteWithHandling<TermImport>(request);
    }

    /// <summary>
    /// Upload xliff to termImport
    /// </summary>
    /// <param name="creds">Credentials</param>
    /// <param name="TermImportUuid">Uuid of the termimport</param>
    /// <param name="FileLocalFullPath">Local path of the xliff file</param>
    public Task<Dto_File> UploadFile(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        string TermImportUuid,
        string FileLocalFullPath
    )
    {    
        var request = new AppRestRequest(ApiEndpoints.Files, Method.Post, creds);

        request.AddFile("uploadedFile", FileLocalFullPath);
        request.AddParameter("termImportUuid", TermImportUuid);
        request.AddParameter("uuid", Guid.NewGuid().ToString());

        request.AlwaysMultipartFormData = true;

        return Client.ExecuteWithHandling<Dto_File>(request);
    }


    /// <summary>
    /// Start the termImport on termbase.io
    /// </summary>
    /// <param name="creds">Credentials</param>
    /// <param name="termImportUuid">Uuid of termimport to start</param>
    public Task<TermImport> StartTermImport(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        string termImportUuid)
    {
        var request = new AppRestRequest(ApiEndpoints.TermImports + "/" + termImportUuid + "/start", Method.Patch, creds);
        request.AddParameter("application/merge-patch+json", "{}", ParameterType.RequestBody);
        return Client.ExecuteWithHandling<TermImport>(request);
    }

    /// <summary>
    /// Get TermImport action
    /// </summary>
    /// <param name="creds">Credentials</param>
    /// <param name="termImportUuid">Callback creation data</param>
    public Task<TermImport> GetTermImport(
         IEnumerable<AuthenticationCredentialsProvider> creds,
        string termImportUuid)
    {
        var request = new AppRestRequest(ApiEndpoints.TermImports + "/" + termImportUuid, Method.Get, creds);

        return Client.ExecuteWithHandling<TermImport>(request);
    }

    /// <summary>
    /// Export TermImport as tbx
    /// </summary>
    /// <param name="creds">Credentials</param>
    /// <param name="termbaseUuid">Action parameter with the data for downloading the file</param>
    /// <param name="destinationLocalFilePath">Action parameter with the data for downloading the file</param>

    /// <returns>File data</returns>
    public async Task ExportTermImportAsTbx(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        string termbaseUuid, 
        string destinationLocalFilePath)
    {

        var httpClient = new HttpClient();

        var authHeader = creds.First(x => x.KeyName == CredsNames.Authorization);

        httpClient.DefaultRequestHeaders.Add(authHeader.KeyName, authHeader.Value);

        var apiUrl = Urls.Api + ApiEndpoints.Termbases + "/" + termbaseUuid + "/export_to_tbx";

        try
        {
            using (var response = await httpClient.GetAsync(apiUrl))
            {
                response.EnsureSuccessStatusCode(); // Throws an exception if the HTTP response is not successful

                using (var fileStream = new FileStream(destinationLocalFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await response.Content.CopyToAsync(fileStream);
                }
            }

            Console.WriteLine($"File downloaded successfully to: {destinationLocalFilePath}");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error downloading file: {ex.Message}");
        }

    }
}
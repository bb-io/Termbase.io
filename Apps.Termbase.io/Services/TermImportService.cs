using Apps.Termbase.io.Constants;
using Apps.Termbase.io.Models.Dto;
using Apps.Termbase.io.Models.Request;
using Apps.Termbase.io.Api;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using Dto_File = Apps.Termbase.io.Models.Dto.File;

namespace Apps.Termbase.io.Webhooks.Handlers.Base;

public class TermImportService
{
    private TermbaseClient Client { get; }
    
    public TermImportService()
    {
        Client = new();
    }

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

        var request = new TermbaseRequest(new TermbaseRequestParameters
        {
            Url = Urls.Api + ApiEndpoints.TermImports,
            Method = Method.Post
        }, creds.ToArray())
            .AddJsonBody(createTermImportRequest);

        return await Client.ExecuteWithJson<TermImport>(request);
    }

    public Task<Dto_File> UploadFile(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        string TermImportUuid,
        string FileLocalFullPath
    )
    {    
        var request = new TermbaseRequest(new TermbaseRequestParameters
        {
            Url = Urls.Api + ApiEndpoints.Files,
            Method = Method.Post
        }, creds.ToArray());

        request.AddFile("uploadedFile", FileLocalFullPath);
        request.AddParameter("termImportUuid", TermImportUuid);
        request.AddParameter("uuid", Guid.NewGuid().ToString());

        request.AlwaysMultipartFormData = true;

        return Client.ExecuteWithJson<Dto_File>(request);
    }

    
    public Task<TermImport> StartTermImport(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        string termImportUuid)
    {
        var request = new TermbaseRequest(new TermbaseRequestParameters
        {
            Url = Urls.Api + ApiEndpoints.TermImports + "/" + termImportUuid + "/start",
            Method = Method.Patch
        }, creds)
        .AddParameter("application/merge-patch+json", "{}", ParameterType.RequestBody);
        
        return Client.ExecuteWithJson<TermImport>(request);
    }
    
    public Task<TermImport> GetTermImport(
         IEnumerable<AuthenticationCredentialsProvider> creds,
        string termImportUuid)
    {
        return Client.ExecuteWithJson<TermImport>(ApiEndpoints.TermImports + "/" + termImportUuid, Method.Get, null, creds.ToArray());
    }
    
    public async Task ExportTermImportAsTbx(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        string termbaseUuid, 
        string destinationLocalFilePath)
    {

        var httpClient = new HttpClient();

        var authHeader = creds.First(x => x.KeyName == CredsNames.ApiKey);

        httpClient.DefaultRequestHeaders.Add(authHeader.KeyName, authHeader.Value);

        var apiUrl = Urls.Api + ApiEndpoints.Termbases + "/" + termbaseUuid + "/export_to_tbx";

        try
        {
            using (var response = await httpClient.GetAsync(apiUrl))
            {
                response.EnsureSuccessStatusCode();
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
using Apps.Termbase.io.Api;
using Apps.Termbase.io.Constants;
using Apps.Termbase.io.Models.Dto;
using Apps.Termbase.io.Models.Request;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using RestSharp;
using Dto_File = Apps.Termbase.io.Models.Dto.File;

namespace Apps.Termbase.io.Services;

public class TermbaseTermUpdateWebhookTransformerService
{
    private TermbaseClient Client { get; } = new();

    public async Task<TermImport> CreateTermImport(CreateTermImportActionRequest request, IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var createTermImportRequest = new CreateTermImportRequest
        {
            Name = request.Name,
            Description = request.Description,
            Uuid = Guid.NewGuid().ToString(),
            TermImportType = request.TermbaseType ?? "/api/term_import_types/termbase"
        };

        return await Client.ExecuteWithJson<TermImport>(ApiEndpoints.TermImports, Method.Post, createTermImportRequest,
            creds.ToArray());
    }

    public async Task<Dto_File> UploadFile(CreateTermImportActionRequest createRequest, string termImportUuid, Stream fileStream, IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var bytes = await fileStream.GetByteData();
        var request = new TermbaseRequest(new TermbaseRequestParameters
            {
                Url = Urls.Api + ApiEndpoints.Files,
                Method = Method.Post
            }, creds.ToArray())
            .AddFile("uploadedFile", bytes, createRequest.File.Name, ContentType.Binary)
            .AddParameter("termImportUuid", termImportUuid)
            .AddParameter("uuid", Guid.NewGuid().ToString());

        request.AlwaysMultipartFormData = true;
        return await Client.ExecuteWithJson<Dto_File>(request);
    }

    public Task<TermImport> StartTermImport(string termImportUuid, IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var request = new TermbaseRequest(new TermbaseRequestParameters
            {
                Url = Urls.Api + ApiEndpoints.TermImports + $"/{termImportUuid}/start",
                Method = Method.Patch
            }, creds)
            .AddParameter("application/merge-patch+json", "{}", ParameterType.RequestBody);

        return Client.ExecuteWithJson<TermImport>(request);
    }

    public Task<TermImport> GetTermImport(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        string termImportUuid)
    {
        return Client.ExecuteWithJson<TermImport>(ApiEndpoints.TermImports + "/" + termImportUuid, Method.Get, null,
            creds.ToArray());
    }

    public async Task<Stream> ExportTermImportAsTbx(
        string termbaseUuid,
        string format,
        IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var request = new TermbaseRequest(new TermbaseRequestParameters
        {
            Url = Urls.Api + ApiEndpoints.Termbases + $"/{termbaseUuid}/export_to_{format}",
            Method = Method.Get
        }, creds).AddHeader("Accept", "application/ld+json");
        
        var response = await Client.ExecuteWithHandling(request);
        var memoryStream = new MemoryStream(response.RawBytes!);
        memoryStream.Seek(0, SeekOrigin.Begin);

        return memoryStream;
    }
}
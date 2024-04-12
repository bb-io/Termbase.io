using Apps.Termbase.io.Api;
using Apps.Termbase.io.Constants;
using Apps.Termbase.io.Invocables;
using Apps.Termbase.io.Models.Request;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.Termbase.io.Actions;

public class TermBaseActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : AppInvocable(invocationContext)
{
    [Action("Export termbase as tbx", Description = "Export termbase as tbx")]
    public async Task<FileReference> ExportTermbaseAsTbx([ActionParameter] GetImportTermbaseRequest request)
    {
        string format = "tbx";
        var response = await Client.ExecuteWithHandling(new TermbaseRequest(new TermbaseRequestParameters
        {
            Url = Urls.Api + ApiEndpoints.Termbases + $"/{request.ImportTermbaseUuid}/export_to_{format}",
            Method = Method.Get
        }, Creds));
        
        var memoryStream = new MemoryStream(response.RawBytes!);
        memoryStream.Seek(0, SeekOrigin.Begin);
        
        string contentType = "application/octet-stream";
        return await fileManagementClient.UploadAsync(memoryStream, contentType, $"{request.ImportTermbaseUuid}.tbx");
    }
}
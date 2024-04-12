using Apps.Termbase.io.Api;
using Apps.Termbase.io.Constants;
using Apps.Termbase.io.Invocables;
using Apps.Termbase.io.Models.Request;
using Apps.Termbase.io.Services;
using Apps.Termbase.io.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Glossaries.Utils.Converters;
using RestSharp;

namespace Apps.Termbase.io.Actions;

[ActionList]
public class TermBaseActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : AppInvocable(invocationContext)
{
    [Action("Export termbase", Description = "Export termbase in specified format (by default it will be exported in tbx)")]
    public async Task<FileReference> ExportTermbaseAsTbx([ActionParameter] GetImportTermbaseRequest request)
    {
        string format = request.Format ?? "tbx";
        var response = await Client.ExecuteWithHandling(new TermbaseRequest(new TermbaseRequestParameters
        {
            Url = Urls.Api + ApiEndpoints.Termbases + $"/{request.ImportTermbaseUuid}/export_to_{format}",
            Method = Method.Get
        }, Creds));
        
        Stream memoryStream = new MemoryStream(response.RawBytes!);
        memoryStream.Seek(0, SeekOrigin.Begin);
        
        if(format == "tbx")
        {
            var glossaryExporter = new GlossaryExporter(memoryStream);
            var glossary = glossaryExporter.ExportGlossary();
            
            memoryStream = glossary.ConvertToTbx();
        }
        
        string contentType = "application/octet-stream";
        return await fileManagementClient.UploadAsync(memoryStream, contentType, $"{request.ImportTermbaseUuid}.tbx");
    }
}
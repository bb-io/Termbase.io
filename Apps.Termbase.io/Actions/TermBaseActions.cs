using RestSharp;
using Apps.Termbase.io.Api;
using Apps.Termbase.io.Constants;
using Apps.Termbase.io.Invocables;
using Apps.Termbase.io.Models.Request;
using Apps.Termbase.io.Models.Response;
using Apps.Termbase.io.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Glossaries.Utils.Converters;

namespace Apps.Termbase.io.Actions;

[ActionList]
public class TermBaseActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : AppInvocable(invocationContext)
{
    [Action("Get termbases", Description = "Get all termbases")]
    public async Task<TermBasesResponse> GetTermbases()
    {
        var termbases = await Client.ExecuteWithJson<List<Models.Dto.Termbase>>(
            ApiEndpoints.Termbases, Method.Get, null, Creds.ToArray());

        return new TermBasesResponse { Termbases = termbases };
    }
    
    [Action("Get termbase", Description = "Get termbase by uuid")]
    public async Task<Models.Dto.Termbase> GetTermbase([ActionParameter] GetImportTermbaseRequest request)
    {
        return await Client.ExecuteWithJson<Models.Dto.Termbase>(
            $"{ApiEndpoints.Termbases}/{request.ImportTermbaseUuid}", Method.Get, null, Creds.ToArray());
    }

    [Action("Export termbase",
        Description = "Export termbase in specified format (by default it will be exported in tbx)")]
    public async Task<FileReference> ExportTermbaseAsTbx([ActionParameter] GetImportTermbaseRequest request,
        [ActionParameter] ExportFileRequest exportFileRequest)
    {
        string format = exportFileRequest.Format ?? "tbx";
        var restRequest = new TermbaseRequest(new TermbaseRequestParameters
        {
            Url = Urls.Api + ApiEndpoints.Termbases + $"/{request.ImportTermbaseUuid}/export_to_{format}",
            Method = Method.Get
        }, Creds).AddHeader("Accept", "application/ld+json");
        var response = await Client.ExecuteWithHandling(restRequest);

        Stream memoryStream = new MemoryStream(response.RawBytes!);
        memoryStream.Seek(0, SeekOrigin.Begin);

        if (format == "tbx")
        {
            var glossaryExporter = new GlossaryExporter(memoryStream)
            {
                SkipEmptyTerms = exportFileRequest.SkipEmptyTerms ?? true
            };

            var glossary = glossaryExporter.ExportGlossary();
            memoryStream = glossary.ConvertToTbx();
        }

        var termbase = await GetTermbase(request);

        string fileName = $"{termbase.Name}.{format}";
        string contentType = MimeTypes.GetMimeType(fileName);
        return await fileManagementClient.UploadAsync(memoryStream, contentType, fileName);
    }
}
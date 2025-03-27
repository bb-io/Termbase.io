using Apps.Termbase.io.Constants;
using Apps.Termbase.io.Invocables;
using Apps.Termbase.io.Models.Dto;
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
using TermImportService = Apps.Termbase.io.Services.TermbaseTermUpdateWebhookTransformerService;

namespace Apps.Termbase.io.Actions;

[ActionList]
public class TermImportActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : AppInvocable(invocationContext)
{
    #region Actions
    
    [Action("Create term import", Description = "Create a term import and start it, by default it will be imported in xliff format")]
    public async Task<TermImport> CreateTermImportAction([ActionParameter] CreateTermImportActionRequest createTermImportRequest)
    {
        var termImportService = new TermImportService();
        var termImport = await termImportService.CreateTermImport(createTermImportRequest, Creds);
        
        var fileStream = await fileManagementClient.DownloadAsync(createTermImportRequest.File);
        await termImportService.UploadFile(createTermImportRequest, termImport.Uuid, fileStream, Creds);

        var startedTermImport = await termImportService.StartTermImport(termImport.Uuid, Creds);
        return startedTermImport;
    }
    
    [Action("Get term import", Description = "Get a term import by uuid")]
    public async Task<TermImport> GetTermImport([ActionParameter] GetTermImportRequest getTermImportRequest)
    {
        var termImportService = new TermImportService();
        return await termImportService.GetTermImport(Creds, getTermImportRequest.TermImportUuid);
    }
    
    [Action("Delete term import", Description = "Delete a term import by uuid")]
    public async Task DeleteTermImport([ActionParameter] GetTermImportRequest deleteTermImportRequest)
    {
         await Client.ExecuteWithJson<TermImport>(ApiEndpoints.TermImports + "/" + deleteTermImportRequest.TermImportUuid, Method.Get, null,
            Creds.ToArray());
    }
    
    [Action("Export termbase from term import", Description = "Download termbase from term import in specified format (by default it will be exported in tbx)")]
    public async Task<FileReference> ExportTermImportAsTbx([ActionParameter] GetTermImportRequest request, 
        [ActionParameter] ExportFileRequest exportFileRequest)
    {
        var format = exportFileRequest.Format ?? "tbx";
        
        var termImportService = new TermImportService();
        var monitoredTermImport = await termImportService.GetTermImport(Creds, request.TermImportUuid);

        var termbaseUuid = monitoredTermImport.TermImportTermbase.Uuid;
        var stream = await termImportService.ExportTermImportAsTbx(termbaseUuid, format, Creds);
        
        if(format == "tbx")
        {
            var glossaryExporter = new GlossaryExporter(stream)
            {
                SkipEmptyTerms = exportFileRequest.SkipEmptyTerms ?? true
            };
            
            var glossary = glossaryExporter.ExportGlossary();
            stream = glossary.ConvertToTbx();
        }
        
        string fileName = $"{monitoredTermImport.Name}.{format}";
        string contentType = MimeTypes.GetMimeType(fileName);
        return await fileManagementClient.UploadAsync(stream, contentType, fileName);
    }

    #endregion
}
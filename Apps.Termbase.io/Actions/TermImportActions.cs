using Apps.Termbase.io.Constants;
using Apps.Termbase.io.DataSourceHandlers.StaticDataSourceHandlers;
using Apps.Termbase.io.Invocables;
using Apps.Termbase.io.Models.Dto;
using Apps.Termbase.io.Models.Request;
using Apps.Termbase.io.Services;
using Apps.Termbase.io.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Glossaries.Utils.Converters;
using RestSharp;

namespace Apps.Termbase.io.Actions;

[ActionList]
public class TermImportActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : AppInvocable(invocationContext)
{
    #region Actions
    
    [Action("Create term import", Description = "Creates a term import")]
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
    
    [Action("Export term import", Description = "Download tbx from term import")]
    public async Task<FileReference> ExportTermImportAsTbx([ActionParameter] GetTermImportRequest request,
        [ActionParameter, Display("Format"), StaticDataSource(typeof(ExportFormatStaticDataSourceHandler))] string? format)
    {
        format ??= "tbx";
        var termImportService = new TermImportService();

        var monitoredTermImport = await termImportService.GetTermImport(Creds, request.TermImportUuid);

        var termbaseUuid = monitoredTermImport.TermImportTermbase.Uuid;
        var stream = await termImportService.ExportTermImportAsTbx(termbaseUuid, Creds);
        
        if(format == "tbx")
        {
            var glossaryExporter = new GlossaryExporter(stream);
            var glossary = glossaryExporter.ExportGlossary();
            stream = glossary.ConvertToTbx();
        }
        
        string contentType = "application/octet-stream";
        return await fileManagementClient.UploadAsync(stream, contentType, $"{monitoredTermImport.Name}.tbx");
    }

    #endregion
}
using Apps.Termbase.io.Constants;
using Apps.Termbase.io.Invocables;
using Apps.Termbase.io.Models.Dto;
using Apps.Termbase.io.Models.Request;
using Apps.Termbase.io.Services;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
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
    
    [Action("Export term import as TBX", Description = "Download tbx from term import")]
    public async Task<FileReference> ExportTermImportAsTbx([ActionParameter] GetTermImportRequest request)
    {
        var termImportService = new TermImportService();

        var monitoredTermImport = await termImportService.GetTermImport(Creds, request.TermImportUuid);

        var termbaseUuid = monitoredTermImport.TermImportTermbase.Uuid;
        var stream = await termImportService.ExportTermImportAsTbx(termbaseUuid, Creds);
        
        string contentType = "application/octet-stream";
        return await fileManagementClient.UploadAsync(stream, contentType, $"{monitoredTermImport.Name}.tbx");
    }

    #endregion
}
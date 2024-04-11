
using Apps.Termbase.io.Invocables;
using Apps.Termbase.io.Models.Dto;
using Apps.Termbase.io.Models.Request;
using Apps.Termbase.io.Webhooks.Handlers.Base;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Termbase.io.Actions.CreateTermImportFromXliffActions;

/// <summary>
/// Contains list of actions
/// </summary>
[ActionList]
public class CreateTermImportFromXliffActions : AppInvocable
{
    #region Constructors

    public CreateTermImportFromXliffActions(InvocationContext invocationContext) : base(invocationContext)
    {}


    #endregion

    #region Actions
    
    [Action("Create termImport", Description = "Creates a new termImport")]
    public async Task<TermImport> CreateTermImportAction([ActionParameter] CreateTermImportActionRequest createTermImportActionRequest)
    {
        var termImportService = new TermImportService();
        var termImport = await termImportService.CreateTermImport(Creds, createTermImportActionRequest.Name, createTermImportActionRequest.Description);
        
        var file = await termImportService.UploadFile(Creds,termImport.Uuid, createTermImportActionRequest.FileLocalFullPath);

        var startedTermImport = await termImportService.StartTermImport(Creds, termImport.Uuid);
        return startedTermImport;
    }
    
    [Action("Get termImport", Description = "Get a termImport")]
    public async Task<TermImport> GetTermImport(string termImportUuid)
    {
        var termImportService = new TermImportService();

        return await termImportService.GetTermImport(Creds, termImportUuid);
    }
    
    [Action("Download tbx from termImport", Description = "Download tbx from termImport")]
    public async Task ExportTermImportAsTbx(string termImportUuid, string destinationFilePath)
    {
        var termImportService = new TermImportService();

        var monitoredTermImport = await termImportService.GetTermImport(Creds, termImportUuid);

        var termbaseUuid = monitoredTermImport.TermImportTermbase.Uuid;
        await termImportService.ExportTermImportAsTbx(Creds, termbaseUuid, destinationFilePath);
    }

    #endregion
}
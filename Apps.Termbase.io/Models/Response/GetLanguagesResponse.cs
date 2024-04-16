using Apps.Termbase.io.Models.Dto;
using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io.Models.Response;

public class GetLanguagesResponse
{
    [Display("Languages")]
    public IEnumerable<Language> Languages { get; set; }
}
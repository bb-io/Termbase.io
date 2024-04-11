using Apps.Termbase.io.Models.Dto;

namespace Apps.Termbase.io.Models.Response;

public class GetLanguagesResponse
{
    public IEnumerable<Language> Languages { get; set; }
}
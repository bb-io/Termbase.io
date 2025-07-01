using Newtonsoft.Json.Linq;
namespace Apps.Termbase.io.Services;

public class TermbaseTermEntryFieldService : ITermbaseTermEntryFieldService
{
    public string ModifyTermJson(string json)
    {
        var obj = JObject.Parse(json);

        // Navigate to: termbaseTermEntry.fields.id
        var idToken = obj.SelectToken("termbaseTermEntry.fields[?(@.name == 'id')].value");

        if (idToken != null)
        {
            // Set value to termbaseTermEntry.fieldId
            obj["termbaseTermEntry"]!["IdField"] = idToken;
        }

        return obj.ToString();
    }
}

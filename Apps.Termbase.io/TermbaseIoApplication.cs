using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.Termbase.io;

public class TermbaseIoApplication :  IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => new[] { ApplicationCategory.ArtificialIntelligence };
        set { }
    }
    
    public string Name
    {
        get => "termbase.io";
        set { }
    }
    
    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}
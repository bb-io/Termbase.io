using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io;

public class TermbaseIoApplication :  IApplication
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => new[] { ApplicationCategory.QualityManagement, ApplicationCategory.CatAndTms };
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
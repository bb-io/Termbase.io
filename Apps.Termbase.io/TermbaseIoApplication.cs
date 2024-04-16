using Blackbird.Applications.Sdk.Common;

namespace Apps.Termbase.io;

public class TermbaseIoApplication :  IApplication
{
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
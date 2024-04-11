using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Termbase.io;

public class Application : BaseInvocable,  IApplication
{
    public string Name
    {
        get => "termbase.io";
        set { }
    }
    
    private readonly Dictionary<Type, object> _typesInstances;

    public Application(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public T GetInstance<T>()
    {
        // Logic for OAuth auth
        // if (!_typesInstances.TryGetValue(typeof(T), out var value))
        // {
        //     throw new InvalidOperationException($"Instance of type '{typeof(T)}' not found");
        // }
        //
        // return (T)value;

        throw new NotImplementedException();
    }

    private Dictionary<Type, object> CreateTypesInstances()
    {
        return new Dictionary<Type, object>
        {
           
        };
    }
}
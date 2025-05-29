using Apps.Termbase.io.Connections;
using Apps.Termbase.io.Webhooks.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Termbase.io.Base;

namespace Tests.Termbase.io;

[TestClass]
public class SubscriptionTests : TestBase
{
    const string SubscriptionUrl = "www.example.com";

    [TestMethod]
    public async Task Subscribe_and_unsubscribe()
    {
        var handler = new TermbaseTermUpdatedHandler();
        await handler.SubscribeAsync(InvocationContext.AuthenticationCredentialsProviders, new Dictionary<string, string> { { "payloadUrl", SubscriptionUrl } });

        await handler.UnsubscribeAsync(InvocationContext.AuthenticationCredentialsProviders, new Dictionary<string, string> { { "payloadUrl", SubscriptionUrl } });
    }
}

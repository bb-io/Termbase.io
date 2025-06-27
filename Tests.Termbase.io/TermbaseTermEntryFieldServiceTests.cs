using Apps.Termbase.io.Webhooks;
using Apps.Termbase.io.Webhooks.Models.Payload;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Tests.Termbase.io;

[TestClass]
public class WebhookHandlerTests
{
    [TestMethod]
    public async Task HandlerWebhook_Should_Modify_FieldId_When_TermbaseTermUpdated()
    {
        // Arrange
        var json = @"{
            ""termbaseTermEntry"": {
                ""fields"": [
                    { ""name"": ""id"", ""value"": ""1234"" },
                    { ""name"": ""Note"", ""value"": ""xyz"" },
                    { ""name"": ""other"", ""value"": ""xyz"" }

                ]
            }
        }";


        var webhookRequest = new WebhookRequest
        {
            Body = json
        };

        var webhookList = new WebhookList(); // Uses your existing logic with typeof(T)

        // Use reflection to call private HandlerWebhook<T>
        var method = typeof(WebhookList)
            .GetMethod("HandlerWebhook", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.MakeGenericMethod(typeof(TermbaseTermUpdatedPayload));

        Assert.IsNotNull(method, "HandlerWebhook<T> method not found");

        var task = (Task<WebhookResponse<TermbaseTermUpdatedPayload>>)method.Invoke(webhookList, new object[] { webhookRequest })!;
        var response = await task;


        // Assert
        Assert.IsNotNull(response);
        Assert.IsNotNull(response.Result);
        Assert.AreEqual("1234", response.Result.TermbaseTermEntry.IdField);
    }
}

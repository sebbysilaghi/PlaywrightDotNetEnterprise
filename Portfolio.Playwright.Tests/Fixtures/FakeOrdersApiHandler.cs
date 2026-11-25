using System.Net;
using System.Text;

namespace Portfolio.Playwright.Tests.Fixtures;

/// <summary>
/// A deterministic fake HTTP transport for testing the client-facing API contract.
/// It does not replace integration tests against a running backend.
/// </summary>
internal sealed class FakeOrdersApiHandler : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (request.Method != HttpMethod.Get)
        {
            return Task.FromResult(JsonResponse(HttpStatusCode.MethodNotAllowed,
                """{"error":"Method not allowed"}"""));
        }

        var path = request.RequestUri?.AbsolutePath;
        return Task.FromResult(path switch
        {
            "/api/orders/ORD-1002" => JsonResponse(HttpStatusCode.OK,
                """{"reference":"ORD-1002","status":"Processing"}"""),
            _ => JsonResponse(HttpStatusCode.NotFound,
                """{"error":"Order not found"}""")
        });
    }

    private static HttpResponseMessage JsonResponse(HttpStatusCode status, string json) =>
        new(status)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
}

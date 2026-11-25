using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using NUnit.Framework;
using Portfolio.Playwright.Tests.Fixtures;

namespace Portfolio.Playwright.Tests.Api;

[TestFixture]
public sealed class OrderApiTests
{
    private HttpClient _client = null!;

    [SetUp]
    public void SetUp()
    {
        // Uses an in-memory HTTP handler. No external API or network is required.
        _client = new HttpClient(new FakeOrdersApiHandler())
        {
            BaseAddress = new Uri("https://orders.example.test/")
        };
    }

    [TearDown]
    public void TearDown() => _client.Dispose();

    [Test]
    public async Task GetExistingOrder_Returns200WithExpectedContract()
    {
        using var response = await _client.GetAsync("api/orders/ORD-1002");

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content.Headers.ContentType?.MediaType,
                Is.EqualTo("application/json"));
        });

        var order = await response.Content.ReadFromJsonAsync<OrderResponse>();
        Assert.That(order, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(order!.Reference, Is.EqualTo("ORD-1002"));
            Assert.That(order.Status, Is.EqualTo("Processing"));
        });
    }

    [Test]
    public async Task GetMissingOrder_Returns404WithUsefulError()
    {
        using var response = await _client.GetAsync("api/orders/ORD-9999");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.That(error?.Error, Is.EqualTo("Order not found"));
    }

    private sealed record OrderResponse(
        [property: JsonPropertyName("reference")] string Reference,
        [property: JsonPropertyName("status")] string Status);

    private sealed record ErrorResponse(
        [property: JsonPropertyName("error")] string Error);
}

using Microsoft.Playwright;
using Portfolio.Playwright.Tests.Fixtures;

namespace Portfolio.Playwright.Tests;

[TestFixture]
public sealed class OrderSearchTests : OrderSearchTestBase
{
    [TestCaseSource(typeof(OrderSearchCases), nameof(OrderSearchCases.SearchScenarios))]
    public async Task Search_ReturnsExpectedResults(
        string query,
        int expectedCount,
        string? expectedOrder)
    {
        await Orders.SearchForOrderAsync(query);

        await Expect(Orders.Results.GetByRole(AriaRole.Listitem))
            .ToHaveCountAsync(expectedCount);

        var expectedMessage = expectedCount == 0
            ? "No orders found"
            : $"{expectedCount} order(s) found";
        await Expect(Orders.EmptyMessage).ToHaveTextAsync(expectedMessage);

        if (expectedOrder is not null)
        {
            await Expect(Orders.MatchingOrder(expectedOrder)).ToHaveCountAsync(1);
        }
    }

    [Test]
    public async Task Search_AfterChangingQuery_UpdatesResults()
    {
        await Orders.SearchForOrderAsync("ORD-1002");
        await Expect(Orders.MatchingOrder("ORD-1002")).ToHaveCountAsync(1);

        await Orders.SearchForOrderAsync("ORD-9999");
        await Expect(Orders.Results.GetByRole(AriaRole.Listitem)).ToHaveCountAsync(0);
        await Expect(Orders.EmptyMessage).ToHaveTextAsync("No orders found");
    }
}

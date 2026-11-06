using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Portfolio.Playwright.Tests.Fixtures;
using Portfolio.Playwright.Tests.Pages;

namespace Portfolio.Playwright.Tests;

[TestFixture]
public sealed class OrderSearchTests : PageTest
{
    [TestCaseSource(typeof(OrderSearchCases), nameof(OrderSearchCases.SearchScenarios))]
    public async Task Search_ReturnsExpectedResults(
        string query,
        int expectedCount,
        string? expectedOrder)
    {
        await Page.SetContentAsync(OrderSearchDemo.Html);
        var orders = new OrderSearchPage(Page);

        await orders.SearchForOrderAsync(query);

        await Expect(orders.Results.GetByRole(AriaRole.Listitem))
            .ToHaveCountAsync(expectedCount);

        var expectedMessage = expectedCount == 0
            ? "No orders found"
            : $"{expectedCount} order(s) found";
        await Expect(orders.EmptyMessage).ToHaveTextAsync(expectedMessage);

        if (expectedOrder is not null)
        {
            await Expect(orders.MatchingOrder(expectedOrder)).ToHaveCountAsync(1);
        }
    }
}

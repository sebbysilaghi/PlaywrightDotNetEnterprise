using Microsoft.Playwright.NUnit;
using Portfolio.Playwright.Tests.Fixtures;
using Portfolio.Playwright.Tests.Pages;

namespace Portfolio.Playwright.Tests;

[TestFixture]
public sealed class OrderSearchTests : PageTest
{
    [Test]
    public async Task Search_WithExistingReference_ReturnsMatchingOrder()
    {
        await Page.SetContentAsync(OrderSearchDemo.Html);
        var orders = new OrderSearchPage(Page);

        await orders.SearchForOrderAsync("ORD-1002");

        await Expect(orders.MatchingOrder("ORD-1002")).ToHaveCountAsync(1);
        await Expect(orders.Results.GetByRole(Microsoft.Playwright.AriaRole.Listitem)).ToHaveCountAsync(1);
        await Expect(orders.EmptyMessage).ToHaveTextAsync("1 order(s) found");
    }

    [Test]
    public async Task Search_WithUnknownReference_ShowsEmptyState()
    {
        await Page.SetContentAsync(OrderSearchDemo.Html);
        var orders = new OrderSearchPage(Page);

        await orders.SearchForOrderAsync("ORD-9999");

        await Expect(orders.Results.GetByRole(Microsoft.Playwright.AriaRole.Listitem)).ToHaveCountAsync(0);
        await Expect(orders.EmptyMessage).ToHaveTextAsync("No orders found");
    }
}

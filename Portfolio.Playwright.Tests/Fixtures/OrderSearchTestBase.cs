using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using Portfolio.Playwright.Tests.Pages;

namespace Portfolio.Playwright.Tests.Fixtures;

/// <summary>
/// Creates a fresh order-search page for each test case. NUnit runs this setup
/// after Playwright's PageTest infrastructure has provided the current Page.
/// </summary>
public abstract class OrderSearchTestBase : PageTest
{
    protected OrderSearchPage Orders { get; private set; } = null!;

    [SetUp]
    public async Task SetUpOrderSearchAsync()
    {
        await Page.SetContentAsync(OrderSearchDemo.Html);
        Orders = new OrderSearchPage(Page);
    }
}

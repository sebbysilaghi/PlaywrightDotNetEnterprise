using Microsoft.Playwright;

namespace Portfolio.Playwright.Tests.Pages;

/// <summary>Encapsulates interactions with the controlled order-search demonstration.</summary>
public sealed class OrderSearchPage
{
    private readonly IPage _page;

    public OrderSearchPage(IPage page)
    {
        _page = page ?? throw new ArgumentNullException(nameof(page));
    }

    private ILocator SearchInput => _page.GetByRole(AriaRole.Searchbox, new() { Name = "Order reference" });
    private ILocator SearchButton => _page.GetByRole(AriaRole.Button, new() { Name = "Search", Exact = true });

    public ILocator Results => _page.GetByRole(AriaRole.List, new() { Name = "Search results" });
    public ILocator MatchingOrder(string reference) => Results.GetByRole(AriaRole.Listitem).Filter(new() { HasText = reference });
    public ILocator EmptyMessage => _page.GetByRole(AriaRole.Status);

    public async Task SearchForOrderAsync(string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        await SearchInput.FillAsync(reference);
        await SearchButton.ClickAsync();
    }
}

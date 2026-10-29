using Microsoft.Playwright;

namespace Portfolio.Playwright.Tests.Pages;

public sealed class HomePage
{
    private readonly IPage _page;

    public HomePage(IPage page)
    {
        _page = page ?? throw new ArgumentNullException(nameof(page));
    }

    public ILocator WelcomeHeading => _page.GetByRole(AriaRole.Heading, new()
    {
        Name = "Welcome to QA Demo",
        Exact = true
    });
}

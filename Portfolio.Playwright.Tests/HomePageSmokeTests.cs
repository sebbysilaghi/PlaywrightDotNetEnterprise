using Microsoft.Playwright.NUnit;
using Portfolio.Playwright.Tests.Pages;

namespace Portfolio.Playwright.Tests;

[TestFixture]
public sealed class HomePageSmokeTests : PageTest
{
    [Test]
    public async Task LandingPage_ShowsExpectedHeading()
    {
        // Keep the first smoke test self-contained while we learn the Page Object pattern.
        await Page.SetContentAsync("""
            <!doctype html>
            <html lang="en">
              <head><title>QA Demo</title></head>
              <body><main><h1>Welcome to QA Demo</h1></main></body>
            </html>
            """);

        var homePage = new HomePage(Page);

        await Expect(homePage.WelcomeHeading).ToBeVisibleAsync();
    }
}

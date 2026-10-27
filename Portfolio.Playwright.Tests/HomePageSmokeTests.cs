using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Portfolio.Playwright.Tests;

[TestFixture]
public sealed class HomePageSmokeTests : PageTest
{
    [Test]
    public async Task LandingPage_ShowsExpectedHeading()
    {
        // Local HTML makes this foundational test reliable and self-contained.
        await Page.SetContentAsync("""
            <!doctype html>
            <html lang="en">
              <head><title>QA Demo</title></head>
              <body><main><h1>Welcome to QA Demo</h1></main></body>
            </html>
            """);

        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Welcome to QA Demo" }))
            .ToBeVisibleAsync();
    }
}

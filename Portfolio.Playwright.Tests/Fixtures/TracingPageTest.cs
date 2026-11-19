using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Portfolio.Playwright.Tests.Fixtures;

/// <summary>
/// Records browser actions for each test, saving a trace only when the test fails.
/// No trace is retained for a passing test.
/// </summary>
public abstract class TracingPageTest : PageTest
{
    [SetUp]
    public async Task StartTraceAsync()
    {
        await Context.Tracing.StartAsync(new()
        {
            Title = TestContext.CurrentContext.Test.FullName,
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }

    [TearDown]
    public async Task StopTraceAsync()
    {
        var test = TestContext.CurrentContext;
        var failed = test.Result.Outcome.Status == TestStatus.Failed;

        // The NUnit test ID is unique for parameterized cases and safe as a filename.
        string? tracePath = null;
        if (failed)
        {
            var directory = Path.Combine(test.WorkDirectory, "playwright-traces");
            Directory.CreateDirectory(directory);
            tracePath = Path.Combine(directory, $"trace-{test.Test.ID}.zip");
        }

        await Context.Tracing.StopAsync(new() { Path = tracePath });

        if (tracePath is not null)
        {
            TestContext.Progress.WriteLine($"Failure trace: {tracePath}");
        }
    }
}

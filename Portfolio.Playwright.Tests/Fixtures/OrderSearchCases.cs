using NUnit.Framework;

namespace Portfolio.Playwright.Tests.Fixtures;

/// <summary>Reusable inputs and expected outcomes for the controlled order-search demo.</summary>
internal static class OrderSearchCases
{
    public static IEnumerable<TestCaseData> SearchScenarios()
    {
        yield return Case("ORD-1002", 1, "ORD-1002", "Exact reference");
        yield return Case("ORD-100", 2, null, "Partial reference");
        yield return Case("ord-2001", 1, "ORD-2001", "Case-insensitive reference");
        yield return Case("  ORD-1002  ", 1, "ORD-1002", "Trim surrounding whitespace");
        yield return Case("ORD-9999", 0, null, "Unknown reference");
        yield return Case("XYZ", 0, null, "Unrelated text");
    }

    private static TestCaseData Case(string query, int expectedCount, string? expectedOrder, string name) =>
        new TestCaseData(query, expectedCount, expectedOrder).SetName($"Order search: {name}");
}

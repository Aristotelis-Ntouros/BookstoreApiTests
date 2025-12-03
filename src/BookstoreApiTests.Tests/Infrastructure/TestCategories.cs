using Xunit.Sdk;

namespace BookstoreApiTests.Tests.Infrastructure;

public static class TestCategories
{
    public const string Smoke = "Smoke";
    public const string Regression = "Regression";
    public const string Integration = "Integration";
    public const string Performance = "Performance";
}

public class SmokeTestAttribute : TraitAttribute
{
    public SmokeTestAttribute() : base("Category", TestCategories.Smoke) { }
}

public class RegressionTestAttribute : TraitAttribute
{
    public RegressionTestAttribute() : base("Category", TestCategories.Regression) { }
}

public class PerformanceTestAttribute : TraitAttribute
{
    public PerformanceTestAttribute() : base("Category", TestCategories.Performance) { }
}

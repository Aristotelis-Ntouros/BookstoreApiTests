namespace BookstoreApiTests.Tests.Infrastructure;

public static class TestCategories
{
    public const string Smoke = "Smoke";
    public const string Regression = "Regression";
    public const string Integration = "Integration";
    public const string Performance = "Performance";
}

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class TestCategoryAttribute : Attribute
{
    public string Category { get; }

    public TestCategoryAttribute(string category)
    {
        Category = category;
    }
}

public class SmokeTestAttribute : TestCategoryAttribute
{
    public SmokeTestAttribute() : base(TestCategories.Smoke) { }
}

public class RegressionTestAttribute : TestCategoryAttribute
{
    public RegressionTestAttribute() : base(TestCategories.Regression) { }
}

public class PerformanceTestAttribute : TestCategoryAttribute
{
    public PerformanceTestAttribute() : base(TestCategories.Performance) { }
}

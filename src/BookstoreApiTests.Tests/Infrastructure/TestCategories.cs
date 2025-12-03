using Xunit.Abstractions;
using Xunit.Sdk;

namespace BookstoreApiTests.Tests.Infrastructure;

public static class TestCategories
{
    public const string Smoke = "Smoke";
    public const string Regression = "Regression";
    public const string Integration = "Integration";
    public const string Performance = "Performance";
}

[TraitDiscoverer("BookstoreApiTests.Tests.Infrastructure.SmokeTraitDiscoverer", "BookstoreApiTests.Tests")]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class SmokeTestAttribute : Attribute, ITraitAttribute { }

[TraitDiscoverer("BookstoreApiTests.Tests.Infrastructure.RegressionTraitDiscoverer", "BookstoreApiTests.Tests")]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class RegressionTestAttribute : Attribute, ITraitAttribute { }

[TraitDiscoverer("BookstoreApiTests.Tests.Infrastructure.PerformanceTraitDiscoverer", "BookstoreApiTests.Tests")]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class PerformanceTestAttribute : Attribute, ITraitAttribute { }

public class SmokeTraitDiscoverer : ITraitDiscoverer
{
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", TestCategories.Smoke);
    }
}

public class RegressionTraitDiscoverer : ITraitDiscoverer
{
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", TestCategories.Regression);
    }
}

public class PerformanceTraitDiscoverer : ITraitDiscoverer
{
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", TestCategories.Performance);
    }
}

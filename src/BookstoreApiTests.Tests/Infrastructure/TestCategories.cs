using Xunit.Sdk;

namespace BookstoreApiTests.Tests.Infrastructure;

public static class TestCategories
{
    public const string Smoke = "Smoke";
    public const string Regression = "Regression";
    public const string Integration = "Integration";
    public const string Performance = "Performance";
}

[TraitDiscoverer("BookstoreApiTests.Tests.Infrastructure.CategoryTraitDiscoverer", "BookstoreApiTests.Tests")]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class SmokeTestAttribute : Attribute, ITraitAttribute { }

[TraitDiscoverer("BookstoreApiTests.Tests.Infrastructure.CategoryTraitDiscoverer", "BookstoreApiTests.Tests")]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class RegressionTestAttribute : Attribute, ITraitAttribute { }

[TraitDiscoverer("BookstoreApiTests.Tests.Infrastructure.CategoryTraitDiscoverer", "BookstoreApiTests.Tests")]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class PerformanceTestAttribute : Attribute, ITraitAttribute { }

public class CategoryTraitDiscoverer : ITraitDiscoverer
{
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        var attributeTypeName = traitAttribute.AttributeType.Name;

        if (attributeTypeName == nameof(SmokeTestAttribute))
            yield return new KeyValuePair<string, string>("Category", TestCategories.Smoke);
        else if (attributeTypeName == nameof(RegressionTestAttribute))
            yield return new KeyValuePair<string, string>("Category", TestCategories.Regression);
        else if (attributeTypeName == nameof(PerformanceTestAttribute))
            yield return new KeyValuePair<string, string>("Category", TestCategories.Performance);
    }
}

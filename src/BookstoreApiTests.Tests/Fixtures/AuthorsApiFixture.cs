using BookstoreApiTests.Tests.Clients;

namespace BookstoreApiTests.Tests.Fixtures;

public class AuthorsApiFixture : IDisposable
{
    public AuthorsApiClient Client { get; }

    public AuthorsApiFixture()
    {
        Client = new AuthorsApiClient();
    }

    public void Dispose()
    {
        Client.Dispose();
        GC.SuppressFinalize(this);
    }
}

[CollectionDefinition("AuthorsApi")]
public class AuthorsApiCollection : ICollectionFixture<AuthorsApiFixture>
{
}

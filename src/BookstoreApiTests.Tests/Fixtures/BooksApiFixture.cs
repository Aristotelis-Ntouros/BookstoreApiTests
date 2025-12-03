using BookstoreApiTests.Tests.Clients;

namespace BookstoreApiTests.Tests.Fixtures;

public class BooksApiFixture : IDisposable
{
    public BooksApiClient Client { get; }

    public BooksApiFixture()
    {
        Client = new BooksApiClient();
    }

    public void Dispose()
    {
        Client.Dispose();
        GC.SuppressFinalize(this);
    }
}

[CollectionDefinition("BooksApi")]
public class BooksApiCollection : ICollectionFixture<BooksApiFixture>
{
}

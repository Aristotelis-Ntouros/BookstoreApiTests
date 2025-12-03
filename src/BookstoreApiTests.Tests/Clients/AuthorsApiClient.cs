using RestSharp;
using BookstoreApiTests.Tests.Models;
using BookstoreApiTests.Tests.Infrastructure;

namespace BookstoreApiTests.Tests.Clients;

public class AuthorsApiClient : ApiClientBase
{
    private const string AuthorsEndpoint = "/api/v1/Authors";

    public async Task<RestResponse<List<Author>>> GetAllAuthors()
    {
        var request = new RestRequest(AuthorsEndpoint, Method.Get);
        return await ExecuteWithRetry<List<Author>>(request);
    }

    public async Task<RestResponse<Author>> GetAuthorById(int id)
    {
        var request = new RestRequest($"{AuthorsEndpoint}/{id}", Method.Get);
        return await ExecuteWithRetry<Author>(request);
    }

    public async Task<RestResponse<Author>> CreateAuthor(Author author)
    {
        var request = new RestRequest(AuthorsEndpoint, Method.Post);
        request.AddJsonBody(author);
        return await ExecuteWithRetry<Author>(request);
    }

    public async Task<RestResponse<Author>> UpdateAuthor(int id, Author author)
    {
        var request = new RestRequest($"{AuthorsEndpoint}/{id}", Method.Put);
        request.AddJsonBody(author);
        return await ExecuteWithRetry<Author>(request);
    }

    public async Task<RestResponse> DeleteAuthor(int id)
    {
        var request = new RestRequest($"{AuthorsEndpoint}/{id}", Method.Delete);
        return await ExecuteWithRetry(request);
    }

    public async Task<RestResponse<List<Author>>> GetAuthorsByBookId(int bookId)
    {
        var request = new RestRequest($"/api/v1/Authors/authors/books/{bookId}", Method.Get);
        return await ExecuteWithRetry<List<Author>>(request);
    }
}

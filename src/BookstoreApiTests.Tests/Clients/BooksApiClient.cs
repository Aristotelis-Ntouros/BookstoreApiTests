using RestSharp;
using BookstoreApiTests.Tests.Models;
using BookstoreApiTests.Tests.Infrastructure;

namespace BookstoreApiTests.Tests.Clients;

public class BooksApiClient : ApiClientBase
{
    private const string BooksEndpoint = "/api/v1/Books";

    public async Task<RestResponse<List<Book>>> GetAllBooks()
    {
        var request = new RestRequest(BooksEndpoint, Method.Get);
        return await ExecuteWithRetry<List<Book>>(request);
    }

    public async Task<RestResponse<Book>> GetBookById(int id)
    {
        var request = new RestRequest($"{BooksEndpoint}/{id}", Method.Get);
        return await ExecuteWithRetry<Book>(request);
    }

    public async Task<RestResponse<Book>> CreateBook(Book book)
    {
        var request = new RestRequest(BooksEndpoint, Method.Post);
        request.AddJsonBody(book);
        return await ExecuteWithRetry<Book>(request);
    }

    public async Task<RestResponse<Book>> UpdateBook(int id, Book book)
    {
        var request = new RestRequest($"{BooksEndpoint}/{id}", Method.Put);
        request.AddJsonBody(book);
        return await ExecuteWithRetry<Book>(request);
    }

    public async Task<RestResponse> DeleteBook(int id)
    {
        var request = new RestRequest($"{BooksEndpoint}/{id}", Method.Delete);
        return await ExecuteWithRetry(request);
    }
}

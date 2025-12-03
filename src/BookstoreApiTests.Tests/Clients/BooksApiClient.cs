using System.Net.Http.Json;
using BookstoreApiTests.Tests.Models;

namespace BookstoreApiTests.Tests.Clients;

public class BooksApiClient : ApiClientBase
{
    private const string BooksEndpoint = "/api/v1/Books";

    public async Task<HttpResponseMessage> GetAllBooksResponse()
    {
        return await GetAsync(BooksEndpoint);
    }

    public async Task<List<Book>?> GetAllBooks()
    {
        return await GetAsync<List<Book>>(BooksEndpoint);
    }

    public async Task<HttpResponseMessage> GetBookByIdResponse(int id)
    {
        return await GetAsync($"{BooksEndpoint}/{id}");
    }

    public async Task<Book?> GetBookById(int id)
    {
        return await GetAsync<Book>($"{BooksEndpoint}/{id}");
    }

    public async Task<HttpResponseMessage> CreateBook(Book book)
    {
        return await PostAsync(BooksEndpoint, book);
    }

    public async Task<Book?> CreateBookAndReturn(Book book)
    {
        var response = await PostAsync(BooksEndpoint, book);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Book>(JsonOptions);
    }

    public async Task<HttpResponseMessage> UpdateBook(int id, Book book)
    {
        return await PutAsync($"{BooksEndpoint}/{id}", book);
    }

    public async Task<Book?> UpdateBookAndReturn(int id, Book book)
    {
        var response = await PutAsync($"{BooksEndpoint}/{id}", book);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Book>(JsonOptions);
    }

    public async Task<HttpResponseMessage> DeleteBook(int id)
    {
        return await DeleteAsync($"{BooksEndpoint}/{id}");
    }
}

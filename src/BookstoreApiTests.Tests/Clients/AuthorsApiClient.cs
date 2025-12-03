using System.Net.Http.Json;
using BookstoreApiTests.Tests.Models;

namespace BookstoreApiTests.Tests.Clients;

public class AuthorsApiClient : ApiClientBase
{
    private const string AuthorsEndpoint = "/api/v1/Authors";

    public async Task<HttpResponseMessage> GetAllAuthorsResponse()
    {
        return await GetAsync(AuthorsEndpoint);
    }

    public async Task<List<Author>?> GetAllAuthors()
    {
        return await GetAsync<List<Author>>(AuthorsEndpoint);
    }

    public async Task<HttpResponseMessage> GetAuthorByIdResponse(int id)
    {
        return await GetAsync($"{AuthorsEndpoint}/{id}");
    }

    public async Task<Author?> GetAuthorById(int id)
    {
        return await GetAsync<Author>($"{AuthorsEndpoint}/{id}");
    }

    public async Task<HttpResponseMessage> CreateAuthor(Author author)
    {
        return await PostAsync(AuthorsEndpoint, author);
    }

    public async Task<Author?> CreateAuthorAndReturn(Author author)
    {
        var response = await PostAsync(AuthorsEndpoint, author);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Author>(JsonOptions);
    }

    public async Task<HttpResponseMessage> UpdateAuthor(int id, Author author)
    {
        return await PutAsync($"{AuthorsEndpoint}/{id}", author);
    }

    public async Task<Author?> UpdateAuthorAndReturn(int id, Author author)
    {
        var response = await PutAsync($"{AuthorsEndpoint}/{id}", author);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Author>(JsonOptions);
    }

    public async Task<HttpResponseMessage> DeleteAuthor(int id)
    {
        return await DeleteAsync($"{AuthorsEndpoint}/{id}");
    }

    public async Task<HttpResponseMessage> GetAuthorsByBookIdResponse(int bookId)
    {
        return await GetAsync($"/api/v1/Authors/authors/books/{bookId}");
    }

    public async Task<List<Author>?> GetAuthorsByBookId(int bookId)
    {
        return await GetAsync<List<Author>>($"/api/v1/Authors/authors/books/{bookId}");
    }
}

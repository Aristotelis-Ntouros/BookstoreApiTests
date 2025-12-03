using System.Net;
using Allure.Xunit.Attributes;
using FluentAssertions;
using BookstoreApiTests.Tests.Fixtures;

namespace BookstoreApiTests.Tests.Tests.Books;

[Collection("BooksApi")]
[AllureParentSuite("Books API")]
[AllureSuite("GET /api/v1/Books")]
public class GetAllBooksTests
{
    private readonly BooksApiFixture _fixture;

    public GetAllBooksTests(BooksApiFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    [AllureFeature("Get All Books")]
    [AllureStory("Happy Path")]
    public async Task GetAllBooks_ReturnsOkStatus()
    {
        // Act
        var response = await _fixture.Client.GetAllBooksResponse();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Get All Books")]
    [AllureStory("Happy Path")]
    public async Task GetAllBooks_ReturnsNonEmptyList()
    {
        // Act
        var books = await _fixture.Client.GetAllBooks();

        // Assert
        books.Should().NotBeNull();
        books.Should().NotBeEmpty();
    }

    [Fact]
    [AllureFeature("Get All Books")]
    [AllureStory("Happy Path")]
    public async Task GetAllBooks_ReturnsValidBookStructure()
    {
        // Act
        var books = await _fixture.Client.GetAllBooks();

        // Assert
        books.Should().NotBeNull();
        var firstBook = books!.First();
        firstBook.Id.Should().BeGreaterThan(0);
        firstBook.Title.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [AllureFeature("Get All Books")]
    [AllureStory("Happy Path")]
    public async Task GetAllBooks_ReturnsJsonContentType()
    {
        // Act
        var response = await _fixture.Client.GetAllBooksResponse();

        // Assert
        response.Content.Headers.ContentType?.MediaType.Should().Be("application/json");
    }
}

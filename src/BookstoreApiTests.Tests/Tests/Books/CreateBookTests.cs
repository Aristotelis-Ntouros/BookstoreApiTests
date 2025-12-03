using System.Net;
using System.Net.Http.Json;
using Allure.Xunit.Attributes;
using FluentAssertions;
using BookstoreApiTests.Tests.Fixtures;
using BookstoreApiTests.Tests.Models;

namespace BookstoreApiTests.Tests.Tests.Books;

[Collection("BooksApi")]
[AllureParentSuite("Books API")]
[AllureSuite("POST /api/v1/Books")]
public class CreateBookTests
{
    private readonly BooksApiFixture _fixture;

    public CreateBookTests(BooksApiFixture fixture)
    {
        _fixture = fixture;
    }

    private static Book CreateValidBook() => new()
    {
        Id = 0,
        Title = "Test Book Title",
        Description = "Test Book Description",
        PageCount = 250,
        Excerpt = "This is a test excerpt for the book.",
        PublishDate = DateTime.UtcNow
    };

    [Fact]
    [AllureFeature("Create Book")]
    [AllureStory("Happy Path")]
    public async Task CreateBook_WithValidData_ReturnsOkStatus()
    {
        // Arrange
        var newBook = CreateValidBook();

        // Act
        var response = await _fixture.Client.CreateBook(newBook);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Create Book")]
    [AllureStory("Happy Path")]
    public async Task CreateBook_WithValidData_ReturnsCreatedBook()
    {
        // Arrange
        var newBook = CreateValidBook();
        newBook.Title = "Unique Test Book " + Guid.NewGuid();

        // Act
        var createdBook = await _fixture.Client.CreateBookAndReturn(newBook);

        // Assert
        createdBook.Should().NotBeNull();
        createdBook!.Title.Should().Be(newBook.Title);
        createdBook.Description.Should().Be(newBook.Description);
        createdBook.PageCount.Should().Be(newBook.PageCount);
    }

    [Fact]
    [AllureFeature("Create Book")]
    [AllureStory("Happy Path")]
    public async Task CreateBook_WithMinimalData_ReturnsOkStatus()
    {
        // Arrange
        var minimalBook = new Book
        {
            Id = 0,
            Title = "Minimal Book",
            PageCount = 1,
            PublishDate = DateTime.UtcNow
        };

        // Act
        var response = await _fixture.Client.CreateBook(minimalBook);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Create Book")]
    [AllureStory("Happy Path")]
    public async Task CreateBook_WithLargePageCount_ReturnsOkStatus()
    {
        // Arrange
        var bookWithLargePages = CreateValidBook();
        bookWithLargePages.PageCount = 10000;

        // Act
        var response = await _fixture.Client.CreateBook(bookWithLargePages);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Create Book")]
    [AllureStory("Edge Case")]
    public async Task CreateBook_WithEmptyTitle_ReturnsOkStatus()
    {
        // Arrange - API accepts empty title (FakeRestAPI behavior)
        var bookWithEmptyTitle = CreateValidBook();
        bookWithEmptyTitle.Title = "";

        // Act
        var response = await _fixture.Client.CreateBook(bookWithEmptyTitle);

        // Assert - FakeRestAPI accepts this
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Create Book")]
    [AllureStory("Edge Case")]
    public async Task CreateBook_WithNullTitle_ReturnsOkStatus()
    {
        // Arrange
        var bookWithNullTitle = CreateValidBook();
        bookWithNullTitle.Title = null;

        // Act
        var response = await _fixture.Client.CreateBook(bookWithNullTitle);

        // Assert - FakeRestAPI accepts this
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Create Book")]
    [AllureStory("Edge Case")]
    public async Task CreateBook_WithZeroPageCount_ReturnsOkStatus()
    {
        // Arrange
        var bookWithZeroPages = CreateValidBook();
        bookWithZeroPages.PageCount = 0;

        // Act
        var response = await _fixture.Client.CreateBook(bookWithZeroPages);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Create Book")]
    [AllureStory("Edge Case")]
    public async Task CreateBook_WithNegativePageCount_ReturnsOkStatus()
    {
        // Arrange
        var bookWithNegativePages = CreateValidBook();
        bookWithNegativePages.PageCount = -100;

        // Act
        var response = await _fixture.Client.CreateBook(bookWithNegativePages);

        // Assert - FakeRestAPI accepts negative values
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Create Book")]
    [AllureStory("Edge Case")]
    public async Task CreateBook_WithFuturePublishDate_ReturnsOkStatus()
    {
        // Arrange
        var bookWithFutureDate = CreateValidBook();
        bookWithFutureDate.PublishDate = DateTime.UtcNow.AddYears(10);

        // Act
        var response = await _fixture.Client.CreateBook(bookWithFutureDate);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Create Book")]
    [AllureStory("Edge Case")]
    public async Task CreateBook_WithVeryLongTitle_ReturnsOkStatus()
    {
        // Arrange
        var bookWithLongTitle = CreateValidBook();
        bookWithLongTitle.Title = new string('A', 1000);

        // Act
        var response = await _fixture.Client.CreateBook(bookWithLongTitle);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Create Book")]
    [AllureStory("Edge Case")]
    public async Task CreateBook_WithSpecialCharactersInTitle_ReturnsOkStatus()
    {
        // Arrange
        var bookWithSpecialChars = CreateValidBook();
        bookWithSpecialChars.Title = "Test <Book> & \"Special\" 'Characters' @#$%";

        // Act
        var response = await _fixture.Client.CreateBook(bookWithSpecialChars);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Create Book")]
    [AllureStory("Edge Case")]
    public async Task CreateBook_WithUnicodeCharacters_ReturnsOkStatus()
    {
        // Arrange
        var bookWithUnicode = CreateValidBook();
        bookWithUnicode.Title = "Test Book with Unicode: \u4e2d\u6587 \u0410\u0411\u0412";

        // Act
        var response = await _fixture.Client.CreateBook(bookWithUnicode);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}

using System.Net;
using Allure.Xunit.Attributes;
using FluentAssertions;
using BookstoreApiTests.Tests.Fixtures;
using BookstoreApiTests.Tests.Models;

namespace BookstoreApiTests.Tests.Tests.Books;

[Collection("BooksApi")]
[AllureParentSuite("Books API")]
[AllureSuite("PUT /api/v1/Books/{id}")]
public class UpdateBookTests
{
    private readonly BooksApiFixture _fixture;

    public UpdateBookTests(BooksApiFixture fixture)
    {
        _fixture = fixture;
    }

    private static Book CreateUpdateBook(int id) => new()
    {
        Id = id,
        Title = "Updated Book Title",
        Description = "Updated Book Description",
        PageCount = 300,
        Excerpt = "Updated excerpt content.",
        PublishDate = DateTime.UtcNow
    };

    [Fact]
    [AllureFeature("Update Book")]
    [AllureStory("Happy Path")]
    public async Task UpdateBook_WithValidData_ReturnsOkStatus()
    {
        // Arrange
        var bookId = 1;
        var updatedBook = CreateUpdateBook(bookId);

        // Act
        var response = await _fixture.Client.UpdateBook(bookId, updatedBook);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Update Book")]
    [AllureStory("Happy Path")]
    public async Task UpdateBook_WithValidData_ReturnsUpdatedBook()
    {
        // Arrange
        var bookId = 1;
        var updatedBook = CreateUpdateBook(bookId);
        updatedBook.Title = "Unique Updated Title " + Guid.NewGuid();

        // Act
        var result = await _fixture.Client.UpdateBookAndReturn(bookId, updatedBook);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(bookId);
        result.Title.Should().Be(updatedBook.Title);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(50)]
    [AllureFeature("Update Book")]
    [AllureStory("Happy Path")]
    public async Task UpdateBook_WithVariousValidIds_ReturnsOkStatus(int bookId)
    {
        // Arrange
        var updatedBook = CreateUpdateBook(bookId);

        // Act
        var response = await _fixture.Client.UpdateBook(bookId, updatedBook);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Update Book")]
    [AllureStory("Happy Path")]
    public async Task UpdateBook_WithPartialData_ReturnsOkStatus()
    {
        // Arrange
        var bookId = 1;
        var partialBook = new Book
        {
            Id = bookId,
            Title = "Only Title Updated",
            PublishDate = DateTime.UtcNow
        };

        // Act
        var response = await _fixture.Client.UpdateBook(bookId, partialBook);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Update Book")]
    [AllureStory("Edge Case")]
    public async Task UpdateBook_WithNonExistentId_ReturnsOkStatus()
    {
        // Arrange - FakeRestAPI returns OK even for non-existent IDs
        var nonExistentId = 999999;
        var book = CreateUpdateBook(nonExistentId);

        // Act
        var response = await _fixture.Client.UpdateBook(nonExistentId, book);

        // Assert - FakeRestAPI behavior
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Update Book")]
    [AllureStory("Edge Case")]
    public async Task UpdateBook_WithMismatchedIds_ReturnsOkStatus()
    {
        // Arrange - URL ID and body ID mismatch
        var urlId = 1;
        var book = CreateUpdateBook(999);

        // Act
        var response = await _fixture.Client.UpdateBook(urlId, book);

        // Assert - API accepts mismatched IDs
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Update Book")]
    [AllureStory("Edge Case")]
    public async Task UpdateBook_WithZeroId_ReturnsOkStatus()
    {
        // Arrange
        var zeroId = 0;
        var book = CreateUpdateBook(zeroId);

        // Act
        var response = await _fixture.Client.UpdateBook(zeroId, book);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Update Book")]
    [AllureStory("Edge Case")]
    public async Task UpdateBook_WithNegativeId_ReturnsOkStatus()
    {
        // Arrange
        var negativeId = -1;
        var book = CreateUpdateBook(negativeId);

        // Act
        var response = await _fixture.Client.UpdateBook(negativeId, book);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Update Book")]
    [AllureStory("Edge Case")]
    public async Task UpdateBook_WithEmptyTitle_ReturnsOkStatus()
    {
        // Arrange
        var bookId = 1;
        var book = CreateUpdateBook(bookId);
        book.Title = "";

        // Act
        var response = await _fixture.Client.UpdateBook(bookId, book);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Update Book")]
    [AllureStory("Edge Case")]
    public async Task UpdateBook_WithSpecialCharacters_ReturnsOkStatus()
    {
        // Arrange
        var bookId = 1;
        var book = CreateUpdateBook(bookId);
        book.Title = "<script>alert('xss')</script>";
        book.Description = "Test & \"quotes\" 'apostrophes'";

        // Act
        var response = await _fixture.Client.UpdateBook(bookId, book);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}

using System.Net;
using Allure.Xunit.Attributes;
using FluentAssertions;
using BookstoreApiTests.Tests.Fixtures;

namespace BookstoreApiTests.Tests.Tests.Books;

[Collection("BooksApi")]
[AllureParentSuite("Books API")]
[AllureSuite("GET /api/v1/Books/{id}")]
public class GetBookByIdTests
{
    private readonly BooksApiFixture _fixture;

    public GetBookByIdTests(BooksApiFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    [AllureFeature("Get Book By ID")]
    [AllureStory("Happy Path")]
    public async Task GetBookById_WithValidId_ReturnsOkStatus()
    {
        // Arrange
        var validId = 1;

        // Act
        var response = await _fixture.Client.GetBookByIdResponse(validId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Get Book By ID")]
    [AllureStory("Happy Path")]
    public async Task GetBookById_WithValidId_ReturnsCorrectBook()
    {
        // Arrange
        var validId = 1;

        // Act
        var book = await _fixture.Client.GetBookById(validId);

        // Assert
        book.Should().NotBeNull();
        book!.Id.Should().Be(validId);
    }

    [Fact]
    [AllureFeature("Get Book By ID")]
    [AllureStory("Happy Path")]
    public async Task GetBookById_WithValidId_ReturnsCompleteBookData()
    {
        // Arrange
        var validId = 1;

        // Act
        var book = await _fixture.Client.GetBookById(validId);

        // Assert
        book.Should().NotBeNull();
        book!.Title.Should().NotBeNullOrEmpty();
        book.PageCount.Should().BeGreaterThanOrEqualTo(0);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(100)]
    [AllureFeature("Get Book By ID")]
    [AllureStory("Happy Path")]
    public async Task GetBookById_WithVariousValidIds_ReturnsOkStatus(int bookId)
    {
        // Act
        var response = await _fixture.Client.GetBookByIdResponse(bookId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Get Book By ID")]
    [AllureStory("Edge Case")]
    public async Task GetBookById_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        var nonExistentId = 999999;

        // Act
        var response = await _fixture.Client.GetBookByIdResponse(nonExistentId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    [AllureFeature("Get Book By ID")]
    [AllureStory("Edge Case")]
    public async Task GetBookById_WithZeroId_ReturnsNotFound()
    {
        // Arrange
        var zeroId = 0;

        // Act
        var response = await _fixture.Client.GetBookByIdResponse(zeroId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    [AllureFeature("Get Book By ID")]
    [AllureStory("Edge Case")]
    public async Task GetBookById_WithNegativeId_ReturnsNotFound()
    {
        // Arrange
        var negativeId = -1;

        // Act
        var response = await _fixture.Client.GetBookByIdResponse(negativeId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    [AllureFeature("Get Book By ID")]
    [AllureStory("Edge Case")]
    public async Task GetBookById_WithMaxIntId_ReturnsNotFound()
    {
        // Arrange
        var maxIntId = int.MaxValue;

        // Act
        var response = await _fixture.Client.GetBookByIdResponse(maxIntId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}

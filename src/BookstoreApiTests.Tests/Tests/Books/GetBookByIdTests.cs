using System.Net;
using Allure.Xunit.Attributes;
using FluentAssertions;
using BookstoreApiTests.Tests.Fixtures;
using BookstoreApiTests.Tests.Infrastructure;

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
    [SmokeTest]
    [RegressionTest]
    [AllureFeature("Get Book By ID")]
    [AllureStory("Happy Path")]
    public async Task GetBookById_WithValidId_ReturnsOkStatus()
    {
        // Arrange
        var validId = 1;

        // Act
        var response = await _fixture.Client.GetBookById(validId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [SmokeTest]
    [RegressionTest]
    [AllureFeature("Get Book By ID")]
    [AllureStory("Happy Path")]
    public async Task GetBookById_WithValidId_ReturnsCorrectBook()
    {
        // Arrange
        var validId = 1;

        // Act
        var response = await _fixture.Client.GetBookById(validId);

        // Assert
        response.IsSuccessful.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.Id.Should().Be(validId);
    }

    [Fact]
    [RegressionTest]
    [AllureFeature("Get Book By ID")]
    [AllureStory("Happy Path")]
    public async Task GetBookById_WithValidId_ReturnsCompleteBookData()
    {
        // Arrange
        var validId = 1;

        // Act
        var response = await _fixture.Client.GetBookById(validId);

        // Assert
        response.Data.Should().NotBeNull();
        response.Data!.Title.Should().NotBeNullOrEmpty();
        response.Data.PageCount.Should().BeGreaterThanOrEqualTo(0);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(100)]
    [RegressionTest]
    [AllureFeature("Get Book By ID")]
    [AllureStory("Happy Path")]
    public async Task GetBookById_WithVariousValidIds_ReturnsOkStatus(int bookId)
    {
        // Act
        var response = await _fixture.Client.GetBookById(bookId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [RegressionTest]
    [AllureFeature("Get Book By ID")]
    [AllureStory("Edge Case")]
    public async Task GetBookById_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        var nonExistentId = 999999;

        // Act
        var response = await _fixture.Client.GetBookById(nonExistentId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    [RegressionTest]
    [AllureFeature("Get Book By ID")]
    [AllureStory("Edge Case")]
    public async Task GetBookById_WithZeroId_ReturnsNotFound()
    {
        // Arrange
        var zeroId = 0;

        // Act
        var response = await _fixture.Client.GetBookById(zeroId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    [RegressionTest]
    [AllureFeature("Get Book By ID")]
    [AllureStory("Edge Case")]
    public async Task GetBookById_WithNegativeId_ReturnsNotFound()
    {
        // Arrange
        var negativeId = -1;

        // Act
        var response = await _fixture.Client.GetBookById(negativeId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    [RegressionTest]
    [AllureFeature("Get Book By ID")]
    [AllureStory("Edge Case")]
    public async Task GetBookById_WithMaxIntId_ReturnsNotFound()
    {
        // Arrange
        var maxIntId = int.MaxValue;

        // Act
        var response = await _fixture.Client.GetBookById(maxIntId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}

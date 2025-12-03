using System.Net;
using Allure.Xunit.Attributes;
using FluentAssertions;
using BookstoreApiTests.Tests.Fixtures;

namespace BookstoreApiTests.Tests.Tests.Authors;

[Collection("AuthorsApi")]
[AllureParentSuite("Authors API")]
[AllureSuite("GET /api/v1/Authors/{id}")]
public class GetAuthorByIdTests
{
    private readonly AuthorsApiFixture _fixture;

    public GetAuthorByIdTests(AuthorsApiFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    [AllureFeature("Get Author By ID")]
    [AllureStory("Happy Path")]
    public async Task GetAuthorById_WithValidId_ReturnsOkStatus()
    {
        // Arrange
        var validId = 1;

        // Act
        var response = await _fixture.Client.GetAuthorByIdResponse(validId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Get Author By ID")]
    [AllureStory("Happy Path")]
    public async Task GetAuthorById_WithValidId_ReturnsCorrectAuthor()
    {
        // Arrange
        var validId = 1;

        // Act
        var author = await _fixture.Client.GetAuthorById(validId);

        // Assert
        author.Should().NotBeNull();
        author!.Id.Should().Be(validId);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    [AllureFeature("Get Author By ID")]
    [AllureStory("Happy Path")]
    public async Task GetAuthorById_WithVariousValidIds_ReturnsOkStatus(int authorId)
    {
        // Act
        var response = await _fixture.Client.GetAuthorByIdResponse(authorId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Get Author By ID")]
    [AllureStory("Edge Case")]
    public async Task GetAuthorById_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        var nonExistentId = 999999;

        // Act
        var response = await _fixture.Client.GetAuthorByIdResponse(nonExistentId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    [AllureFeature("Get Author By ID")]
    [AllureStory("Edge Case")]
    public async Task GetAuthorById_WithZeroId_ReturnsNotFound()
    {
        // Arrange
        var zeroId = 0;

        // Act
        var response = await _fixture.Client.GetAuthorByIdResponse(zeroId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    [AllureFeature("Get Author By ID")]
    [AllureStory("Edge Case")]
    public async Task GetAuthorById_WithNegativeId_ReturnsNotFound()
    {
        // Arrange
        var negativeId = -1;

        // Act
        var response = await _fixture.Client.GetAuthorByIdResponse(negativeId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}

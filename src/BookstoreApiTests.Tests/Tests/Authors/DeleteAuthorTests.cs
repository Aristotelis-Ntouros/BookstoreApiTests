using System.Net;
using Allure.Xunit.Attributes;
using FluentAssertions;
using BookstoreApiTests.Tests.Fixtures;

namespace BookstoreApiTests.Tests.Tests.Authors;

[Collection("AuthorsApi")]
[AllureParentSuite("Authors API")]
[AllureSuite("DELETE /api/v1/Authors/{id}")]
public class DeleteAuthorTests
{
    private readonly AuthorsApiFixture _fixture;

    public DeleteAuthorTests(AuthorsApiFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    [AllureFeature("Delete Author")]
    [AllureStory("Happy Path")]
    public async Task DeleteAuthor_WithValidId_ReturnsOkStatus()
    {
        // Arrange
        var validId = 1;

        // Act
        var response = await _fixture.Client.DeleteAuthor(validId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(50)]
    [AllureFeature("Delete Author")]
    [AllureStory("Happy Path")]
    public async Task DeleteAuthor_WithVariousValidIds_ReturnsOkStatus(int authorId)
    {
        // Act
        var response = await _fixture.Client.DeleteAuthor(authorId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Delete Author")]
    [AllureStory("Edge Case")]
    public async Task DeleteAuthor_WithNonExistentId_ReturnsOkStatus()
    {
        // Arrange
        var nonExistentId = 999999;

        // Act
        var response = await _fixture.Client.DeleteAuthor(nonExistentId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Delete Author")]
    [AllureStory("Edge Case")]
    public async Task DeleteAuthor_WithZeroId_ReturnsOkStatus()
    {
        // Arrange
        var zeroId = 0;

        // Act
        var response = await _fixture.Client.DeleteAuthor(zeroId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Delete Author")]
    [AllureStory("Edge Case")]
    public async Task DeleteAuthor_WithNegativeId_ReturnsOkStatus()
    {
        // Arrange
        var negativeId = -1;

        // Act
        var response = await _fixture.Client.DeleteAuthor(negativeId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Delete Author")]
    [AllureStory("Edge Case")]
    public async Task DeleteAuthor_CalledTwiceOnSameId_BothReturnOkStatus()
    {
        // Arrange
        var authorId = 25;

        // Act
        var firstResponse = await _fixture.Client.DeleteAuthor(authorId);
        var secondResponse = await _fixture.Client.DeleteAuthor(authorId);

        // Assert
        firstResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        secondResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}

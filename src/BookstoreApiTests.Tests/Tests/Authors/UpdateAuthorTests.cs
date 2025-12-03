using System.Net;
using Allure.Xunit.Attributes;
using FluentAssertions;
using BookstoreApiTests.Tests.Fixtures;
using BookstoreApiTests.Tests.Models;

namespace BookstoreApiTests.Tests.Tests.Authors;

[Collection("AuthorsApi")]
[AllureParentSuite("Authors API")]
[AllureSuite("PUT /api/v1/Authors/{id}")]
public class UpdateAuthorTests
{
    private readonly AuthorsApiFixture _fixture;

    public UpdateAuthorTests(AuthorsApiFixture fixture)
    {
        _fixture = fixture;
    }

    private static Author CreateUpdateAuthor(int id) => new()
    {
        Id = id,
        IdBook = 1,
        FirstName = "Updated",
        LastName = "Author"
    };

    [Fact]
    [AllureFeature("Update Author")]
    [AllureStory("Happy Path")]
    public async Task UpdateAuthor_WithValidData_ReturnsOkStatus()
    {
        // Arrange
        var authorId = 1;
        var updatedAuthor = CreateUpdateAuthor(authorId);

        // Act
        var response = await _fixture.Client.UpdateAuthor(authorId, updatedAuthor);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Update Author")]
    [AllureStory("Happy Path")]
    public async Task UpdateAuthor_WithValidData_ReturnsUpdatedAuthor()
    {
        // Arrange
        var authorId = 1;
        var updatedAuthor = CreateUpdateAuthor(authorId);
        updatedAuthor.FirstName = "Updated" + Guid.NewGuid().ToString()[..8];

        // Act
        var result = await _fixture.Client.UpdateAuthorAndReturn(authorId, updatedAuthor);

        // Assert
        result.Should().NotBeNull();
        result!.FirstName.Should().Be(updatedAuthor.FirstName);
    }

    [Fact]
    [AllureFeature("Update Author")]
    [AllureStory("Edge Case")]
    public async Task UpdateAuthor_WithNonExistentId_ReturnsOkStatus()
    {
        // Arrange
        var nonExistentId = 999999;
        var author = CreateUpdateAuthor(nonExistentId);

        // Act
        var response = await _fixture.Client.UpdateAuthor(nonExistentId, author);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Update Author")]
    [AllureStory("Edge Case")]
    public async Task UpdateAuthor_WithZeroId_ReturnsOkStatus()
    {
        // Arrange
        var zeroId = 0;
        var author = CreateUpdateAuthor(zeroId);

        // Act
        var response = await _fixture.Client.UpdateAuthor(zeroId, author);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Update Author")]
    [AllureStory("Edge Case")]
    public async Task UpdateAuthor_WithNegativeId_ReturnsOkStatus()
    {
        // Arrange
        var negativeId = -1;
        var author = CreateUpdateAuthor(negativeId);

        // Act
        var response = await _fixture.Client.UpdateAuthor(negativeId, author);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}

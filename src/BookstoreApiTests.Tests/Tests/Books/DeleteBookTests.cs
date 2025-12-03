using System.Net;
using Allure.Xunit.Attributes;
using FluentAssertions;
using BookstoreApiTests.Tests.Fixtures;
using BookstoreApiTests.Tests.Infrastructure;

namespace BookstoreApiTests.Tests.Tests.Books;

[Collection("BooksApi")]
[AllureParentSuite("Books API")]
[AllureSuite("DELETE /api/v1/Books/{id}")]
public class DeleteBookTests
{
    private readonly BooksApiFixture _fixture;

    public DeleteBookTests(BooksApiFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    [SmokeTest]
    [RegressionTest]
    [AllureFeature("Delete Book")]
    [AllureStory("Happy Path")]
    public async Task DeleteBook_WithValidId_ReturnsOkStatus()
    {
        // Arrange
        var validId = 1;

        // Act
        var response = await _fixture.Client.DeleteBook(validId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    [RegressionTest]
    [AllureFeature("Delete Book")]
    [AllureStory("Happy Path")]
    public async Task DeleteBook_WithVariousValidIds_ReturnsOkStatus(int bookId)
    {
        // Act
        var response = await _fixture.Client.DeleteBook(bookId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [RegressionTest]
    [AllureFeature("Delete Book")]
    [AllureStory("Edge Case")]
    public async Task DeleteBook_WithNonExistentId_ReturnsOkStatus()
    {
        // Arrange - FakeRestAPI returns OK for non-existent IDs
        var nonExistentId = 999999;

        // Act
        var response = await _fixture.Client.DeleteBook(nonExistentId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [RegressionTest]
    [AllureFeature("Delete Book")]
    [AllureStory("Edge Case")]
    public async Task DeleteBook_WithZeroId_ReturnsOkStatus()
    {
        // Arrange
        var zeroId = 0;

        // Act
        var response = await _fixture.Client.DeleteBook(zeroId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [RegressionTest]
    [AllureFeature("Delete Book")]
    [AllureStory("Edge Case")]
    public async Task DeleteBook_WithNegativeId_ReturnsOkStatus()
    {
        // Arrange
        var negativeId = -1;

        // Act
        var response = await _fixture.Client.DeleteBook(negativeId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [RegressionTest]
    [AllureFeature("Delete Book")]
    [AllureStory("Edge Case")]
    public async Task DeleteBook_WithMaxIntId_ReturnsOkStatus()
    {
        // Arrange
        var maxIntId = int.MaxValue;

        // Act
        var response = await _fixture.Client.DeleteBook(maxIntId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [RegressionTest]
    [AllureFeature("Delete Book")]
    [AllureStory("Edge Case")]
    public async Task DeleteBook_CalledTwiceOnSameId_BothReturnOkStatus()
    {
        // Arrange - Idempotency test
        var bookId = 50;

        // Act
        var firstResponse = await _fixture.Client.DeleteBook(bookId);
        var secondResponse = await _fixture.Client.DeleteBook(bookId);

        // Assert - Both should return OK (FakeRestAPI is idempotent)
        firstResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        secondResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}

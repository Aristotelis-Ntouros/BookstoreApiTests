using System.Net;
using Allure.Xunit.Attributes;
using FluentAssertions;
using BookstoreApiTests.Tests.Fixtures;
using BookstoreApiTests.Tests.Models;
using BookstoreApiTests.Tests.Infrastructure;

namespace BookstoreApiTests.Tests.Tests.Authors;

[Collection("AuthorsApi")]
[AllureParentSuite("Authors API")]
[AllureSuite("POST /api/v1/Authors")]
public class CreateAuthorTests
{
    private readonly AuthorsApiFixture _fixture;

    public CreateAuthorTests(AuthorsApiFixture fixture)
    {
        _fixture = fixture;
    }

    private static Author CreateValidAuthor() => new()
    {
        Id = 0,
        IdBook = 1,
        FirstName = "telis",
        LastName = "test"
    };

    [Fact]
    [SmokeTest]
    [RegressionTest]
    [AllureFeature("Create Author")]
    [AllureStory("Happy Path")]
    public async Task CreateAuthor_WithValidData_ReturnsOkStatus()
    {
        // Arrange
        var newAuthor = CreateValidAuthor();

        // Act
        var response = await _fixture.Client.CreateAuthor(newAuthor);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [SmokeTest]
    [RegressionTest]
    [AllureFeature("Create Author")]
    [AllureStory("Happy Path")]
    public async Task CreateAuthor_WithValidData_ReturnsCreatedAuthor()
    {
        // Arrange
        var newAuthor = CreateValidAuthor();
        newAuthor.FirstName = "Test" + Guid.NewGuid().ToString()[..8];

        // Act
        var response = await _fixture.Client.CreateAuthor(newAuthor);

        // Assert
        response.IsSuccessful.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.FirstName.Should().Be(newAuthor.FirstName);
        response.Data.LastName.Should().Be(newAuthor.LastName);
    }

    [Fact]
    [RegressionTest]
    [AllureFeature("Create Author")]
    [AllureStory("Edge Case")]
    public async Task CreateAuthor_WithEmptyFirstName_ReturnsOkStatus()
    {
        // Arrange
        var author = CreateValidAuthor();
        author.FirstName = "";

        // Act
        var response = await _fixture.Client.CreateAuthor(author);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [RegressionTest]
    [AllureFeature("Create Author")]
    [AllureStory("Edge Case")]
    public async Task CreateAuthor_WithNullNames_ReturnsOkStatus()
    {
        // Arrange
        var author = new Author
        {
            Id = 0,
            IdBook = 1,
            FirstName = null,
            LastName = null
        };

        // Act
        var response = await _fixture.Client.CreateAuthor(author);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [RegressionTest]
    [AllureFeature("Create Author")]
    [AllureStory("Edge Case")]
    public async Task CreateAuthor_WithSpecialCharacters_ReturnsOkStatus()
    {
        // Arrange
        var author = CreateValidAuthor();
        author.FirstName = "telis@";
        author.LastName = "Ntouros_";

        // Act
        var response = await _fixture.Client.CreateAuthor(author);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [RegressionTest]
    [AllureFeature("Create Author")]
    [AllureStory("Edge Case")]
    public async Task CreateAuthor_WithNonExistentBookId_ReturnsOkStatus()
    {
        // Arrange
        var author = CreateValidAuthor();
        author.IdBook = 999999;

        // Act
        var response = await _fixture.Client.CreateAuthor(author);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}

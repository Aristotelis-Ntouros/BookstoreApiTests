using System.Net;
using Allure.Xunit.Attributes;
using FluentAssertions;
using BookstoreApiTests.Tests.Fixtures;

namespace BookstoreApiTests.Tests.Tests.Authors;

[Collection("AuthorsApi")]
[AllureParentSuite("Authors API")]
[AllureSuite("GET /api/v1/Authors")]
public class GetAllAuthorsTests
{
    private readonly AuthorsApiFixture _fixture;

    public GetAllAuthorsTests(AuthorsApiFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    [AllureFeature("Get All Authors")]
    [AllureStory("Happy Path")]
    public async Task GetAllAuthors_ReturnsOkStatus()
    {
        // Act
        var response = await _fixture.Client.GetAllAuthorsResponse();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [AllureFeature("Get All Authors")]
    [AllureStory("Happy Path")]
    public async Task GetAllAuthors_ReturnsNonEmptyList()
    {
        // Act
        var authors = await _fixture.Client.GetAllAuthors();

        // Assert
        authors.Should().NotBeNull();
        authors.Should().NotBeEmpty();
    }

    [Fact]
    [AllureFeature("Get All Authors")]
    [AllureStory("Happy Path")]
    public async Task GetAllAuthors_ReturnsValidAuthorStructure()
    {
        // Act
        var authors = await _fixture.Client.GetAllAuthors();

        // Assert
        authors.Should().NotBeNull();
        var firstAuthor = authors!.First();
        firstAuthor.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    [AllureFeature("Get All Authors")]
    [AllureStory("Happy Path")]
    public async Task GetAllAuthors_ReturnsJsonContentType()
    {
        // Act
        var response = await _fixture.Client.GetAllAuthorsResponse();

        // Assert
        response.Content.Headers.ContentType?.MediaType.Should().Be("application/json");
    }
}

using System.Net;
using Allure.Xunit.Attributes;
using FluentAssertions;
using BookstoreApiTests.Tests.Fixtures;
using BookstoreApiTests.Tests.Infrastructure;

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
    [SmokeTest]
    [RegressionTest]
    [AllureFeature("Get All Authors")]
    [AllureStory("Happy Path")]
    public async Task GetAllAuthors_ReturnsOkStatus()
    {
        // Act
        var response = await _fixture.Client.GetAllAuthors();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [SmokeTest]
    [RegressionTest]
    [AllureFeature("Get All Authors")]
    [AllureStory("Happy Path")]
    public async Task GetAllAuthors_ReturnsNonEmptyList()
    {
        // Act
        var response = await _fixture.Client.GetAllAuthors();

        // Assert
        response.IsSuccessful.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data.Should().NotBeEmpty();
    }

    [Fact]
    [RegressionTest]
    [AllureFeature("Get All Authors")]
    [AllureStory("Happy Path")]
    public async Task GetAllAuthors_ReturnsValidAuthorStructure()
    {
        // Act
        var response = await _fixture.Client.GetAllAuthors();

        // Assert
        response.Data.Should().NotBeNull();
        var firstAuthor = response.Data!.First();
        firstAuthor.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    [RegressionTest]
    [AllureFeature("Get All Authors")]
    [AllureStory("Happy Path")]
    public async Task GetAllAuthors_ReturnsJsonContentType()
    {
        // Act
        var response = await _fixture.Client.GetAllAuthors();

        // Assert
        response.ContentType.Should().Contain("application/json");
    }
}

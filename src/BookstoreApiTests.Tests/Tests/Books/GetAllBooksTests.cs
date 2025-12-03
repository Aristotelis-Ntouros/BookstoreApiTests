using System.Net;
using Allure.Xunit.Attributes;
using FluentAssertions;
using BookstoreApiTests.Tests.Fixtures;
using BookstoreApiTests.Tests.Infrastructure;

namespace BookstoreApiTests.Tests.Tests.Books;

[Collection("BooksApi")]
[AllureParentSuite("Books API")]
[AllureSuite("GET /api/v1/Books")]
public class GetAllBooksTests
{
    private readonly BooksApiFixture _fixture;

    public GetAllBooksTests(BooksApiFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    [SmokeTest]
    [RegressionTest]
    [AllureFeature("Get All Books")]
    [AllureStory("Happy Path")]
    public async Task GetAllBooks_ReturnsOkStatus()
    {
        // Act
        var response = await _fixture.Client.GetAllBooks();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    [SmokeTest]
    [RegressionTest]
    [AllureFeature("Get All Books")]
    [AllureStory("Happy Path")]
    public async Task GetAllBooks_ReturnsNonEmptyList()
    {
        // Act
        var response = await _fixture.Client.GetAllBooks();

        // Assert
        response.IsSuccessful.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data.Should().NotBeEmpty();
    }

    [Fact]
    [RegressionTest]
    [AllureFeature("Get All Books")]
    [AllureStory("Happy Path")]
    public async Task GetAllBooks_ReturnsValidBookStructure()
    {
        // Act
        var response = await _fixture.Client.GetAllBooks();

        // Assert
        response.Data.Should().NotBeNull();
        var firstBook = response.Data!.First();
        firstBook.Id.Should().BeGreaterThan(0);
        firstBook.Title.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [RegressionTest]
    [AllureFeature("Get All Books")]
    [AllureStory("Happy Path")]
    public async Task GetAllBooks_ReturnsJsonContentType()
    {
        // Act
        var response = await _fixture.Client.GetAllBooks();

        // Assert
        response.ContentType.Should().Contain("application/json");
    }
}

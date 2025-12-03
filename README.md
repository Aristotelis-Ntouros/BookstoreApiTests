# Bookstore API Test Automation Framework

A comprehensive API test automation framework for testing the FakeRestAPI Bookstore endpoints using .NET 8, xUnit, and FluentAssertions.

## Project Overview

This project automates testing of the RESTful API for an online bookstore, covering both Books and Authors API endpoints with happy path and edge case scenarios.

### API Endpoints Covered

**Books API:**
- `GET /api/v1/Books` - Retrieve all books
- `GET /api/v1/Books/{id}` - Retrieve book by ID
- `POST /api/v1/Books` - Create a new book
- `PUT /api/v1/Books/{id}` - Update a book
- `DELETE /api/v1/Books/{id}` - Delete a book

**Authors API (Bonus):**
- `GET /api/v1/Authors` - Retrieve all authors
- `GET /api/v1/Authors/{id}` - Retrieve author by ID
- `POST /api/v1/Authors` - Create a new author
- `PUT /api/v1/Authors/{id}` - Update an author
- `DELETE /api/v1/Authors/{id}` - Delete an author

## Project Structure

```
BookstoreApiTests/
├── .github/
│   └── workflows/
│       └── ci.yml                 # GitHub Actions CI/CD pipeline
├── src/
│   └── BookstoreApiTests.Tests/
│       ├── Clients/               # API client classes
│       │   ├── ApiClientBase.cs   # Base HTTP client
│       │   ├── BooksApiClient.cs  # Books API client
│       │   └── AuthorsApiClient.cs# Authors API client
│       ├── Configuration/         # Configuration management
│       │   ├── ApiSettings.cs
│       │   └── ConfigurationManager.cs
│       ├── Fixtures/              # Test fixtures
│       │   ├── BooksApiFixture.cs
│       │   └── AuthorsApiFixture.cs
│       ├── Models/                # Data models
│       │   ├── Book.cs
│       │   └── Author.cs
│       ├── Tests/                 # Test classes
│       │   ├── Books/
│       │   │   ├── GetAllBooksTests.cs
│       │   │   ├── GetBookByIdTests.cs
│       │   │   ├── CreateBookTests.cs
│       │   │   ├── UpdateBookTests.cs
│       │   │   └── DeleteBookTests.cs
│       │   └── Authors/
│       │       ├── GetAllAuthorsTests.cs
│       │       ├── GetAuthorByIdTests.cs
│       │       ├── CreateAuthorTests.cs
│       │       ├── UpdateAuthorTests.cs
│       │       └── DeleteAuthorTests.cs
│       ├── appsettings.json       # Configuration file
│       ├── allureConfig.json      # Allure reporting config
│       └── BookstoreApiTests.Tests.csproj
├── BookstoreApiTests.sln
└── README.md
```

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Allure Command Line](https://docs.qameta.io/allure/#_installing_a_commandline) (for generating reports locally)

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/BookstoreApiTests.git
cd BookstoreApiTests
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Build the Project

```bash
dotnet build
```

## Running Tests

### Run All Tests

```bash
dotnet test
```

### Run Tests with Detailed Output

```bash
dotnet test --logger "console;verbosity=detailed"
```

### Run Specific Test Category

```bash
# Run only Books API tests
dotnet test --filter "FullyQualifiedName~Books"

# Run only Authors API tests
dotnet test --filter "FullyQualifiedName~Authors"
```

### Run Tests with TRX Report

```bash
dotnet test --logger "trx;LogFileName=test-results.trx" --results-directory ./TestResults
```

## Test Reporting

### Generate Allure Report

1. Run tests to generate Allure results:
```bash
dotnet test
```

2. Generate and open Allure report:
```bash
allure serve ./src/BookstoreApiTests.Tests/bin/Debug/net8.0/allure-results
```

Or generate static report:
```bash
allure generate ./src/BookstoreApiTests.Tests/bin/Debug/net8.0/allure-results -o allure-report
allure open allure-report
```

## CI/CD Pipeline

The project includes a GitHub Actions workflow (`.github/workflows/ci.yml`) that:

1. **Triggers on:**
   - Push to `main`, `master`, or `develop` branches
   - Pull requests to `main` or `master`
   - Manual workflow dispatch

2. **Pipeline Steps:**
   - Checkout code
   - Setup .NET 8.0
   - Restore dependencies
   - Build project
   - Run tests
   - Generate Allure report
   - Upload test artifacts
   - Deploy report to GitHub Pages (on main/master)

### Viewing CI/CD Results

- Test results are available in the GitHub Actions tab
- Allure reports are uploaded as artifacts
- Reports are deployed to GitHub Pages on main branch pushes

## Configuration

### API Settings

Edit `appsettings.json` to configure:

```json
{
  "ApiSettings": {
    "BaseUrl": "https://fakerestapi.azurewebsites.net",
    "TimeoutSeconds": 30
  }
}
```

### Environment Variables

You can override settings using environment variables:
- `ApiSettings__BaseUrl` - API base URL
- `ApiSettings__TimeoutSeconds` - Request timeout

## Test Coverage

### Happy Path Tests
- Successful CRUD operations
- Valid data handling
- Proper response codes and content types

### Edge Case Tests
- Invalid IDs (negative, zero, non-existent, max int)
- Empty/null values
- Special characters and Unicode
- Boundary conditions
- Idempotency checks

## Technologies Used

- **.NET 8.0** - Target framework
- **xUnit** - Test framework
- **FluentAssertions** - Assertion library
- **Allure.Xunit** - Test reporting
- **Microsoft.Extensions.Configuration** - Configuration management
- **GitHub Actions** - CI/CD pipeline

## Best Practices Implemented

- **Clean Architecture** - Separation of concerns with dedicated folders
- **DRY Principle** - Reusable API clients and fixtures
- **SOLID Principles** - Single responsibility, dependency injection
- **Maintainability** - Clear naming conventions, organized structure
- **Scalability** - Easy to add new endpoints and tests
- **Reporting** - Comprehensive test reports with Allure

## License

MIT License

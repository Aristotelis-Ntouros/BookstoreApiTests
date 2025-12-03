# Bookstore API Test Automation Framework

A comprehensive, enterprise-grade API test automation framework for testing the FakeRestAPI Bookstore endpoints using .NET 8, RestSharp, xUnit, and best-in-class libraries.

## 🎯 Project Overview

This project demonstrates senior-level API test automation skills, covering CRUD operations for both Books and Authors API endpoints with comprehensive test coverage including happy paths, edge cases, and error handling.

### API Under Test

**Base URL:** `https://fakerestapi.azurewebsites.net`

**Books API Endpoints:**
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/v1/Books` | Retrieve all books |
| GET | `/api/v1/Books/{id}` | Retrieve book by ID |
| POST | `/api/v1/Books` | Create a new book |
| PUT | `/api/v1/Books/{id}` | Update a book |
| DELETE | `/api/v1/Books/{id}` | Delete a book |

**Authors API Endpoints:**
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/v1/Authors` | Retrieve all authors |
| GET | `/api/v1/Authors/{id}` | Retrieve author by ID |
| POST | `/api/v1/Authors` | Create a new author |
| PUT | `/api/v1/Authors/{id}` | Update an author |
| DELETE | `/api/v1/Authors/{id}` | Delete an author |

---

## 📁 Project Structure

```
BookstoreApiTests/
├── .github/
│   └── workflows/
│       ├── ci.yml                    # Main CI/CD pipeline
│       └── run-tests.yml             # Reusable test workflow
├── src/
│   └── BookstoreApiTests.Tests/
│       ├── Clients/                  # API client classes (RestSharp)
│       │   ├── ApiClientBase.cs      # Base client with retry policy
│       │   ├── BooksApiClient.cs     # Books API operations
│       │   └── AuthorsApiClient.cs   # Authors API operations
│       ├── Configuration/            # Environment configuration
│       │   ├── ApiSettings.cs        # Settings model
│       │   └── ConfigurationManager.cs # Config loader
│       ├── Fixtures/                 # xUnit test fixtures
│       │   ├── BooksApiFixture.cs    # Books test fixture
│       │   └── AuthorsApiFixture.cs  # Authors test fixture
│       ├── Infrastructure/           # Test infrastructure
│       │   ├── TestCategories.cs     # Test categorization (Smoke/Regression)
│       │   └── LoggerConfiguration.cs # Serilog setup
│       ├── Models/                   # Data models
│       │   ├── Book.cs               # Book entity
│       │   └── Author.cs             # Author entity
│       ├── Tests/                    # Test classes
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
│       ├── appsettings.json          # Default configuration
│       ├── appsettings.Development.json  # Dev environment config
│       ├── appsettings.Production.json   # Prod environment config
│       ├── allureConfig.json         # Allure reporting config
│       ├── xunit.runner.json         # xUnit parallel execution config
│       └── BookstoreApiTests.Tests.csproj
├── BookstoreApiTests.sln
└── README.md
```

---

## 🛠️ Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Git](https://git-scm.com/)
- [Allure Command Line](https://docs.qameta.io/allure/#_installing_a_commandline) (optional, for local report generation)

---

## 🚀 Quick Start

### 1. Extract/Setup the Project

```bash
# Extract the project files to a directory
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

### 4. Run All Tests

```bash
dotnet test
```

---

## 🧪 Running Tests

### Run All Tests

```bash
dotnet test
```

### Run with Detailed Output

```bash
dotnet test --logger "console;verbosity=detailed"
```

### Run by Test Category

The framework supports **Smoke** and **Regression** test categories:

```bash
# Run only Smoke tests (quick health check)
dotnet test --filter "Category=Smoke"

# Run only Regression tests (comprehensive)
dotnet test --filter "Category=Regression"

# Run both Smoke and Regression
dotnet test --filter "Category=Smoke|Category=Regression"
```

### Run by API Endpoint

```bash
# Run only Books API tests
dotnet test --filter "FullyQualifiedName~Books"

# Run only Authors API tests
dotnet test --filter "FullyQualifiedName~Authors"

# Run specific test class
dotnet test --filter "FullyQualifiedName~GetAllBooksTests"
```

### Run with TRX Report

```bash
dotnet test --logger "trx;LogFileName=test-results.trx" --results-directory ./TestResults
```

---

## 🌍 Environment Configuration

The framework supports multiple environments through configuration files:

| Environment | Config File | Description |
|-------------|-------------|-------------|
| Development | `appsettings.Development.json` | Default, for local testing |
| Production | `appsettings.Production.json` | Production API settings |

### Switch Environment

Set the `TEST_ENVIRONMENT` environment variable:

```bash
# Windows PowerShell
$env:TEST_ENVIRONMENT="Production"
dotnet test

# Windows CMD
set TEST_ENVIRONMENT=Production
dotnet test

# Linux/macOS
TEST_ENVIRONMENT=Production dotnet test
```

### Configuration Settings

```json
{
  "ApiSettings": {
    "BaseUrl": "https://fakerestapi.azurewebsites.net",
    "TimeoutSeconds": 30
  },
  "RetryPolicy": {
    "MaxRetries": 3,
    "BaseDelaySeconds": 1
  },
  "Logging": {
    "MinimumLevel": "Information"
  }
}
```

---

## 📊 Test Reporting

### Allure Reports

#### Generate and View Report (Local)

```bash
# 1. Run tests
dotnet test

# 2. Serve report (opens browser)
allure serve ./src/BookstoreApiTests.Tests/bin/Debug/net8.0/allure-results

# Or generate static report
allure generate ./src/BookstoreApiTests.Tests/bin/Debug/net8.0/allure-results -o allure-report
allure open allure-report
```

### CI/CD Reports

- **GitHub Actions:** Test results are visible in the Actions tab
- **Allure Reports:** Automatically generated and uploaded as artifacts
- **GitHub Pages:** Reports deployed on main branch pushes

---

## 🔄 CI/CD Pipeline

The project includes a comprehensive GitHub Actions workflow with **reusable workflow architecture** for maintainability and DRY principles:

### Workflow Architecture

```
ci.yml (Main Workflow)
├── build job
├── calls → run-tests.yml (Reusable Workflow)
│   ├── test execution
│   ├── Allure report generation
│   └── test artifacts upload
└── deploy job (GitHub Pages)
```

The test execution logic is extracted into a **reusable workflow** (`run-tests.yml`) that can be called from multiple pipelines with different parameters, enabling:
- Consistent test execution across different workflows
- Easy customization via input parameters
- Reduced code duplication
- Simplified maintenance

### Pipeline Triggers

| Trigger | Branches | Description |
|---------|----------|-------------|
| Push | `main`, `master`, `develop` | Automatic on code push |
| Pull Request | `main`, `master` | On PR creation/update |
| Manual | Any | Workflow dispatch with options |

### Manual Run Options

When triggering manually, you can configure:

| Option | Values | Default | Description |
|--------|--------|---------|-------------|
| .NET Version | `8.0.x`, `7.0.x`, `6.0.x` | `8.0.x` | SDK version |
| Configuration | `Debug`, `Release` | `Release` | Build config |
| Environment | `Development`, `Production` | `Development` | Target env |
| Test Filter | `all`, `smoke`, `regression` | `all` | Test category |
| Generate Report | `true`, `false` | `true` | Allure report |

### Pipeline Steps

1. **Build Job:**
   - Checkout code
   - Setup .NET SDK
   - Cache NuGet packages
   - Restore dependencies
   - Build project
   - Upload build artifacts

2. **Test Job:**
   - Download build artifacts
   - Run tests with selected filter
   - Generate test results (TRX)
   - Generate Allure report
   - Upload test artifacts
   - Publish test summary

3. **Deploy Job (on main/master):**
   - Deploy Allure report to GitHub Pages

---

## 🧰 Technologies & Libraries

| Technology | Purpose |
|------------|---------|
| **.NET 8.0** | Target framework |
| **RestSharp** | HTTP client for API calls |
| **xUnit** | Test framework |
| **FluentAssertions** | Fluent assertion library |
| **Polly** | Retry policies & resilience |
| **Serilog** | Structured logging |
| **Allure.Xunit** | Test reporting |
| **Microsoft.Extensions.Configuration** | Configuration management |
| **GitHub Actions** | CI/CD pipeline |

---

## 🏗️ Architecture & Patterns

### API Client Pattern

```csharp
// Base client with retry policy and logging
public class ApiClientBase
{
    protected readonly RestClient _client;
    protected readonly IAsyncPolicy<RestResponse> _retryPolicy;
    protected readonly ILogger _logger;

    protected async Task<RestResponse<T>> ExecuteWithRetry<T>(RestRequest request)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
            await _client.ExecuteAsync<T>(request));
    }
}
```

### Test Categories

Tests are categorized using custom xUnit traits:

```csharp
[Fact]
[SmokeTest]        // Quick health checks
[RegressionTest]   // Comprehensive testing
public async Task GetAllBooks_ReturnsOkStatus()
{
    // Test implementation
}
```

### Retry Policy

Automatic retry with exponential backoff for transient failures:

```csharp
// Retries: 1s → 2s → 4s (exponential backoff)
Policy<RestResponse>
    .HandleResult(r => IsTransientError(r))
    .WaitAndRetryAsync(3, retryAttempt =>
        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt - 1)));
```

---

## 📋 Test Coverage

### Test Categories

| Category | Purpose | When to Run |
|----------|---------|-------------|
| **Smoke** | Quick API health check | Every deployment |
| **Regression** | Full test coverage | Scheduled/Release |

### Test Types

| Type | Description | Example |
|------|-------------|---------|
| Happy Path | Valid inputs, expected success | Get existing book |
| Edge Case | Boundary conditions | Max int ID, empty values |
| Error Handling | Invalid inputs | Non-existent ID, negative values |
| Data Validation | Response structure | Required fields present |

### Coverage Summary

| Endpoint | Happy Path | Edge Cases | Total Tests |
|----------|------------|------------|-------------|
| GET /Books | ✅ 4 | ✅ 1 | 5 |
| GET /Books/{id} | ✅ 4 | ✅ 4 | 8 |
| POST /Books | ✅ 2 | ✅ 6 | 8 |
| PUT /Books/{id} | ✅ 4 | ✅ 6 | 10 |
| DELETE /Books/{id} | ✅ 2 | ✅ 6 | 8 |
| Authors API | ✅ Similar coverage | ✅ | ~35 |
| **Total** | | | **~75 tests** |

---

## ✅ Best Practices Implemented

- **Clean Architecture** - Clear separation of concerns
- **DRY Principle** - Reusable clients, fixtures, and utilities
- **SOLID Principles** - Single responsibility, dependency injection
- **Resilience** - Retry policies for transient failures
- **Observability** - Structured logging with Serilog
- **Parallel Execution** - Tests run in parallel for speed
- **Environment Isolation** - Separate configs per environment
- **Comprehensive Reporting** - Allure reports with rich metadata
- **CI/CD Integration** - Automated testing and deployment

---

## 🐛 Troubleshooting

### Tests Failing with Timeout

Increase timeout in `appsettings.json`:
```json
{
  "ApiSettings": {
    "TimeoutSeconds": 60
  }
}
```

### Allure Report Not Generating

Ensure Allure CLI is installed:
```bash
# macOS
brew install allure

# Windows (Scoop)
scoop install allure

# Linux
sudo apt-get install allure
```

### Environment Variable Not Working

Ensure the variable is set before running tests:
```powershell
# PowerShell - verify
$env:TEST_ENVIRONMENT
```


# Yld.GamingApi.WebApi

This is a RESTful API for gaming-related functionality.

## Table of Contents
- [Installation](#installation)
- [Usage](#usage)
- [Error Handling](#error-handling)
- [NuGet Packages](#nuget-packages)
- [Scripts](#scripts)
- [Unit Tests](#unit-tests)
- [Specifications](#specifications)

## Installation

1. Clone the repository: `git clone https://github.com/your/repository.git`

2. Build the solution using your preferred development environment.

## Usage

1. Start the API by running the GamingApi.WebApi project.

2. The API exposes the following endpoint:
- `GET /api/games`: Retrieves games with optional query parameters.

  Query Parameters:
  - `offset` (optional): The starting index of the games to retrieve.
  - `limit` (optional): The maximum number of games to retrieve.

  Example: `/api/games?offset=0&limit=10`

3. Use appropriate HTTP clients (e.g., Postman, cURL) to make requests to the API endpoints. (Make sure `User-Agent` header is defined when performing requests)


## Error Handling

To ensure proper error handling, an `ExceptionFormatterMiddleware` has been added as a middleware component. This middleware catches any unhandled exceptions and formats the response with relevant error details before returning it to the user.

## NuGet Packages

The following NuGet packages are used in this project:

- FluentValidations: Provides a fluent interface for validating request models.
- Scrutor: Enables assembly scanning and composition in .NET Core.
- MediatR: Implements the Mediator pattern for decoupling request/response logic.
- Mapperly: A lightweight object-to-object mapper.

## Scripts

The following scripts are available:

- `build-and-publish.sh` (or `build-and-publish.bat`): Builds the tests and publishes the solution.
- `containerize.sh` (or `containerize.bat`): Containerizes the application inside a Docker image.
- `run-container.sh` (or `run-container.bat`): Runs the containerized solution.

## Unit Tests

Unit tests are implemented in .xUnit projects to verify the functionality of the API. You can run the tests using your preferred testing framework or IDE.

## Specifications

Specifications for the API functionality are written in .specflow projects. SpecFlow is a framework for writing executable specifications using Gherkin syntax. These specifications define the expected behavior of the API endpoints and can be used for both automated testing and as living documentation.



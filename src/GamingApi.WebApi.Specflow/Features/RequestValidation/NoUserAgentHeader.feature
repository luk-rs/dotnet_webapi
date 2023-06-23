Feature: Validating absence of User-Agent header in requests to api/games endpoint

Scenario: User-Agent header missing from request
    Given the API is available
    And User-Agent header is not configured as a default request header
    When a GET request is made to "/api/games" with the following parameters
        | limit | offset |
        | 2     | 0      |
    Then the API should respond with the following status codes
        | status code |
        | 400         |

Feature: Validating query parameters boundaries on the api/games endpoint

Scenario: Invalid limit and offsets values
    Given the API is available
    When a GET request is made to "/api/games" with the following parameters
        | limit | offset |
        | -1    | 0      |
        | 9     | -1     |
        | 11    | 2      |
        | -11   | -2     |
    Then the API should respond with the following status codes
        | status code |
        | 400         |
        | 400         |
        | 400         |
        | 400         |

Feature: Validating input errors on the api/v1/games endpoint


Scenario: Unacceptable limit and offsets values
    Given the API is available
    When a GET request is made to "/api/games" with the following parameters
        | limit  | offset |
        | 0      | xpt0   |
        | r4nd0m | 0      |
        | r4nd0m | xpt0   |
    Then the API should respond with the following status codes
        | status code |
        | 400         |
        | 400         |
        | 400         |

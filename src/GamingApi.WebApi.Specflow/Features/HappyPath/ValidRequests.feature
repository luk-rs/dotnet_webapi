Feature: Request should execute properly given valid inputs

Scenario: Valid Inputs return valid responses
    Given the API is available
    When a GET request is made to "/api/games" with the following parameters
        | limit | offset |
        | 2     | 0      |
        | 10    | 2      |
        | 10    | 500    |
    Then the API should respond with the following status codes
        | status code |
        | 200         |
        | 200         |
        | 200         |
    And the responses contain valid game dtos

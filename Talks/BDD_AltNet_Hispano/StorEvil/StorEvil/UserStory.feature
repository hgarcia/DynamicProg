Feature: Adding two numbers
	As an accountant
	I want to use a calculator
	So I don't make mistakes

Scenario Outline: Add two positive numbers
    Given I enter the number <first>
    And enter the number <second>
    When Add is been call
    Then the result should be <expected>

    Examples:
	|first|second|expected|
	| 1   | 2    | 3      |
	| 4   | 6    | 10     |
	| 2   | 2    | 4      |
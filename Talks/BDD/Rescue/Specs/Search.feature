Feature: Search
	In order to find my pets
	As an Adopter
	I want to be able to search the database

Scenario: Search by breed
	Given I have selected a breed
	When I press "Search"
	Then I should see only pets of that breed
	
Scenario: Search by status
	Given I have selected a status
	When I press "Search"
	Then I should see only pets on that status
	
Scenario: Search by gender
	Given I have selected a gender
	When I press "Search"
	Then I should see only pets of that gender
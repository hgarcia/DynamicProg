Feature: Publish
	In order to get a new home for pets
	As a Publisher
	I want to be able to publish and edit pets

Scenario: Browse existing pets
    Given I published the following pets
    When I click the "Publish" menu item
    Then I should see a list of those pets
    
Scenario: Browse with no pets published
    Given I have not published pets
    When I click the "Publish" menu item
    Then I should see an empty table
    
Scenario: Add a new pet
	Given I have entered all the information for a pet like this
	When I press Publish
	Then I should see the pet in the list

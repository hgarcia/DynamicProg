Feature: Publish
	In order to get a new home for pets
	As a Rescuer
	I want to be able to publish and edit pets
    
Scenario: Add a new pet
	Given I have entered all the information for a pet
	When I save the pet
	Then I should see the pet in the list
	
Scenario: Browse existing pets
    Given I published some pets
    When I click the "Publish" menu item
    Then I should see a list of those pets
    
Scenario: Browse with no pets published
    Given I have not published pets
    When I click the "Publish" menu item
    Then I should see an empty table
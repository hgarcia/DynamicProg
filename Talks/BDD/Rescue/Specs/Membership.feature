Feature: Membership
	In order to be able to publish lost pets
	As a Publisher
	I want to be able to sign in

Scenario: Sign in
	Given I have entered "hernan" as my username
	And I have entered "112233" as my password
	When I press the button "Log On"
	Then I should see a label that says "Welcome hernan!"

Scenario: Log off
    Given I am logged in with the username "hernan" 
    And the password "112233"
    When I press the link with text "Log Off"
    Then I should see a link with the text "Log On"
    
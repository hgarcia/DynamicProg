#language: es
Característica: Publish
	In order to get a new home for pets
	As a Rescuer
	I want to be able to publish and edit pets
    
Escenario: Add a new pet
	Dado I have entered all the information for a pet
	Cuando I save the pet
	Entonces I should see the pet in the list
	
Escenario: Browse existing pets
    Dado I published some pets
    Cuando I click the "Publish" menu item
    Entonces I should see a list of those pets
    
Escenario: Browse with no pets published
    Dado I have not published pets
    Cuando I click the "Publish" menu item
    Entonces I should see an empty table
Feature: DogAdvertInformationPage
	As a user who is looking for a pet dog
	I want to view full details of an advertised dog
	in order to help me decide if that dog is right for me

@ignore
Scenario: The dog profile advertisement page show full advert details
	When I visit a dog advertisement profile
	Then I should be presented with the dog's name and vital statistics
	And I should be presented with information about the breed
	And I should be presented with information and warnings relevant to buying a dog


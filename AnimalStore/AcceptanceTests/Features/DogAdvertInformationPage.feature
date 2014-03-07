Feature: DogAdvertInformationPage
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@ignore
Scenario: The dog profile advertisement page show full advert details
	When I visit a dog advertisement profile
	Then I should be presented with the dog's name and vital statistics
	And I should be presented with information about the breed
	And I should be presented with information and warnings relevant to buying a dog


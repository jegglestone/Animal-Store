Feature: DogAdvertInformationPage
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: The dog profile advertisement page show full advert details
	When I visit a dog advertisement profile
	Then I should be presented with the dog's name and vital statistics
	And I should be presented with information about the breed
	And I should be presented with information and warnings relevant to buying a dog

Scenario: The API allows searching for dogs of a specific breed
	When I make a GET request to the dogs API with the breedID
	Then I should be presented with JSON results relevant to the breed
	And there should be other dogs in the same category in the results

Scenario: The API allows searching for dogs of a specific breed within a specific place
	When I make a GET request to the dogs API with a breedID and a placeId
	Then I should be presented with JSON results relevant to the breed and filtered by place
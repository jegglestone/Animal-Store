﻿Feature: DogsAPI
	In order to consume dog data from the API
	As a consumer interested in that dogs data
	I want to get a list of available dogs and optionally filter them on breed and location

Scenario: Searching for dogs of a specific breed
	When I make a GET request to the dogs API with the breedID
	Then I should be presented with JSON results relevant to the breed
	And there should be other dogs in the same category in the results

Scenario Outline: Searching for dogs of a specific breed within a specific place
	When I make a GET request to the dogs API with a breedID and a <placeId>
	Then I should be presented with a JSON response relevant to the breed and filtered by <placeId>
	Examples: 
	| Place       | placeId    |
	| Ab kettleby | 1     |
	| Leeds       | 12472 |

Scenario: No results found
	When there are no matching results in the API
	Then the response is a status code 404

Scenario: Limiting results by page size
	When I make a GET request to the dogs API with a breedID and a placeId with a small pagesize
	And I should be able to navigate the results through using the paging links


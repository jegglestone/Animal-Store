Feature: SearchWidget
	As a pet buyer
	I want to be able to search for a pet
	In order to find one that is right for me

Background: 
	Given I am on the 'Home' page

Scenario: Nationwide search for any kind of dog presents search results
	When I have selected any breed of dog in the UK 
	And I press the search button
	Then I should be presented with a list of dog search results
	And the search description should say 'Search results 1 to 5 out of 47 results for all breeds nationwide.'

Scenario Outline: Nationwide search for specific kind of dog presents search results
	When I have selected a <breed> anywhere in the UK 
	And I press the search button
	And there is less than five matching <breed>
	Then I should be presented with search results for the <breed>
	And some other dogs in the <same category>
	And the search description should say 'Showing results 1 to 5 out of <expectedResultsCount> results for <breed> nationwide'

	Examples: 

	| breed   | same category | expectedResultsCount |
	| Bulldog | Dalmatian     | 7                    |

Scenario Outline: Local search for a specific kind of dog presents local results
	When I have selected a <breed> within a <location>
	And I press the search button
	Then I should be presented with search results for that <breed> within the area of <location>

	Examples: 

	| breed   | location | 
	| Bulldog | Leeds    | 
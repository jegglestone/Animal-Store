Feature: SearchWidget
	As a pet buyer
	I want to be able to search for a pet

Background: 
	Given I am on the 'Home' page

Scenario: Nationwide search for any kind of god
	When I have selected any breed of dog in the UK 
	And I press the search button
	Then I should be presented with a list of dog search results

Scenario Outline: Nationwide search for specific kind of god
	When I have selected a <breed> anywhere in the UK 
	And I press the search button
	Then I should be presented with search results for the <breed>
	And some other dogs in the <same category>

	Examples: 

	| breed     | same category |
	| Dalmatian | Bulldog		|

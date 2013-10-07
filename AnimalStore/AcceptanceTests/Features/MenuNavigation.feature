Feature: MenuNavigation
	In order to allow user navigation of the Website
	As a user
	I want to be able to get to the page that I want

Background: 
	Given I am on the 'Home' page

Scenario Outline: The main menu navigates to different hub pages
	When I select the '<MenuItem>' option from the main menu
	Then I am taken to the '<Page>' page
Examples:
	| MenuItem | Page  |
	| Home     | Home  |
	| About    | About |
	| Contact  | Contact  |

Scenario Outline: The top sub menu navigates to different hub pages
	When I select the '<MenuItem>' option from the sub menu
	Then I am taken to the '<Page>' page
Examples:
	| MenuItem | Page  |
	| Home     | Home  |
	| About    | About |
	| Contact  | Contact |
Feature: Demo

@mytag
Scenario: Add two numbers
	Given I have entered 50 into the calculator
	And I have entered 70 into the calculator
	And I have entered Dmitry into the my name textbox
	When I press add 
	Then the result should be 120 on the screen

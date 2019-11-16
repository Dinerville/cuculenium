Feature: Demo

@mytag
Scenario: Add two numbers
	Given I go to post in tut site
	When I navigate to url 'https://tut.by'
	When I click element on 'mainPage' page named 'postButton' via browser

Feature: DesiredSteps

@mytag
Scenario: Add two numbers
	Then 'mainPage's postButton' text is '<text>'. if not say 'the text is wrong'
	Then 'mainPage's postButton' input value is '<text>'. if not say 'the text is wrong'
	Then 'mainPage's postButton' checkbox value is '<true>'. If not say 'the text is wrong'

	When I click 'mainPage's postButton'
	When I click invisible 'mainPage's postButton' #via js
	When I click center of coordinates of 'mainPage's postButton' #via action

	When I double click 'mainPage's postButton'
	When I double click invisible 'mainPage's postButton' #via js
	When I double click center of coordinates of 'mainPage's postButton' #via action

	When I set '<text>' for 'mainPage's postInput' input
	When I set '<true>' for 'mainPage's postCheckbox' checkbox 
	
	When I save value of input 'mainPage's postButton' to '<inputValue>' 
	When I save text of 'mainPage's postButton' to '<labelValue>'
	
	When I save '<inputValue>' as global variable named '<globalName>'

	When I refresh the page
	When I navigate to url '<url>'
	When I scroll to 'mainPage's postButton'
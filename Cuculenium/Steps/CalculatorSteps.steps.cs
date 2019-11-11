using TechTalk.SpecFlow;
namespace Framework
{
  [Binding]
  public sealed class CalculatorSteps : Steps
  {

		[Given ("I have entered (.*) into the calculator")]
		
		public void IHaveEnteredIntoTheCalculator(string aNumber)
		{
			
			When($"I click element on mainPage with name numberButton and with parameters {aNumber}");
		}


		[Given("I have entered (.*) into the my name textbox")]
		
		public void IHaveEnteredIntoTheMyNameTextbox(string aName)
		{
			
			When($"I set text {aName} for element on mainPage with name nameTextBox");
		}


		[When("I press add")]
		[Given("I press add")]
		
		public void IPressAdd()
		{
			
			When($"I click element on mainPage with name addButton");
		}


		[Then("the result should be (.*) on the screen")]
		
		public void TheResultShouldBeOnTheScreen(string expectedResult)
		{
			When($"I get text of element on mainPage with name resultLabel as <@setLocal(actualResult)>");
			Then($"I assert that {expectedResult} equals to <@getLocal(actualResult)>. If not say 'Expected result is not equal to actual'");
		}

  }
}
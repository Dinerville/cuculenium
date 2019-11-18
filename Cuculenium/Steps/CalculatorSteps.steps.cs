using TechTalk.SpecFlow;
namespace Framework
{
  [Binding]
  public sealed class CalculatorSteps : Steps
  {

		[Given ("I go to main tut by page")]
		
		public void IGoToMainTutByPage()
		{
			
			When($"I navigate to url 'https://tut.by'");
		}


		[Then("I assert text is '(.*)' and click post")]
		
		public void IAssertTextIsAndClickPost(string text)
		{
			
			Then($"text of element on 'mainPage' page named 'postButton' is '{text}'. If not say 'It is not post'");
			When($"I click element on 'mainPage' page named 'postButton'");
		}

  }
}
using TechTalk.SpecFlow;
namespace Framework
{
  [Binding]
  public sealed class CalculatorSteps : Steps
  {

		[Given ("I go to post in tut site")]
		
		public void IGoToPostInTutSite()
		{
			
			When($"I navigate to url 'https://tut.by'");
			When($"I click element on 'mainPage' page named 'postButton' via browser");
		}

  }
}
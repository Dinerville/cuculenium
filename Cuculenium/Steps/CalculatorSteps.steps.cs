using TechTalk.SpecFlow;
namespace Framework
{
  [Binding]
  public sealed class CalculatorSteps : Steps
  {

		[Given ("I searched for (.*)")]
		
		public void ISearchedFor(string searchString)
		{
			
			When($"I set text {searchString} for element on 'mainPage' named 'searchTextBox'");
			When($"I click element on 'mainPage' named 'searchButton'");
		}

  }
}
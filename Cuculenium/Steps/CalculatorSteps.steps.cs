using System;
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

		[When(@"I set text (.*) for element on '(.*)' named '(.*)'")]
		public void WhenISetTextForElementOnNamed(string p0, string p1, string p2)
		{
			Console.WriteLine("Fine set text");
		}

		[When(@"I click element on '(.*)' named '(.*)'")]
		public void WhenIClickElementOnNamed(string p0, string p1)
		{
			Console.WriteLine("Fine click text");
		}
	}
}
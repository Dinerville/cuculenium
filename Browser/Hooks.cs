using System;
using System.Collections.Generic;
using System.Text;
using BoDi;
using Browser.Browser;
using TechTalk.SpecFlow;

namespace Browser
{
	[Binding]
	public class Hooks
	{
		private readonly IObjectContainer objectContainer;
		private Browser.Browser Browser;

		public Hooks(IObjectContainer objectContainer)
		{
			this.objectContainer = objectContainer;
		}

		[BeforeScenario]
		public void BeforeScenario()
		{
			Browser = new BrowserBuilder().BuildByKey(Configuration.Configuration.BrowserToRun);
			objectContainer.RegisterInstanceAs(Browser);
		}

		[AfterScenario]
		public void AfterScenario()
		{
			Browser.Driver?.Quit();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using Browser.Utils;
using OpenQA.Selenium;

namespace Browser.WebElement
{
	public class WebElement
	{
		private Browser.Browser Browser { get; set; }
		private By Locator { get; set; }
		private string Name { get; set; }

		public WebElement(Browser.Browser browser, By locator, string name, string pageName)
		{
			Browser = browser;
			Browser.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
			Locator = locator;
			Name = $"{name} on {pageName}";
		}

		private IWebElement GetElement()
		{
			return Browser.Driver.FindElement(Locator);
		}

		public string GetText()
		{
			string text = null;
			Retry.DoWithRetry(() =>
			{
				text = GetElement().Text;

			}, $"I get text of element by locator {Locator}");
			return text;
		}

		public void Click()
		{
			Retry.DoWithRetry(() =>
			{
				GetElement().Click();
            
			},$"I click on element by locator {Locator}");
		}
	}
}

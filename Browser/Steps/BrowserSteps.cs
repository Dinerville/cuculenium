using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BoDi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using YamlDotNet.Serialization;

namespace Browser.Steps
{
	[Binding]
	public class BrowserSteps
	{
		private readonly Browser.Browser Browser;
		public BrowserSteps(IObjectContainer objectContainer)
		{
			Browser = objectContainer.Resolve<Browser.Browser>();
		}

		private By GetLocatorModel(string pageName, string locatorName)
		{
			var locators = Directory.GetFiles(Environment.CurrentDirectory, "locators.yaml", SearchOption.AllDirectories);
			var yamlFile = File.ReadAllText(locators[0]);

			var deserializer = new Deserializer();
			var yamlObject = deserializer.Deserialize(new StringReader(yamlFile));
			var jsonString = JsonConvert.SerializeObject(yamlObject);
			var jsonObject = JsonConvert.DeserializeObject<JObject>(jsonString);
			var locator = jsonObject[pageName][locatorName];
			var locatorValue = locator["value"].Value<string>().Trim();
			var locatorType = locator["type"].Value<string>().Trim();
			switch (locatorType.ToLower())
			{
				case "xpath":
					return By.XPath(locatorValue);
				case "id":
					return By.Id(locatorValue);
				case "cssselector":
					return By.CssSelector(locatorValue);
				case "name":
					return By.Name(locatorValue);
				case "classname":
					return By.ClassName(locatorValue);
				case "linktext":
					return By.LinkText(locatorValue);
				case "partiallinktext":
					return By.PartialLinkText(locatorValue);
				case "tagname":
					return By.TagName(locatorValue);
				default: throw new Exception($"Invalid locator type for locator on page {pageName} named {locatorName}. Found type is {locatorType} .Possible options are ClassName, CssSelector, Id, LinkText, Name, TagName, XPath");
			}
		}

		[When(@"I click element on '(.*)' page named '(.*)' via browser")]
		[Given(@"I click element on '(.*)' page named '(.*)' via browser")]
		public void WhenIAcceptAndChooseCreditCard(string pageName, string locatorName)
		{
			var locator = GetLocatorModel(pageName, locatorName);
			var element = new WebElement.WebElement(Browser, locator, locatorName, pageName);
			element.Click();
		}

		[When("I navigate to url '(.*)'")]
		[Given("I navigate to url '(.*)'")]
		public void NavigateToUrl(string url)
		{
			Browser.Driver.Navigate().GoToUrl(url);
		}
	}
}

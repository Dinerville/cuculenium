using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Browser.Browser
{
	public class BrowserBuilder
	{
		Browser Browser { get; set; }

		public BrowserBuilder()
		{
			Browser = new Browser();
		}

		public BrowserBuilder WithLocalChrome()
		{
			Browser.Driver = new ChromeDriver(Environment.CurrentDirectory,Configuration.Configuration.LocalChromeOptions);
			return this;
		}

		public BrowserBuilder WithLocalFirefox()
		{
			Browser.Driver = new FirefoxDriver(Environment.CurrentDirectory,Configuration.Configuration.LocalFirefoxOptions);
			return this;
		}

		public Browser BuildByKey(string key)
		{
			switch (key.ToLower())
			{
				case "local chrome":
					return WithLocalChrome().Build();
				case "local firefox":
					return WithLocalFirefox().Build();
				default:
					throw new Exception($"Browser key is not correct. You've set {key}. Possible options are: local chrome, local firefox");
			}
		}

		public Browser Build()
		{
			return Browser;
		}
	}
}

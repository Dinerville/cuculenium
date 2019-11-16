using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Browser.Configuration
{
	public static class Configuration
	{
		public static TimeSpan DefaultStepExecutionWait { get; set; } = TimeSpan.FromMinutes(1);
		public static ChromeOptions LocalChromeOptions
		{
			get
			{
				var chromeOptions = new ChromeOptions();
				chromeOptions.AddArgument("--incognito");
				return chromeOptions;
			}
		}
		public static FirefoxOptions LocalFirefoxOptions
		{
			get
			{
				var FirefoxOptions = new FirefoxOptions();
				FirefoxOptions.AddArgument("-private");
				return FirefoxOptions;
			}
		}

		public static string BrowserToRun { get; } = "local chrome";
	}
}

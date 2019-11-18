using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Browser.Steps
{
	public class AssertionSteps
	{
		[Then("I assert that expected '(.*)' equals to '(.*)'. If not say '(.*)'")]
		public void AssertThatEquals(string expected, string actual, string message)
		{
			Assert.AreEqual(expected, actual, message);
			Logger.Logger.LogInfo($"Assert equals expected:[{expected}], actual[{actual}] with not success message {message} has PASSED");
		}
	}
}

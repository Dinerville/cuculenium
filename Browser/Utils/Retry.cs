using Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Browser.Utils
{
	public static class Retry
	{
		public static void DoWithRetry(Action action, string actionName)
		{
			var watch = new Stopwatch();
			watch.Start();
			while (true)
			{
				try
				{
					Logger.Logger.LogInfo($"{actionName}");
					action();
				}
				catch (Exception)
				{
					Logger.Logger.LogInfo($"Failed to perform action {actionName}. Retrying");
				}

				if (watch.ElapsedMilliseconds>Configuration.Configuration.DefaultStepExecutionWait.Milliseconds)
				{
					throw new Exception($"Failed to perform action {actionName} within {Configuration.Configuration.DefaultStepExecutionWait.Seconds} seconds. Aborting");
				}
			}
		}
	}
}

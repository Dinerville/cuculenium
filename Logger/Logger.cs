using System;

namespace Logger
{
	public static class Logger
	{
		static string PatternLog(string level, string message) => $"{DateTime.Now} - [{level}] - {message}";

		public static void LogInfo(string message)
		{
			Console.WriteLine(PatternLog("INFO",message));
		}

		public static void LogDebug(string message)
		{
			Console.WriteLine(PatternLog("DEBUG", message));
		}
	}
}

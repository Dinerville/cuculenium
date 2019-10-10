using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace StepDefinitionsGenerator
{
	public static class Extensions
	{
		public static string ToCamelCase(this string str)
		{
			TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
			str = cultInfo.ToTitleCase(str);
			str = str.Replace(" ", "");
			return str;
		}
	}
}

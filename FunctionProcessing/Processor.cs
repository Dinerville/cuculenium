using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FunctionProcessing
{
	public class Processor
	{
		private static string VariableRegexp { get; } = "<@(.*)>";

		public static string ProcessVariable(string variable)
		{
			if (!variable.StartsWith("<@"))
			{
				return variable;
			}
			else
			{
				var match = Regex.Match(variable, VariableRegexp);
				var methodString = match.Groups[1].Value;
				var methodProps = methodString.Split(' ');
				var methodname = methodProps[0];
				var methodParams = methodProps.Where(methodProp => methodProp != methodname).ToArray();

				var obj = new Functions();
				var type = obj.GetType();
				var method = type.GetMethod(methodname);
				var result = method.Invoke(obj, methodParams);
				return (string)result;
			}
		}
	}
}

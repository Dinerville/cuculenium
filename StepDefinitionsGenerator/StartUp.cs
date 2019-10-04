using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace StepDefinitionsGenerator
{
	public class StartUp
	{
		public static void Main(string[] args)
		{
			var stepsFiles = Directory.GetFiles(@"..\..\..\..\Cuculenium\Steps", "*.steps", SearchOption.AllDirectories);
			var classes = stepsFiles.Select(stepsFile => StepsFileParser.Parse(stepsFile)).ToList();
			Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(classes));
		}
	}
}

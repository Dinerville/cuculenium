using System;
using System.Collections.Generic;
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

			var classes = stepsFiles.Select(StepsFileParser.Parse).ToList();

			classes.ForEach(cs =>
			{
				new ClassGenerator(cs).GenerateClassString().SaveAsCsFile();
			});
		}
	}
}

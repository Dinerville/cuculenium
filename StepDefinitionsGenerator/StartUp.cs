using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using StepDefinitionsGenerator.Generators;

namespace StepDefinitionsGenerator
{
	public class StartUp
	{
		public static void Main(string[] args)
		{
			Console.WriteLine(args.Length);
			Console.WriteLine("Args are : " + args[0]);
			var stepsFiles = Directory.GetFiles(args[0], "*.steps", SearchOption.AllDirectories);
			var classes = stepsFiles.Select(StepsFileParser.Parse).ToList();
			classes.ForEach(ClassGenerator.GenerateAndSaveAsFile);
		}
	}
}

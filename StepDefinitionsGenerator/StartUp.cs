using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace StepDefinitionsGenerator
{
	public class StartUp
	{
		public static void Main(string[] args)
		{
			var stepsFiles = Directory.GetFiles(@"..\..\..\..\Cuculenium\Steps", "*.steps", SearchOption.AllDirectories);
			var classes = stepsFiles.Select(stepsFile => StepsFileParser.Parse(stepsFile)).ToList();
			Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(classes));

			foreach (var classModel in classes)
			{

				var methods = new List<string>();
				foreach (var classModelStepModel in classModel.StepModels)
				{
					var stepName = classModelStepModel.StepName;
					var variables = new List<string>();
					var matches = Regex.Matches(stepName, "<(.*)>");
					foreach (Match match in matches)
					{
						variables.Add(match.Groups[1].Value);
					}

					var stepFinal = Regex.Replace(stepName, "<(.*)>", "(.*)");

					var method = "";
					foreach (var tag in classModelStepModel.Tags)
					{
						method += $"{tag}(\"{stepFinal}\")]{Environment.NewLine}";
					}

					method += $@"
						public void {}(string meetingNamePath)
						{{
							
						}}

					";
				}

				var classFile = $@"
				using System;
				using BoDi;
				using TechTalk.SpecFlow;
				namespace Framework
				{{
					[Binding]
					public sealed class {classModel.ClassName}
				    {{
						{string.Join(Environment.NewLine,methods)}
                    }}
				}}

				";
				if (File.Exists($"C:\\Automation\\{classModel.ClassName}.cs"))
				{
					File.Delete($"C:\\Automation\\{classModel.ClassName}.cs");
				}
				
				File.WriteAllText($"C:\\Automation\\{classModel.ClassName}.cs", classFile);
			}
			
		}
	}
}

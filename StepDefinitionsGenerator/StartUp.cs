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
				var pattern = "<([^>]*)>";
				var methods = new List<string>();
				foreach (var classModelStepModel in classModel.StepModels)
				{
					var stepName = classModelStepModel.StepName;
					var variables = new List<string>();
					var matches = Regex.Matches(stepName, pattern);
					foreach (Match match in matches)
					{
						variables.Add(match.Groups[1].Value);
					}

					var stepFinal = Regex.Replace(stepName, pattern, "(.*)").Trim();

					var method = "";
					foreach (var tag in classModelStepModel.Tags)
					{
						method += $"[{tag.Replace("@","")}(\"{stepFinal}\")]{Environment.NewLine}";
					}

					var methodName = stepFinal.ToCamelCase()
							.Replace(" ", "")
						.Replace("'", "")
						.Replace("(", "")
						.Replace(".", "")
						.Replace(")", "")
						.Replace("*", "");

					var parameters = "";

					foreach (var variable in variables)
					{
						parameters += $"string {variable}, ";
					}

					var steps = "";
					foreach (var step in classModelStepModel.Steps)
					{
						var actualStep = step;
						var matchesSteps = Regex.Matches(step, pattern);
						foreach (Match matchesStep in matchesSteps)
						{
							actualStep = actualStep.Replace(matchesStep.Value, "{" + matchesStep.Groups[1].Value + "}");
						}

						if (matchesSteps.Count==0)
						{
							actualStep = step;
						}

						var tagStepDef = actualStep.Split(' ');
						actualStep = actualStep.Substring(tagStepDef[0].Length, actualStep.Length- tagStepDef[0].Length).Trim();
						steps+=$"{tagStepDef[0]}($\"{actualStep}\");{Environment.NewLine}";
					}

					var finalParams = "";
					if (parameters.Length>0)
					{
						finalParams = parameters.Substring(0, parameters.Length - 2);
					}
					method += $@"
public void {methodName}({finalParams})
{{
  {steps}
}}

";
					methods.Add(method);
				}

				var classFile = $@"
using System;
using BoDi;
using TechTalk.SpecFlow;
namespace Framework
{{
  [Binding]
  public sealed class {classModel.ClassName} : TechTalk.SpecFlow.Steps
  {{
     {string.Join(Environment.NewLine,methods)}
  }}
}}";
				if (File.Exists($"C:\\Automation\\{classModel.ClassName}.cs"))
				{
					File.Delete($"C:\\Automation\\{classModel.ClassName}.cs");
				}
				
				File.WriteAllText($"C:\\Automation\\{classModel.ClassName}.cs", classFile);
			}
			
		}
	}
}

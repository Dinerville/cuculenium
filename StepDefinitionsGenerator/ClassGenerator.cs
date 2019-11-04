using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace StepDefinitionsGenerator
{
	public class ClassGenerator
	{
		private readonly ClassModel classModel;

		public ClassGenerator(ClassModel model)
		{
			classModel = model;
		}

		private string VariablePattern { get; } = "<([^>]*)>";
		private string GeneratedString { get; set; }

		public ClassGenerator GenerateClassString()
		{
			var methods = new List<string>();
			foreach (var classModelStepModel in classModel.StepModels)
				methods.Add(CreateMethodString(classModelStepModel));

			var classFile = $@"
using System;
using BoDi;
using TechTalk.SpecFlow;
namespace Framework
{{
  [Binding]
  public sealed class {classModel.ClassName} : TechTalk.SpecFlow.Steps
  {{
     {string.Join(Environment.NewLine, methods)}
  }}
}}";
			GeneratedString = classFile;
			return this;
		}

		private string CreateMethodString(StepModel stepModel)
		{
			var stepName = stepModel.StepName;
			var stepFinal = Regex.Replace(stepName, VariablePattern, "(.*)").Trim();

			var method = CreateTags(stepModel, stepFinal);

			var steps = "";
			foreach (var step in stepModel.Steps)
			{
				steps += CreateMethodBody(step);
			}

			
			method += $@"
public void {CreateMethodName(stepFinal)}({CreateParametersString(stepModel)})
{{
  {steps}
}}

";
			return method;
		}

		private string CreateMethodName(string stepFinal)
		{
			return stepFinal.ToCamelCase()
				.Replace(" ", "")
				.Replace("'", "")
				.Replace("(", "")
				.Replace(".", "")
				.Replace(")", "")
				.Replace("*", "");
		}

		private string CreateParametersString(StepModel model)
		{
			var variables = new List<string>();
			var matches = Regex.Matches(model.StepName, VariablePattern);
			foreach (Match match in matches) variables.Add(match.Groups[1].Value);

			var parameters = "";
			foreach (var variable in variables) parameters += $"string {variable}, ";
			if (parameters.Length > 0) parameters = parameters.Substring(0, parameters.Length - 2);
			return parameters;
		}

		private string CreateTags(StepModel model, string stepFinal)
		{
			var tags = "";
			foreach (var tag in model.Tags)
				tags += $"[{tag.Replace("@", "")}(\"{stepFinal}\")]{Environment.NewLine}";
			return tags;
		}

		private string CreateMethodBody(string step)
		{
			var actualStep = step;
			var matchesSteps = Regex.Matches(step, VariablePattern);
			foreach (Match matchesStep in matchesSteps)
				actualStep = actualStep.Replace(matchesStep.Value, "{" + matchesStep.Groups[1].Value + "}");

			if (matchesSteps.Count == 0) actualStep = step;

			var tagStepDef = actualStep.Split(' ');
			actualStep = actualStep.Substring(tagStepDef[0].Length, actualStep.Length - tagStepDef[0].Length)
				.Trim();
			return $"{tagStepDef[0]}($\"{actualStep}\");{Environment.NewLine}";
		}

		public void SaveAsCsFile()
		{
			if (File.Exists($"{classModel.StepsClassPath}.cs")) File.Delete($"{classModel.StepsClassPath}.cs");

			File.WriteAllText($"{classModel.StepsClassPath}.cs", GeneratedString);
		}
	}
}
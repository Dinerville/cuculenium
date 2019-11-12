using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace StepDefinitionsGenerator.Generators
{
	public class MethodGenerator
	{
		private string VariablePattern { get; } = "<([^>]*)>";
		private List<string> LocalVariables = new List<string>();
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
		private List<string> GetParameters(StepModel model)
		{
			var variables = new List<string>();
			var matches = Regex.Matches(model.StepName, VariablePattern);
			foreach (Match match in matches) variables.Add(match.Groups[1].Value);
			return variables;
		}

		private string CreateParametersString(StepModel model)
		{
			var variables = GetParameters(model);
			var parameters = "";
			foreach (var variable in variables) parameters += $"string {variable}, ";
			if (parameters.Length > 0) parameters = parameters.Substring(0, parameters.Length - 2);
			return parameters;
		}

		private string CreateTags(StepModel model, string stepFinal)
		{
			var tags = "";
			foreach (var tag in model.Tags)
				tags += $"[{tag.Replace("@", "")}(\"{stepFinal}\")]{Environment.NewLine}\t\t";
			return tags;
		}

		private string CreateMethodBody(StepModel stepModel)
		{
			var methodBody = "";
			foreach (var step in stepModel.Steps)
			{
				var actualStep = GetStepWithoutTag(ReplaceStepVariables(step, GetParameters(stepModel)));
				methodBody += $"{Environment.NewLine}\t\t\t{GetStepTag(step)}($\"{actualStep}\");";
			}
			return methodBody;
		}

		private string GetStepTag(string step)
		{
			return step.Split(' ')[0];
		}

		private string GetStepWithoutTag(string step)
		{
			var tag = GetStepTag(step);
			return step.Substring(tag.Length, step.Length - tag.Length)
				.Trim();
		}

		private string ReplaceStepVariables(string step, List<string> methodVariables)
		{
			var actualStep = step;
			var matchesSteps = Regex.Matches(step, VariablePattern);
			foreach (Match matchesStep in matchesSteps)
			{
				if (methodVariables.Contains(matchesStep.Groups[1].Value))
				{
					actualStep = actualStep.Replace(matchesStep.Value, "{" + matchesStep.Groups[1].Value + "}");
				}
				else
				{
					if (!LocalVariables.Contains(matchesStep.Groups[1].Value))
					{
						actualStep = actualStep.Replace(matchesStep.Value, "<@SetLocal " + matchesStep.Groups[1].Value + ">");
						LocalVariables.Add(matchesStep.Groups[1].Value);
					}
					else
					{
						actualStep = actualStep.Replace(matchesStep.Value, "<@GetLocal " + matchesStep.Groups[1].Value + ">");
					}
				}
				
			}
				

			if (matchesSteps.Count == 0) actualStep = step;
			return actualStep;
		}

		public string CreateMethodString(StepModel stepModel)
		{
			var stepName = stepModel.StepName;
			var stepFinal = Regex.Replace(stepName, VariablePattern, "(.*)").Trim();
			var method = $@"
		{CreateTags(stepModel, stepFinal)}
		public void {CreateMethodName(stepFinal)}({CreateParametersString(stepModel)})
		{{
			{CreateMethodBody(stepModel)}
		}}
";
			return method;
		}
	}
}

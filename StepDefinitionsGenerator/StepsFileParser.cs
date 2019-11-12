using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace StepDefinitionsGenerator
{
	public static class StepsFileParser
	{
		public static ClassModel Parse(string stepsFile)
		{
			var steps = File.ReadAllLines(stepsFile);

			var classModel = new ClassModel();
			classModel.StepsClassPath = stepsFile;
			StepModel currentStep = new StepModel();
			var listOfTags = new List<string>();

			for (var index = 0; index < steps.Length; index++)
			{
				var step = steps[index];
				if (step.Equals(String.Empty)||step.StartsWith("#"))
				{
					if (index == steps.Length - 1)
					{
						classModel.StepModels.Add(currentStep);
					}
					continue;
				}
				if (step.StartsWith("Steps:"))
				{
					var className = step.Replace("Steps:", "");
					className = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(className.ToLower());
					className = className.Replace(" ", "").Trim();
					classModel.ClassName = className;
					continue;
				}

				if (step.StartsWith("@Given") || step.StartsWith("@When") || step.StartsWith("@Then"))
				{
					if (currentStep.StepName != null)
					{
						classModel.StepModels.Add(currentStep);
						currentStep = new StepModel();
						listOfTags = new List<string>();
					}

					listOfTags.Add(step);

					if (steps[index + 1].StartsWith("Step:"))
					{
						currentStep.Tags = listOfTags;
					}
					continue;
				}

				if (step.StartsWith("Step:"))
				{
					currentStep.StepName = step.Replace("Step:", "");
					continue;
				}

				currentStep?.Steps.Add(step.Trim());


				if (index == steps.Length - 1)
				{
					classModel.StepModels.Add(currentStep);
				}
			}

			return classModel;
		}
	}
}

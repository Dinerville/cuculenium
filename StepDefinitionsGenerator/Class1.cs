using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace StepDefinitionsGenerator
{
	public class Class1
	{
		public static void Main(string[] args)
		{
			var stepsFiles = Directory.GetFiles(@"..\..\..\..\Cuculenium\Steps", "*.steps", SearchOption.AllDirectories);
			var classes = new List<ClassModel>();

			foreach (var stepsFile in stepsFiles)
			{
				var steps = File.ReadAllLines(stepsFile);

				var classModel = new ClassModel();
				StepModel currentStep = new StepModel();
				var listOfTags = new List<string>();

				for (var index = 0; index < steps.Length; index++)
				{
					var step = steps[index];
					if (step.Equals(String.Empty))
					{
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
						if (currentStep.StepName!=null)
						{
							classModel.StepModels.Add(currentStep);
							currentStep = new StepModel();
							listOfTags = new List<string>();
						}

						listOfTags.Add(step);

						if (steps[index+1].StartsWith("Step:"))
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


					if (index== steps.Length-1)
					{
						classModel.StepModels.Add(currentStep);
					}
				}

				classes.Add(classModel);
			}


			Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(classes));
		}
	}

	public class ClassModel
	{
		public string ClassName { get; set; }
		public List<StepModel> StepModels { get; set; } = new List<StepModel>();
	}

	public class StepModel
	{
		public List<string> Tags { get; set; }
		public string StepName { get; set; }
		public List<string> Steps { get; set; } = new List<string>();
	}
}

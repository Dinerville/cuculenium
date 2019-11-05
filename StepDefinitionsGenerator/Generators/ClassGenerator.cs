using System;
using System.Collections.Generic;
using System.IO;

namespace StepDefinitionsGenerator.Generators
{
	public static class ClassGenerator
	{
		public static void GenerateAndSaveAsFile(ClassModel classModel)
		{
			var filepath = $"{classModel.StepsClassPath}.cs";
			if (File.Exists(filepath)) File.Delete(filepath);
			File.WriteAllText(filepath, Generate(classModel));
		}

		private static string Generate(ClassModel classModel)
		{
			var methods = new List<string>();
			foreach (var classModelStepModel in classModel.StepModels)
			{
				methods.Add(new MethodGenerator().CreateMethodString(classModelStepModel));
			}

			var classFile = $@"using TechTalk.SpecFlow;
namespace Framework
{{
  [Binding]
  public sealed class {classModel.ClassName} : Steps
  {{
{string.Join(Environment.NewLine, methods)}
  }}
}}";
			return classFile;
		}

		
	}
}
using System.Collections.Generic;

namespace StepDefinitionsGenerator
{
	public class ClassModel
	{
		public string ClassName { get; set; }
		public List<StepModel> StepModels { get; set; } = new List<StepModel>();
	}
}
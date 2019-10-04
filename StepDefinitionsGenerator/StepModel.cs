using System.Collections.Generic;

namespace StepDefinitionsGenerator
{
	public class StepModel
	{
		public List<string> Tags { get; set; }
		public string StepName { get; set; }
		public List<string> Steps { get; set; } = new List<string>();
	}
}
using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	public class ConfigurationEnums
	{
		public ConfigurationEnums()
		{
			Fields = new Dictionary<string, int>();
		}

		public ConfigurationEnums(string name, string desc = "") : this()
		{
			Name = name;
			Desc = desc;
		}

		public string Name { get; set; }

		public string Desc { get; set; }

		public Dictionary<string, int> Fields { get; }

		public void AppendField(string fieldName, int fieldValue)
		{
			Fields.Add(fieldName, fieldValue);
		}
	}
}
using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	public class ConfigurationEnumField
	{
		public ConfigurationEnumField() { /*..*/ }

		public ConfigurationEnumField(string name, int value, string desc = "")
		{
			Name = name;
			Value = value;
			Desc = desc;
		}

		public string Name { get; set; }

		public int Value { get; set; }

		public string Desc { get; set; }
	}
}

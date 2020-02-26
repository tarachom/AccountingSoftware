using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	public class ConfigurationEnums
	{
		public ConfigurationEnums()
		{
			Fields = new Dictionary<string, ConfigurationEnumField>();
		}

		public ConfigurationEnums(string name, int serialNumber = 0, string desc = "") : this()
		{
			Name = name;
			Desc = desc;
			SerialNumber = serialNumber;
		}

		public string Name { get; set; }

		public string Desc { get; set; }

		public int SerialNumber { get; set; }

		public Dictionary<string, ConfigurationEnumField> Fields { get; }

		public void AppendField(ConfigurationEnumField field)
		{
			Fields.Add(field.Name, field);
		}
	}
}
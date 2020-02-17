using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	public class ConfigurationObjectTablePart : ConfigurationObject
	{
		public ConfigurationObjectTablePart()
		{
			Fields = new Dictionary<string, ConfigurationObjectField>();
		}

		public ConfigurationObjectTablePart(string name, string table, string desc = "") : this()
		{
			Name = name;
			Table = table;
			Desc = desc;
		}

		public Dictionary<string, ConfigurationObjectField> Fields { get; }

		public ConfigurationObjectField AppendField(ConfigurationObjectField field)
		{
			Fields.Add(field.Name, field);
			return field;
		}
	}
}

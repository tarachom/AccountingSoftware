using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	public class ConfigurationObjectTablePart
	{
		public ConfigurationObjectTablePart()
		{
			Init();
		}

		public ConfigurationObjectTablePart(string name, string table, string desc = "")
		{
			Init();

			Name = name;
			Table = table;
			Desc = desc;
		}

		private void Init()
		{
			Fields = new Dictionary<string, ConfigurationObjectField>();
		}

		public string Name { get; set; }

		public string Table { get; set; }

		public string Desc { get; set; }

		public Dictionary<string, ConfigurationObjectField> Fields { get; set; }

		public ConfigurationObjectField AppendField(ConfigurationObjectField field)
		{
			Fields.Add(field.Name, field);
			return field;
		}
	}
}

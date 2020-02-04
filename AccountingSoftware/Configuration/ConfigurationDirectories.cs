using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	public class ConfigurationDirectories : ConfigurationObject
	{
		public ConfigurationDirectories()
		{
			Init();
		}

		public ConfigurationDirectories(string name, string table, string desc = "")
		{
			Init();

			base.Name = name;
			base.Table = table;
			base.Desc = desc;
		}

		private void Init()
		{
			Fields = new Dictionary<string, ConfigurationObjectField>();
			TabularParts = new Dictionary<string, ConfigurationObjectTablePart>();
			Views = new Dictionary<string, ConfigurationObjectView>();
		}

		public Dictionary<string, ConfigurationObjectField> Fields { get; set; }

		public Dictionary<string, ConfigurationObjectTablePart> TabularParts { get; set; }

		public Dictionary<string, ConfigurationObjectView> Views { get; set; }

		public ConfigurationObjectField AppendField(ConfigurationObjectField field)
		{
			Fields.Add(field.Name, field);
			return field;
		}
	}
}

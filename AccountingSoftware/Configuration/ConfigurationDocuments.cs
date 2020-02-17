using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	public class ConfigurationDocuments : ConfigurationObject
	{
		public ConfigurationDocuments()
		{
			Fields = new Dictionary<string, ConfigurationObjectField>();
			TabularParts = new Dictionary<string, ConfigurationObjectTablePart>();
		}

		public ConfigurationDocuments(string name, string table, string desc = "") : this()
		{
			Name = name;
			Table = table;
			Desc = desc;
		}

		public Dictionary<string, ConfigurationObjectField> Fields { get; }

		public Dictionary<string, ConfigurationObjectTablePart> TabularParts { get; }
	}
}

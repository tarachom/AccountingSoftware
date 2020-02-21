using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	public class ConfigurationObjectView : ConfigurationObject
	{
		public ConfigurationObjectView()
		{
			Fields = new Dictionary<string, string>();
			Where = new List<string>();
		}

		public ConfigurationObjectView(string name, string table, string desc) : this()
		{
			Name = name;
			Table = table;
			Desc = desc;
		}

		public Dictionary<string, string> Fields { get; }

		public List<string> Where { get; } // ? 
	}
}
using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	//
	//
	public class ConfigurationObjectView
	{
		public ConfigurationObjectView()
		{
			Init();
		}

		public ConfigurationObjectView(string name, string table, string primaryField, string desc)
		{
			Init();

			Name = name;
			Table = table;
			PrimaryField = primaryField;
			Desc = desc;
		}

		private void Init()
		{
			Fields = new Dictionary<string, string>();
			Where = new List<string>();
		}

		public string Name { get; set; }

		public string Table { get; set; }

		public string PrimaryField { get; set; }

		public string Desc { get; set; }

		public Dictionary<string, string> Fields { get; set; }

		public List<string> Where { get; set; }
	}
}

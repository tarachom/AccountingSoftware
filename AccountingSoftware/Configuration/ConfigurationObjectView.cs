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

		public ConfigurationObjectView(string name, string desc = "")
		{
			Init();

			Name = name;
			Desc = desc;
		}

		private void Init()
		{
			Fields = new Dictionary<string, string>();
		}

		public string Name { get; set; }

		public string Desc { get; set; }

		public Dictionary<string, string> Fields { get; set; }
	}
}

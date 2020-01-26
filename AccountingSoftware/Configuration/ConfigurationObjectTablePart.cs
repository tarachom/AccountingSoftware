using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware
{
	//
	//
	public class ConfigurationObjectTablePart
	{
		public ConfigurationObjectTablePart()
		{
			Init();
		}

		public ConfigurationObjectTablePart(string name, string desc = "")
		{
			Init();

			Name = name;
			Desc = desc;
		}

		private void Init()
		{
			Fields = new Dictionary<string, ConfigurationObjectField>();
		}

		public string Name { get; set; }

		public string Desc { get; set; }

		public Dictionary<string, ConfigurationObjectField> Fields { get; set; }
	}
}

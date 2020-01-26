using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware
{
	//
	//
	public class ConfigurationObjectField
	{
		public ConfigurationObjectField()
		{

		}

		public ConfigurationObjectField(string name, string desc = "", string type = "string")
		{
			Name = name;
			Desc = desc;
			Type = type;
		}

		public string Name { get; set; }

		public string Desc { get; set; }

		public string Type { get; set; }
	}
}

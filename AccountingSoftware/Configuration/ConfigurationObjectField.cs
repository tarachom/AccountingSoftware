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

		public ConfigurationObjectField(string name, string type, string pointer, string desc = "")
		{
			Name = name;
			Type = type;
			Pointer = pointer;

			Desc = desc;
		}

		public string Name { get; set; }

		public string Type { get; set; }

		public string Pointer { get; set; }

		public string Desc { get; set; }
	}
}

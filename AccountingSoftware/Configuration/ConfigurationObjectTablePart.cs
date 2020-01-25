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
			Fields = new Dictionary<string, ConfigurationObjectField>();
		}

		public string Name { get; set; }

		public string Desc { get; set; }

		public Dictionary<string, ConfigurationObjectField> Fields { get; set; }
	}
}

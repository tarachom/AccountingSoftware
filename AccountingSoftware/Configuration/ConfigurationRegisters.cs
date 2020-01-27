using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccountingSoftware
{
	public class ConfigurationRegisters : ConfigurationObject
	{
		public ConfigurationRegisters()
		{
			Fields = new Dictionary<string, ConfigurationObjectField>();
		}

		public Dictionary<string, ConfigurationObjectField> Fields { get; set; }
	}
}

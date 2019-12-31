using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestErlang.AccountingSoftware
{
	//
	//
	class ConfigurationObjectTablePart
	{
		string Name { get; set; }

		Dictionary<string, ConfigurationObjectField> Fields { get; set; }
	}
}

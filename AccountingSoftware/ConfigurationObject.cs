using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestErlang.AccountingSoftware
{
	//Клас для обєкту конфігурації
	//Наприклад довідник чи документ
	class ConfigurationObject
	{
		string Name { get; set; }

		ConfigurationObjectType ConfObjectType { get; set; }

		List<ConfigurationObjectValue> ConfObjectValue { get; set; }

		Dictionary<string, ConfigurationObjectField> Fields { get; set; }

		Dictionary<string, ConfigurationObjectTablePart> TabularParts { get; set; }
	}
}

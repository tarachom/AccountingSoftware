using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware
{
	//Клас для обєкту конфігурації
	//Наприклад довідник чи документ
	public class ConfigurationObject
	{
		public string Name { get; set; }

		public ConfigurationObjectType ConfObjectType { get; set; }

		public List<ConfigurationObjectValue> ConfObjectValue { get; set; }

		public Dictionary<string, ConfigurationObjectField> Fields { get; set; }

		public Dictionary<string, ConfigurationObjectTablePart> TabularParts { get; set; }
	}
}

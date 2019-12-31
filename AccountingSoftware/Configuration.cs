using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestErlang.AccountingSoftware
{
	//Конфігурація
	//В цьому класі має міститися вся інформація про конфігурацію
	class Configuration
	{
		string Name { get; set; }

		Dictionary<string, ConfigurationObject> Constants { get; set; }

		Dictionary<string, ConfigurationObject> Directories { get; set; }

		Dictionary<string, ConfigurationObject> Documents { get; set; }

		Dictionary<string, ConfigurationObject> Enums { get; set; }

		Dictionary<string, ConfigurationObject> Registers { get; set; }
	}

	
}

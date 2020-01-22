using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware
{
	//Конфігурація
	//В цьому класі має міститися вся інформація про конфігурацію
	public class Configuration
	{
		public string Name { get; set; }

		public Dictionary<string, ConfigurationObject> Constants { get; set; }

		public Dictionary<string, ConfigurationObject> Directories { get; set; }

		public Dictionary<string, ConfigurationObject> Documents { get; set; }

		public Dictionary<string, ConfigurationObject> Enums { get; set; }

		public Dictionary<string, ConfigurationObject> Registers { get; set; }
	}

	
}

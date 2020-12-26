using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configurator
{
	public class ConfigurationParam
	{
		public string ConfigurationKey { get; set; }

		public string ConfigurationName { get; set; }

		public string ConfigurationPath { get; set; }

		public string DataBaseServer { get; set; }

		public int DataBasePort { get; set; }

		public string DataBaseLogin { get; set; }

		public string DataBasePassword { get; set; }

		public string DataBaseBaseName { get; set; }

		public override string ToString()
		{
			return String.IsNullOrWhiteSpace(ConfigurationName) ? "<>" : ConfigurationName;
		}
	}
}

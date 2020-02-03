using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	public abstract class ConfigurationObject 
	{
		public string Name { get; set; }

		public string Table { get; set; }

		public string Desc { get; set; }
	}
}

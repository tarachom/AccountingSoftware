﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware
{
	public class ConfigurationDocuments : ConfigurationObject
	{
		public ConfigurationDocuments()
		{
			Fields = new Dictionary<string, ConfigurationObjectField>();
			TabularParts = new Dictionary<string, ConfigurationObjectTablePart>();
		}

		public Dictionary<string, ConfigurationObjectField> Fields { get; set; }

		public Dictionary<string, ConfigurationObjectTablePart> TabularParts { get; set; }
	}
}
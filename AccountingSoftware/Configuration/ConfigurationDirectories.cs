using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware
{
	public class ConfigurationDirectories : ConfigurationObject
	{
		public ConfigurationDirectories()
		{
			Init();
		}

		public ConfigurationDirectories(string name, string desc)
		{
			Init();

			base.Name = Name;
			base.Desc = desc;
		}

		private void Init()
		{
			Fields = new Dictionary<string, ConfigurationObjectField>();
			TabularParts = new Dictionary<string, ConfigurationObjectTablePart>();
		}

		public Dictionary<string, ConfigurationObjectField> Fields { get; set; }

		public Dictionary<string, ConfigurationObjectTablePart> TabularParts { get; set; }

		public void AppendField(ConfigurationObjectField field)
		{
			Fields.Add(field.Name, field);
		}
	}
}

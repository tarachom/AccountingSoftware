using System;
using System.Collections.Generic;
using System.Text;

namespace AccountingSoftware
{
	class ConfigurationInformationSchema
	{
		public ConfigurationInformationSchema()
		{
			Tables = new Dictionary<string, ConfigurationInformationSchema_Table>();
		}

		public Dictionary<string, ConfigurationInformationSchema_Table> Tables { get; }

		public void Append(string table, string column, string dataType, string udtName)
		{
			if (!Tables.ContainsKey(table))
			{
				Tables.Add(table, new ConfigurationInformationSchema_Table());
			}

			Tables[table].Columns.Add(column, new ConfigurationInformationSchema_Column(column, dataType, udtName));
		}
	}

	class ConfigurationInformationSchema_Table
	{
		public ConfigurationInformationSchema_Table()
		{
			Columns = new Dictionary<string, ConfigurationInformationSchema_Column>();
		}

		public string TableName { get; set; }

		public Dictionary<string, ConfigurationInformationSchema_Column> Columns { get; }
	}

	class ConfigurationInformationSchema_Column
	{
		public ConfigurationInformationSchema_Column() { }

		public ConfigurationInformationSchema_Column(string columnName, string dataType, string udtName)
		{
			ColumnName = columnName;
			DataType = dataType;
			UdtName = udtName;
		}

		public string ColumnName { get; set; }

		public string DataType { get; set; }

		public string UdtName { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AccountingSoftware
{
	public class ConfigurationInformationSchema
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
				Tables.Add(table, new ConfigurationInformationSchema_Table(table));
			}

			Tables[table].Columns.Add(column, new ConfigurationInformationSchema_Column(column, dataType, udtName));
		}
	}

	public class ConfigurationInformationSchema_Table
	{
		public ConfigurationInformationSchema_Table(string tableName)
		{
			TableName = tableName;
			Columns = new Dictionary<string, ConfigurationInformationSchema_Column>();
		}

		public string TableName { get; set; }
		public Dictionary<string, ConfigurationInformationSchema_Column> Columns { get; }
	}

	public class ConfigurationInformationSchema_Column
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
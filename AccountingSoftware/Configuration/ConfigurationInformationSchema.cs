/*
Copyright (C) 2019-2020 Tarakhomin Yuri Ivanovich
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

/*
Автор:    Тарахомин Юрій Іванович
Адреса:   Україна, м. Львів
Сайт:     find.org.ua
*/

using System;
using System.Collections.Generic;

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
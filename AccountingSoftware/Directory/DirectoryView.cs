/*
Copyright (C) 2019-2020 TARAKHOMYN YURIY IVANOVYCH
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
Сайт:     accounting.org.ua
*/

using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	/// <summary>
	/// Довідник Візуалізація для вибірки даних
	/// </summary>
	public abstract class DirectoryView
	{
		public DirectoryView(Kernel kernel, string table, string[] fieldsNameInTableArray, 
			string[] fieldsNameArray, string[] fieldsTypeArray, string name)
		{
			Table = table;
			Kernel = kernel;
			//FieldTypeArray = fieldsTypeArray;
			Name = name;

			Alias = new Dictionary<string, string>();
			AliasRevers = new Dictionary<string, string>();
			AliasFieldType = new Dictionary<string, string>();

			QuerySelect = new Query(table);

			for (int i = 0; i < fieldsNameInTableArray.Length; i++)
			{
				Alias.Add(fieldsNameArray[i], fieldsNameInTableArray[i]);
				AliasRevers.Add(fieldsNameInTableArray[i], fieldsNameArray[i]);
				AliasFieldType.Add(fieldsNameInTableArray[i], fieldsTypeArray[i]);
				QuerySelect.Field.Add(fieldsNameInTableArray[i]);
			}				
		}

		private Kernel Kernel { get; set; }

		private string Table { get; set; }

		public Dictionary<string, string> Alias { get; }
		public Dictionary<string, string> AliasRevers { get; }
		public Dictionary<string, string> AliasFieldType { get; }

		public string Name { get; } 

		public Query QuerySelect { get; set; }

		public string Read()
		{
			return Kernel.DataBase.SelectDirectoryView(this);
		}

		public void DeleteTempTable()
		{
			Kernel.DataBase.DeleteDirectoryViewTempTable(this);
		}
	}
}

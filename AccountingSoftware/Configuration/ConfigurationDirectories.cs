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
Сайт:     find.org.ua
*/

using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	public class ConfigurationDirectories : ConfigurationObject
	{
		public ConfigurationDirectories()
		{
			Fields = new Dictionary<string, ConfigurationObjectField>();
			TabularParts = new Dictionary<string, ConfigurationObjectTablePart>();
			Views = new Dictionary<string, ConfigurationObjectView>();
		}

		public ConfigurationDirectories(string name, string table, string desc = "") : this()
		{
			Name = name;
			Table = table;
			Desc = desc;
		}

		public Dictionary<string, ConfigurationObjectField> Fields { get; }

		public Dictionary<string, ConfigurationObjectTablePart> TabularParts { get; }

		public Dictionary<string, ConfigurationObjectView> Views { get; }

		public ConfigurationObjectField AppendField(ConfigurationObjectField field)
		{
			Fields.Add(field.Name, field);
			return field;
		}

		public ConfigurationObjectTablePart AppendTablePart(ConfigurationObjectTablePart tablePart)
		{
			TabularParts.Add(tablePart.Name, tablePart);
			return tablePart;
		}

		public ConfigurationObjectView AppendView(ConfigurationObjectView view)
		{
			Views.Add(view.Name, view);
			return view;
		}
	}
}

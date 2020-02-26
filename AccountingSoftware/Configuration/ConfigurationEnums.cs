﻿/*
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
	public class ConfigurationEnums
	{
		public ConfigurationEnums()
		{
			Fields = new Dictionary<string, ConfigurationEnumField>();
		}

		public ConfigurationEnums(string name, int serialNumber = 0, string desc = "") : this()
		{
			Name = name;
			Desc = desc;
			SerialNumber = serialNumber;
		}

		public string Name { get; set; }

		public string Desc { get; set; }

		public int SerialNumber { get; set; }

		public Dictionary<string, ConfigurationEnumField> Fields { get; }

		public void AppendField(ConfigurationEnumField field)
		{
			Fields.Add(field.Name, field);
		}
	}
}
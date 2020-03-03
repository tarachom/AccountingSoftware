﻿/*
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
	/// <summary>
	/// Константи
	/// </summary>
	public class ConfigurationConstants
	{
		public ConfigurationConstants() { }

		public ConfigurationConstants(string name, string type, ConfigurationConstantsBlock block, string pointer = "", string desc = "") /* : this() */
		{
			Name = name;
			Type = type;
			Block = block;
			Pointer = pointer;
			Desc = desc;
		}

		public ConfigurationConstantsBlock Block { get; set; }

		/// <summary>
		/// Назва
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Опис
		/// </summary>
		public string Desc { get; set; }

		/// <summary>
		/// Тип даних
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// Вказівник на об'єкт конфігурації
		/// </summary>
		public string Pointer { get; set; }
	}
}

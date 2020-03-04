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
	/// <summary>
	/// Регістри накопичення
	/// </summary>
	public class ConfigurationRegistersAccumulation : ConfigurationObject
	{
		public ConfigurationRegistersAccumulation()
		{
			DimensionFields = new Dictionary<string, ConfigurationObjectField>();
			ResourcesFields = new Dictionary<string, ConfigurationObjectField>();
			PropertyFields = new Dictionary<string, ConfigurationObjectField>();
		}

		/// <summary>
		/// Регістри накопичення
		/// </summary>
		/// <param name="name">Назва</param>
		/// <param name="table">Таблиця в базі даних</param>
		/// <param name="desc">Опис</param>
		public ConfigurationRegistersAccumulation(string name, string table, int type, string desc = "") : this()
		{
			Name = name;
			Table = table;
			Desc = desc;

			TypeRegistersAccumulation = (TypeRegistersAccumulation)type;
		}

		public TypeRegistersAccumulation TypeRegistersAccumulation { get; set; }

		/// <summary>
		/// Виміри
		/// </summary>
		public Dictionary<string, ConfigurationObjectField> DimensionFields { get; }

		/// <summary>
		/// Русурси
		/// </summary>
		public Dictionary<string, ConfigurationObjectField> ResourcesFields { get; }

		/// <summary>
		/// Реквізити
		/// </summary>
		public Dictionary<string, ConfigurationObjectField> PropertyFields { get; }
	}

	public enum TypeRegistersAccumulation
	{
		/// <summary>
		/// Залишки
		/// </summary>
		Residues = 1,

		/// <summary>
		/// Оборот
		/// </summary>
		Turnover = 2
	}
}

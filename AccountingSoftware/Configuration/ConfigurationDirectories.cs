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

using System.Collections.Generic;

namespace AccountingSoftware
{
	/// <summary>
	/// Довідник
	/// </summary>
	public class ConfigurationDirectories : ConfigurationObject
	{
		/// <summary>
		/// Довідник
		/// </summary>
		public ConfigurationDirectories()
		{
			Fields = new Dictionary<string, ConfigurationObjectField>();
			TabularParts = new Dictionary<string, ConfigurationObjectTablePart>();
			Views = new Dictionary<string, ConfigurationObjectView>();
		}

		/// <summary>
		/// Довідник
		/// </summary>
		/// <param name="name">Назва</param>
		/// <param name="table">Таблиця в базі даних</param>
		/// <param name="desc">Опис</param>
		public ConfigurationDirectories(string name, string table, string desc = "") : this()
		{
			Name = name;
			Table = table;
			Desc = desc;
		}

		/// <summary>
		/// Поля
		/// </summary>
		public Dictionary<string, ConfigurationObjectField> Fields { get; }

		/// <summary>
		/// Табличні частини
		/// </summary>
		public Dictionary<string, ConfigurationObjectTablePart> TabularParts { get; }

		/// <summary>
		/// Візуалізації
		/// </summary>
		public Dictionary<string, ConfigurationObjectView> Views { get; }

		/// <summary>
		/// Додати нове поле
		/// </summary>
		/// <param name="field">Нове поле</param>
		public void AppendField(ConfigurationObjectField field)
		{
			Fields.Add(field.Name, field);
		}

		/// <summary>
		/// Додати нову табличну частину
		/// </summary>
		/// <param name="tablePart">Нова таблична частина</param>
		public void AppendTablePart(ConfigurationObjectTablePart tablePart)
		{
			TabularParts.Add(tablePart.Name, tablePart);
		}

		/// <summary>
		/// Додати нову візуалізацію
		/// </summary>
		/// <param name="view">Нова візуалізація</param>
		public void AppendView(ConfigurationObjectView view)
		{
			Views.Add(view.Name, view);
		}
	}
}

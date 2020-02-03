using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	/// <summary>
	/// Поле
	/// </summary>
	public class ConfigurationObjectField
	{
		public ConfigurationObjectField()
		{

		}

		/// <summary>
		/// Поле
		/// </summary>
		/// <param name="name">Назва поля</param>
		/// <param name="nameInTable">Назва поля в базі даних</param>
		/// <param name="type">Тип поля</param>
		/// <param name="pointer">Вказівник</param>
		/// <param name="desc">Опис</param>
		public ConfigurationObjectField(string name, string nameInTable, string type, string pointer, string desc = "")
		{
			Name = name;
			NameInTable = nameInTable;
			Type = type;
			Pointer = pointer;
			Desc = desc;
		}

		/// <summary>
		/// Назва поля в конфігурації
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Назва поля в базі даних
		/// </summary>
		public string NameInTable { get; set; }

		public string Type { get; set; }

		/// <summary>
		/// Якщо тип (Type = "pointer") потрібно вказати назву довідника з конфігурації на який робиться ссилка
		/// </summary>
		public string Pointer { get; set; }

		/// <summary>
		/// Опис
		/// </summary>
		public string Desc { get; set; }
	}
}

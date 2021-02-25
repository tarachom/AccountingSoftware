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
using System.Text;

namespace AccountingSoftware
{
	/// <summary>
	/// Конструктор SELECT запиту
	/// </summary>
	public class Query
	{
		/// <summary>
		/// Конструктор SELECT запиту
		/// </summary>
		/// <param name="table">Таблиця</param>
		public Query(string table)
		{
			Field = new List<string>();
			Where = new List<Where>();
			Order = new Dictionary<string, SelectOrder>();

			Table = table;
		}

		/// <summary>
		/// Назва таблиці
		/// </summary>
		public string Table { get; set; }

		/// <summary>
		/// Назва тимчасової таблиці
		/// </summary>
		public string TempTable { get; set; }

		/// <summary>
		/// Створити тимчасову таблицю на основі запиту
		/// </summary>
		public bool CreateTempTable { get; set; }

		/// <summary>
		/// Які поля вибирати
		/// </summary>
		public List<string> Field { get; set; }

		/// <summary>
		/// Умови.
		/// 1. Назва поля
		/// 2. Тип порівняння
		/// 3. Значення
		/// 4. Тип порівняння з наступним блоком (по замовчуванню AND)
		/// Example: Name EQ "Test" AND (Name = "Test" AND ... )
		/// </summary>
		public List<Where> Where { get; set; }

		/// <summary>
		/// Сортування. 
		/// Назва поля, тип сортування
		/// Name ASC, Code Desc
		/// </summary>
		public Dictionary<string, SelectOrder> Order { get; set; }

		/// <summary>
		/// Обмеження вибірки
		/// </summary>
		public int Limit { get; set; }

		/// <summary>
		/// Пропустити задану кількість записів
		/// </summary>
		public int Offset { get; set; }

		/// <summary>
		/// Збирає запит
		/// </summary>
		/// <returns>Повертає запит</returns>
		public string Construct()
		{
			string query = "";

			if (CreateTempTable == true)
			{
				TempTable = "tmp_" + Guid.NewGuid().ToString().Replace("-", "");
				query = "CREATE TEMP TABLE " + TempTable + " AS \n";
			}

			query += "SELECT uid";

			if (Field.Count > 0)
			{
				foreach (string field in Field)
					query += ", " + field;
			}

			query += "\nFROM " + Table;

			if (Where.Count > 0)
			{
				int count = 0;
				int lenght = Where.Count;

				query += "\nWHERE ";

				foreach (Where field in Where)
				{
					count++;

					if (count > 1)
					{
						if (field.ComparisonPreceding != Comparison.Empty)
							query += " " + field.ComparisonPreceding;
					}

					query += " " + field.Name;

					switch (field.Comparison)
					{
						case Comparison.EQ:
							{
								query += " = " + (field.UsingSQLToValue ? field.Value : "@" + field.Name);
								break;
							}

						case Comparison.IN:
						case Comparison.NOT_IN:
							{
								query += (field.Comparison == Comparison.NOT_IN ? " NOT" : "") + 
									" IN (" + (field.UsingSQLToValue ? field.Value : "@" + field.Name) + ")";
								break;
							}

						case Comparison.NOT:
							{
								query += " != " + (field.UsingSQLToValue ? field.Value : "@" + field.Name);
								break;
							}

						case Comparison.ISNULL:
						case Comparison.NOTNULL:
							{
								if (field.UsingSQLToValue)
									query += " " + field.Comparison;
								else
									query += " /* For ISNULL and NOTNULL set UsingSQLToValue = true */ ";
								break;
							}

						default:
							{
								query += " " + field.Comparison + " " + (field.UsingSQLToValue ? field.Value : "@" + field.Name);
								break;
							}
					}

					if (count < lenght)
					{
						if (field.ComparisonNext != Comparison.Empty)
							query += " " + field.ComparisonNext;
					}
				}
			}

			if (Order.Count > 0)
			{
				int count = 0;
				query += "\nORDER BY ";

				foreach (KeyValuePair<string, SelectOrder> field in Order)
				{
					query += (count > 0 ? ", " : "") + field.Key + " " + field.Value;
					count++;
				}
			}

			if (Limit > 0)
				query += "\nLIMIT " + Limit.ToString();

			if (Offset > 0)
				query += "\nOFFSET " + Offset.ToString();

			return query;
		}

	}

	/// <summary>
	/// Умови відбору
	/// </summary>
	public class Where
	{
		/// <summary>
		/// Умова відбору
		/// </summary>
		/// <param name="name">Назва поля</param>
		/// <param name="comparison">Тип порівняння</param>
		/// <param name="value">Значення поля</param>
		/// <param name="usingSQLToValue">Використання запиту SQL в якості значення поля</param>
		/// <param name="comparisonNext">Звязок між блоками відборів</param>
		public Where(string name, Comparison comparison, object value, bool usingSQLToValue = false, Comparison comparisonNext = Comparison.Empty)
		{
			ComparisonPreceding = Comparison.Empty;
			Name = name;
			Comparison = comparison;
			Value = value;
			UsingSQLToValue = usingSQLToValue;
			ComparisonNext = comparisonNext;
		}

		/// <summary>
		/// Умова відбору
		/// </summary>
		/// <param name="comparisonPreceding">Звязок між блоками відборів</param>
		/// <param name="name">Назва поля</param>
		/// <param name="comparison">Тип порівняння</param>
		/// <param name="value">Значення поля</param>
		/// <param name="usingSQLToValue">Використання запиту SQL в якості значення поля</param>
		public Where(Comparison comparisonPreceding, string name, Comparison comparison, object value, bool usingSQLToValue = false)
		{
			ComparisonPreceding = comparisonPreceding;
			Name = name;
			Comparison = comparison;
			Value = value;
			UsingSQLToValue = usingSQLToValue;
			ComparisonNext = Comparison.Empty;
		}

		/// <summary>
		/// Назва поля
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Значення поля
		/// </summary>
		public object Value { get; set; }

		/// <summary>
		/// Тип порівняння
		/// </summary>
		public Comparison Comparison { get; set; }

		/// <summary>
		/// Звязок між блоками відборів
		/// </summary>
		public Comparison ComparisonPreceding { get; set; }

		/// <summary>
		/// Звязок між блоками відборів
		/// </summary>
		public Comparison ComparisonNext { get; set; }

		/// <summary>
		/// Використання запиту SQL в якості значення поля
		/// </summary>
		public bool UsingSQLToValue { get; set; }
	}

	/// <summary>
	/// Тип порівняння
	/// </summary>
	public enum Comparison
	{
		/// <summary>
		/// И
		/// </summary>
		AND,

		/// <summary>
		/// ИЛИ
		/// </summary>
		OR,

		/// <summary>
		/// НЕ
		/// </summary>
		NOT,

		/// <summary>
		/// НЕ В Списку
		/// </summary>
		NOT_IN,

		/// <summary>
		/// В списку
		/// </summary>
		IN,

		/// <summary>
		/// =
		/// </summary>
		EQ,

		/// <summary>
		/// &lt;
		/// </summary>
		LT,

		/// <summary>
		/// >
		/// </summary>
		QT,

		/// <summary>
		/// Тільки для UsingSQLToValue = true
		/// </summary>
		ISNULL,

		/// <summary>
		/// Тільки для UsingSQLToValue = true
		/// </summary>
		NOTNULL,

		/// <summary>
		/// Пустий
		/// </summary>
		Empty
	}

	/// <summary>
	/// Сортування
	/// </summary>
	public enum SelectOrder
	{
		ASC,
		DESC
	}
}

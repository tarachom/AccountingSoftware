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
using System.Text;

namespace AccountingSoftware
{
	public class Query
	{
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

		public string Construct()
		{
			StringBuilder sb = new StringBuilder();

			if (CreateTempTable == true) 
			{
				TempTable = "tmp_" + Guid.NewGuid().ToString().Replace("-", "");
				sb.AppendLine("CREATE TEMP TABLE " + TempTable /*+ " ON COMMIT DROP"*/);
				sb.AppendLine("AS ");
			}

			sb.Append("SELECT uid ");

			if (Field.Count > 0)
			{
				foreach (string field in Field)
				{
					sb.Append(", " + field);
				}
			}

			sb.AppendLine("");
			sb.Append("FROM " + Table + " ");

			if (Where.Count > 0)
			{
				int count = 0;
				int lenght = Where.Count;

				sb.AppendLine("");
				sb.Append("WHERE ");

				foreach (Where field in Where)
				{
					count++;

					if (count > 1)
					{
						if (field.ComparisonPreceding != Comparison.Empty)
							sb.Append(field.ComparisonPreceding + " ");
					}

					sb.Append(field.Name);

					switch (field.Comparison)
					{
						case Comparison.EQ:
							{
								if (field.UsingSQLToValue)
								    sb.Append(" = " + field.Value + " ");
								else
									sb.Append(" = @" + field.Name + " ");
							}
							break;

						case Comparison.IN:
							{
								if (field.UsingSQLToValue)
									sb.Append(" IN (" + field.Value + ") ");
								else
									sb.Append(" IN (@" + field.Name + ") ");
							}
							break;

						case Comparison.NOT:
							{
								if (field.UsingSQLToValue)
									sb.Append(" != " + field.Value + " ");
								else
									sb.Append(" != @" + field.Name + " ");
							}
							break;

						case Comparison.ISNULL:
						case Comparison.NOTNULL:
							{
								if (field.UsingSQLToValue)
									sb.Append(" " + field.Comparison + " ");
							}
							break;

						default:
							{
								if (field.UsingSQLToValue)
									sb.Append(" " + field.Comparison + " " + field.Value + " ");
								else
									sb.Append(" " + field.Comparison + "  @" + field.Name + " ");
							}
							break;
					}

					if (count < lenght)
					{
						if (field.ComparisonNext != Comparison.Empty)
							sb.Append(field.ComparisonNext + " ");
					}
				}
			}

			if (Order.Count > 0)
			{
				int count = 0;

				sb.AppendLine();
				sb.Append("ORDER BY ");

				foreach (KeyValuePair<string, SelectOrder> field in Order)
				{
					if (count > 0)
						sb.Append(", ");

					sb.Append(field.Key + " " + field.Value + " ");

					count++;
				}
			}

			if (Limit > 0)
			{
				sb.AppendLine();
				sb.Append("LIMIT " + Limit.ToString() + " ");
			}

			if (Offset > 0)
			{
				sb.AppendLine();
				sb.Append("OFFSET " + Offset.ToString() + " ");
			}

			return sb.ToString();
		}
	}

	public class Where
	{
		public Where(string name, Comparison comparison, object value, bool usingSQLToValue = false, Comparison comparisonNext = Comparison.Empty)
		{
			ComparisonPreceding = Comparison.Empty;
			Name = name;
			Comparison = comparison;
			Value = value;
			UsingSQLToValue = usingSQLToValue;
			ComparisonNext = comparisonNext;
		}

		public Where(Comparison comparisonPreceding, string name, Comparison comparison, object value, bool usingSQLToValue = false)
		{
			ComparisonPreceding = comparisonPreceding;
			Name = name;
			Comparison = comparison;
			Value = value;
			UsingSQLToValue = usingSQLToValue;
			ComparisonNext = Comparison.Empty;
		}

		public string Name { get; set; }

		public object Value { get; set; }

		public Comparison Comparison { get; set; }

		public Comparison ComparisonPreceding { get; set; }

		public Comparison ComparisonNext { get; set; }

		public bool UsingSQLToValue { get; set; }
	}

	public enum Comparison
	{
		AND,
		OR,
		NOT,

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

		ISNULL,
		NOTNULL,

		/// <summary>
		/// Пустий
		/// </summary>
		Empty
	}

	public enum SelectOrder
	{
		ASC,
		DESC
	}
}

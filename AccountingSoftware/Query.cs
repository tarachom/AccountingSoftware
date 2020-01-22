﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware
{
	public class Query
	{
		public Query()
		{
			Field = new Dictionary<string, string>();
			Where = new List<Where>();
			Order = new Dictionary<string, SelectOrder>();
		}

		/// <summary>
		/// Назва таблиці
		/// </summary>
		public string Table { get; set; }

		/// <summary>
		/// Які поля вибирати.
		/// Назва поля, синонім
		/// </summary>
		public Dictionary<string, string> Field { get; set; }

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

		public string Construct()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("SELECT ");

			if (Field.Count > 0)
			{
				int count = 0;

				foreach (KeyValuePair<string, string> field in Field)
				{
					if (count > 0)
						sb.Append(", ");

					sb.Append(field.Key);

					count++;
				}
			}
			else
			{
				sb.Append("*");
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

					sb.Append(field.Name);

					switch (field.Comparison)
					{
						case Comparison.EQ:
							sb.Append(" = @" + field.Name + " ");
							break;

						case Comparison.IN:
							sb.Append(" IN (@" + field.Name + ") ");
							break;

						default:
							sb.Append(" " + field.Comparison + "  @" + field.Name + " ");
							break;
					}

					if (count < lenght)
					{
						if (field.ComparisonNext != Comparison.Empty)
						{
							sb.Append(field.ComparisonNext + " ");
						}
						else
							sb.Append(Comparison.AND + " ");
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

			return sb.ToString();
		}
	}

	//Tuple<string, Comparison, string, Comparison>
	public class Where
	{
		public Where(string name, Comparison comparison, object value, Comparison comparisonNext = Comparison.Empty)
		{
			Name = name;
			Comparison = comparison;
			Value = value;
			ComparisonNext = comparisonNext;
		}

		public string Name { get; set; }

		public object Value { get; set; }

		public Comparison Comparison { get; set; }

		public Comparison ComparisonNext { get; set; }
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

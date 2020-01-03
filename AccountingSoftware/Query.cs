using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestErlang.AccountingSoftware
{
	public class Query
	{
		public Query()
		{
			Field = new Dictionary<string, string>();
			Where = new List<Tuple<string, Comparison, string, Comparison>>();
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
		public List<Tuple<string, Comparison, string, Comparison>> Where { get; set; }

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

			if (Field != null && Field.Count > 0)
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

			if (Where != null && Where.Count > 0)
			{
				int count = 0;
				int lenght = Where.Count;

				sb.AppendLine("");
				sb.Append("WHERE ");

				foreach (Tuple<string, Comparison, string, Comparison> field in Where)
				{
					count++;

					sb.Append(field.Item1);

					switch (field.Item2)
					{
						case Comparison.EQ:
							sb.Append(" = ");
							break;

						default:
							sb.Append(" " + field.Item2 + " ");
							break;
					}

					sb.Append(field.Item3 + " ");

					if (count < lenght)
					{
						if (field.Item4 != Comparison.Empty)
						{
							sb.Append(field.Item4 + " ");
						}
						else
							sb.Append(Comparison.AND + " ");
					}
				}
			}

			if (Order != null && Order.Count > 0)
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

	public enum Comparison
	{
		AND,
		OR,
		NOT,
		EQ, //=
		Empty
	}

	public enum SelectOrder
	{
		ASC,
		DESC
	}
}

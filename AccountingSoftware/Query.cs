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

			//PrimaryField = "uid"; //!!!
		}

		/// <summary>
		/// Назва таблиці
		/// </summary>
		public string Table { get; set; } //!!

		/// <summary>
		/// Назва тимчасової таблиці
		/// </summary>
		public string TempTable { get; set; }

		/// <summary>
		/// Створити тимчасову таблицю на основі запиту
		/// </summary>
		public bool CreateTempTable { get; set; }

		/// <summary>
		/// Первинний ключ
		/// </summary>
		//public string PrimaryField { get; set; } //!!!

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

			if (Offset > 0)
			{
				sb.AppendLine();
				sb.Append("OFFSET " + Offset.ToString() + " ");
			}

			return sb.ToString();
		}
	}

	//Tuple<string, Comparison, string, Comparison>
	public class Where
	{
		public Where(string name, Comparison comparison, object value, bool usingSQLToValue = false, Comparison comparisonNext = Comparison.Empty)
		{
			Name = name;
			Comparison = comparison;
			Value = value;
			UsingSQLToValue = usingSQLToValue;
			ComparisonNext = comparisonNext;
		}

		public string Name { get; set; }

		public object Value { get; set; }

		public Comparison Comparison { get; set; }

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

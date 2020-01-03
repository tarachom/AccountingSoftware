using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestErlang.AccountingSoftware
{
	public class UnigueID
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="uid">Унікальний ідентифікатор</param>
		/// <param name="table">Таблиця задається у випадку составного типу {1111, table1} або {1111, table2}.
		/// Составний тип використовується у випадку коли в одне поле можна записати елементи з різних довідників наприклад, і
		/// потрібно вказувати який саме довідник використаний.</param>
		public UnigueID(string uid, string table = "")
		{
			UID = uid;
			Table = table;
		}

		public string UID { get; }
		public string Table { get; }
	}
}

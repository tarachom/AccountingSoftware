using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware
{
	public class UnigueID
	{
		/// <summary>
		/// Унікальний ідентифікатор
		/// </summary>
		/// <param name="uguid">Унікальний ідентифікатор</param>
		/// <param name="table">Таблиця задається у випадку составного типу {1111, table1} або {1111, table2}.
		/// Составний тип використовується у випадку коли в одне поле можна записати елементи з різних довідників наприклад, і
		/// потрібно вказувати який саме довідник використаний.</param>
		public UnigueID(Guid uGuid, string table = "") //
		{
			UGuid = uGuid;
			Table = table;
		}

		public Guid UGuid { get; private set; }

		public string Table { get; private set; }

		public override string ToString()
		{
			return UGuid.ToString();
		}
	}
}

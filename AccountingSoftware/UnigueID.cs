using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	public class UnigueID
	{
		/// <summary>
		/// Унікальний ідентифікатор
		/// </summary>
		/// <param name="uGuid">Унікальний ідентифікатор</param>
		/// <param name="table">Таблиця задається у випадку составного типу {1111, table1} або {1111, table2}.
		/// Составний тип використовується у випадку коли в одне поле можна записати елементи з різних довідників, і
		/// потрібно вказувати який саме довідник використаний.</param>
		public UnigueID(Guid uGuid, string table = "") //
		{
			UGuid = uGuid;
			Table = table;
		}

		/// <summary>
		/// Унікальний ідентифікатор у формі тексту. Використовується Guid.Parse(uGuid).
		/// </summary>
		/// <param name="uGuid">Унікальний ідентифікатор</param>
		/// <param name="table">Таблиця задається у випадку составного типу</param>
		public UnigueID(string uGuid, string table = "") //
		{
			UGuid = Guid.Parse(uGuid);
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

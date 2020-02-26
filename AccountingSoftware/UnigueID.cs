﻿/*
Copyright (C) 2019-2020 Tarakhomin Yuri Ivanovich
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
		public UnigueID(Guid uGuid, string table = "")
		{
			UGuid = uGuid;
			Table = table;
		}

		public UnigueID(object uGuid, string table = "")
		{
			if (uGuid != null && uGuid != DBNull.Value)
			{
				UGuid = (Guid)uGuid;
			}
			else
			{
				UGuid = Guid.Empty;
			}

			Table = table;
		}

		/// <summary>
		/// Унікальний ідентифікатор у формі тексту. Використовується Guid.Parse(uGuid).
		/// </summary>
		/// <param name="uGuid">Унікальний ідентифікатор</param>
		/// <param name="table">Таблиця задається у випадку составного типу</param>
		public UnigueID(string uGuid, string table = "")
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

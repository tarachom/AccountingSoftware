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

namespace AccountingSoftware
{
	/// <summary>
	/// Журнал Документів
	/// </summary>
	public abstract class JournalObject
	{
		public JournalObject(Kernel kernel)
		{
			Kernel = kernel;
		}

		/// <summary>
		/// Ядро
		/// </summary>
		private Kernel Kernel { get; set; }

		/// <summary>
		/// Таблиця
		/// </summary>
		protected string Table { get; set; }

		/// <summary>
		/// Назва як задано в конфігураторі
		/// </summary>
		protected string TypeDocument { get; set; }

		/// <summary>
		/// Унікальний ідентифікатор запису
		/// </summary>
		public UnigueID UnigueID { get; private set; }

		/// <summary>
		/// Документ проведений
		/// </summary>
		public bool Spend { get; private set; }

		/// <summary>
		/// Дата проведення документу
		/// </summary>
		public DateTime SpendDate { get; private set; }

		/// <summary>
		/// Зчитати дані
		/// </summary>
		/// <param name="uid">Унікальний ідентифікатор </param>
		/// <returns></returns>
		protected bool BaseRead(UnigueID uid)
		{
			if (uid == null || uid.UGuid == Guid.Empty)
				return false;

			bool spend = false;
			DateTime spend_date = DateTime.MinValue;

			if (Kernel.DataBase.SelectDocumentObject(uid, ref spend, ref spend_date, Table, null, null))
			{
				UnigueID = uid;
				Spend = spend;
				SpendDate = spend_date;

				return true;
			}
			else
				return false;
		}
	}
}

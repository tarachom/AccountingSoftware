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
	/// Документ Об'єкт
	/// </summary>
	public abstract class DocumentObject
	{
		public DocumentObject(Kernel kernel, string table, string[] fieldsArray)
		{
			Kernel = kernel;
			Table = table;
			FieldArray = fieldsArray;

			FieldValue = new Dictionary<string, object>();

			foreach (string field in FieldArray)
				FieldValue.Add(field, null);
		}

		/// <summary>
		/// Ядро
		/// </summary>
		private Kernel Kernel { get; set; }

		/// <summary>
		/// Таблиця
		/// </summary>
		private string Table { get; set; }

		/// <summary>
		/// Масив назв полів
		/// </summary>
		private string[] FieldArray { get; set; }

		/// <summary>
		/// Значення полів
		/// </summary>
		protected Dictionary<string, object> FieldValue { get; set; }

		/// <summary>
		/// Унікальний ідентифікатор запису
		/// </summary>
		public UnigueID UnigueID { get; private set; }

		/// <summary>
		/// Чи це новий?
		/// </summary>
		public bool IsNew { get; private set; }

		/// <summary>
		/// Новий обєкт
		/// </summary>
		public void New()
		{
			UnigueID = new UnigueID(Guid.NewGuid());
			IsNew = true;
		}

		/// <summary>
		/// Очистка вн. списку
		/// </summary>
		protected void BaseClear()
		{
			foreach (string field in FieldArray)
				FieldValue[field] = null;
		}

		/// <summary>
		/// Зчитати дані
		/// </summary>
		/// <param name="uid">Унікальний ідентифікатор </param>
		/// <returns></returns>
		protected bool BaseRead(UnigueID uid)
		{
			if (uid == null || uid.UGuid == Guid.Empty)
				return false;

			BaseClear();

			if (Kernel.DataBase.SelectDocumentObject(this, uid, Table, FieldArray, FieldValue))
			{
				UnigueID = uid;
				return true;
			}
			else
				return false;
		}

		/// <summary>
		/// Зберегти дані
		/// </summary>
		protected void BaseSave()
		{
			if (IsNew)
			{
				Kernel.DataBase.InsertDocumentObject(this, Table, FieldArray, FieldValue);
			}
			else
			{
				Kernel.DataBase.UpdateDocumentObject(this, Table, FieldArray, FieldValue);
			}

			BaseClear();
		}

		/// <summary>
		/// Видалити запис
		/// </summary>
		protected void BaseDelete(string[] tablePartsTables)
		{
			Kernel.DataBase.BeginTransaction();

			//Видалити сам документ
			Kernel.DataBase.DeleteDocumentObject(UnigueID, Table);

			//Видалення даних з табличних частин
			foreach (string tablePartsTable in tablePartsTables)
				Kernel.DataBase.DeleteDocumentTablePartRecords(UnigueID, tablePartsTable);

			Kernel.DataBase.CommitTransaction();

			BaseClear();
		}
	}
}

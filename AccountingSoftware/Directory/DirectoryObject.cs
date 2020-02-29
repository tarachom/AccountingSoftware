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

namespace AccountingSoftware
{
	/// <summary>
	/// Довідник Об'єкт
	/// </summary>
	public abstract class DirectoryObject
	{
		public DirectoryObject(Kernel kernel, string table, string[] fieldsArray)
		{
			Kernel = kernel;
			Table = table;
			FieldArray = fieldsArray;

			FieldValue = new Dictionary<string, object>();

			foreach (string field in FieldArray)
				FieldValue.Add(field, null);
		}

		private Kernel Kernel { get; set; }

		private string Table { get; set; }

		private string[] FieldArray { get; set; }

		protected Dictionary<string, object> FieldValue { get; set; }

		public UnigueID UnigueID { get; private set; }

		public bool IsNew { get; private set; }

		public void New()
		{
			UnigueID = new UnigueID(Guid.NewGuid());
			IsNew = true;
		}

		protected void BaseClear()
		{
			foreach (string field in FieldArray)
				FieldValue[field] = null;
		}

		protected bool BaseRead(UnigueID uid)
		{
			if (uid == null || uid.UGuid == Guid.Empty)
				return false;

			BaseClear();

			if (Kernel.DataBase.SelectDirectoryObject(this, uid, Table, FieldArray, FieldValue))
			{
				UnigueID = uid;
				return true;
			}
			else
				return false;
		}

		protected void BaseSave()
		{
			if (IsNew)
			{
				Kernel.DataBase.InsertDirectoryObject(this, Table, FieldArray, FieldValue);
			}
			else
			{
				Kernel.DataBase.UpdateDirectoryObject(this, Table, FieldArray, FieldValue);
			}

			BaseClear();
		}

		protected void BaseDelete()
		{
			Kernel.DataBase.DeleteDirectoryObject(UnigueID, Table);

			BaseClear();
		}
	}
}
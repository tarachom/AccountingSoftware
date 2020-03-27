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
	/// Довідник Таблична частина
	/// </summary>
	public abstract class DirectoryTablePart
	{
		public DirectoryTablePart(Kernel kernel, string table, string[] fieldsArray)
		{
			Kernel = kernel;
			Table = table;
			FieldArray = fieldsArray;

			FieldValueList = new List<Dictionary<string, object>>();
		}

		private Kernel Kernel { get; set; }

		private string Table { get; set; }

		private string[] FieldArray { get; set; }

		protected List<Dictionary<string, object>> FieldValueList { get; private set; }

		protected void BaseClear()
		{
			FieldValueList.Clear();
		}

		protected void BaseRead(UnigueID ownerUnigueID)
		{
			BaseClear();
			Kernel.DataBase.SelectDirectoryTablePartRecords(ownerUnigueID, Table, FieldArray, FieldValueList);
		}

		protected void BaseBeginTransaction()
		{
			Kernel.DataBase.BeginTransaction();
		}

		protected void BaseCommitTransaction()
		{
			Kernel.DataBase.CommitTransaction();
		}

		protected void BaseRollbackTransaction()
		{
			Kernel.DataBase.RollbackTransaction();
		}

		protected void BaseDelete(UnigueID ownerUnigueID)
		{
			Kernel.DataBase.DeleteDirectoryTablePartRecords(ownerUnigueID, Table);
		}

		protected void BaseSave(Guid UID, UnigueID ownerUnigueID, Dictionary<string, object> fieldValue)
		{
			Guid recordUnigueID = Guid.NewGuid(); //(UID == null ? Guid.NewGuid() : UID);	

			Kernel.DataBase.InsertDirectoryTablePartRecords(recordUnigueID, ownerUnigueID, Table, FieldArray, fieldValue);
		}
	}
}


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
	/// 
	/// </summary>
	public abstract class RegisterInformationRecordsSet
	{
		public RegisterInformationRecordsSet(Kernel kernel, string table, string[] fieldsArray)
		{
			Kernel = kernel;
			Table = table;
			FieldArray = fieldsArray;

			FieldValueList = new List<Dictionary<string, object>>();
			BaseFilter = new List<Where>();
		}

		private Kernel Kernel { get; set; }

		private string Table { get; set; }

		private string[] FieldArray { get; set; }

		protected List<Dictionary<string, object>> FieldValueList { get; private set; }

		protected List<Where> BaseFilter { get; }

		protected void BaseClear()
		{
			FieldValueList.Clear();
		}

		protected void BaseRead()
		{
			BaseClear();
			Kernel.DataBase.SelectRegisterInformationRecords(Table, FieldArray, BaseFilter, FieldValueList);
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

		protected void BaseDelete()
		{
			Kernel.DataBase.DeleteRegisterInformationRecords(Table, BaseFilter);
		}

		protected void BaseSave(Guid UID, Dictionary<string, object> fieldValue)
		{
			Guid recordUnigueID = (UID == null ? Guid.NewGuid() : UID);
			Kernel.DataBase.InsertRegisterInformationRecords(recordUnigueID, Table, FieldArray, fieldValue);
		}
	}
}

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
	/// 
	/// </summary>
	public abstract class RegisterAccumulationRecordsSet
	{
		public RegisterAccumulationRecordsSet(Kernel kernel, string table, string[] fieldsArray)
		{
			Kernel = kernel;
			Table = table;
			FieldArray = fieldsArray;

			QuerySelect = new Query(Table);
			QuerySelect.Field.AddRange(new string[] { "period", "income", "owner" });
			QuerySelect.Field.AddRange(fieldsArray);

			FieldValueList = new List<Dictionary<string, object>>();
			JoinValue = new Dictionary<string, Dictionary<string, string>>();
			BaseFilter = new List<Where>();
		}

		public Query QuerySelect { get; set; }

		private Kernel Kernel { get; set; }

		private string Table { get; set; }

		private string[] FieldArray { get; set; }

		protected List<Dictionary<string, object>> FieldValueList { get; private set; }

		public Dictionary<string, Dictionary<string, string>> JoinValue { get; private set; }

		protected List<Where> BaseFilter { get; }

		protected void BaseClear()
		{
			FieldValueList.Clear();
		}

		protected void BaseRead()
		{
			BaseClear();

			JoinValue.Clear();

			QuerySelect.Where.Clear();
			QuerySelect.Where.AddRange(BaseFilter);

			Kernel.DataBase.SelectRegisterAccumulationRecords(QuerySelect, FieldValueList);

			if (QuerySelect.FieldAndAlias.Count > 0)
			{
				foreach (Dictionary<string, object> fieldValue in FieldValueList)
				{
					Dictionary<string, string> joinFieldValue = new Dictionary<string, string>();
					JoinValue.Add(fieldValue["uid"].ToString(), joinFieldValue);

					foreach (KeyValuePair<string, string> fieldAndAlias in QuerySelect.FieldAndAlias)
						joinFieldValue.Add(fieldAndAlias.Value, fieldValue[fieldAndAlias.Value].ToString());
				}
			}
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

		protected void BaseDelete(Guid owner)
		{
			Kernel.DataBase.DeleteRegisterAccumulationRecords(Table, owner);
		}

		protected void BaseSave(Guid UID, DateTime period, bool income, Guid owner, Dictionary<string, object> fieldValue)
		{
			Guid recordUnigueID = (UID == Guid.Empty ? Guid.NewGuid() : UID);
			Kernel.DataBase.InsertRegisterAccumulationRecords(recordUnigueID, Table, period, income, owner, FieldArray, fieldValue);
		}
	}
}

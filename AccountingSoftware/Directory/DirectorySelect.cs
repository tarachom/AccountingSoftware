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
	/// Довідник Вибірка Вказівників
	/// </summary>
	public abstract class DirectorySelect
	{
		public DirectorySelect(Kernel kernel, string table, string[] fieldsNameInTableArray, string[] fieldsNameArray)
		{
			Kernel = kernel;
			Table = table;

			QuerySelect = new Query(table);
			BaseSelectList = new List<DirectoryPointer>();

			Alias = new Dictionary<string, string>();
			for (int i = 0; i < fieldsNameInTableArray.Length; i++)
			{
				Alias.Add(fieldsNameArray[i], fieldsNameInTableArray[i]);
			}
		}

		public Query QuerySelect { get; set; }

		public void MoveToFirst()
		{
			Position = 0;
			MoveToPosition();
		}

		public int Count()
		{
			return BaseSelectList.Count;
		}

		private Kernel Kernel { get; set; }

		private string Table { get; set; }

		protected int Position { get; private set; }

		protected Dictionary<string, string> Alias { get; }

		protected DirectoryPointer DirectoryPointerPosition { get; private set; }

		protected List<DirectoryPointer> BaseSelectList { get; private set; }

		protected bool MoveToPosition()
		{
			if (Position < BaseSelectList.Count)
			{
				DirectoryPointerPosition = BaseSelectList[Position];
				Position++;
				return true;
			}
			else
			{
				DirectoryPointerPosition = null;
				return false;
			}
		}

		protected bool BaseSelect()
		{
			Position = 0;
			DirectoryPointerPosition = null;
			BaseSelectList.Clear();

			Kernel.DataBase.SelectDirectoryPointers(QuerySelect, BaseSelectList);

			return Count() > 0;
		}

		protected bool BaseSelectSingle()
		{
			int oldLimitValue = QuerySelect.Limit;
			QuerySelect.Limit = 1;

			BaseSelect();

			QuerySelect.Limit = oldLimitValue;

			return Count() > 0;
		}

		protected DirectoryPointer BaseFindByField(string fieldName, object fieldValue)
		{
			if (!Alias.ContainsKey(fieldName))
				throw new Exception("Поле " + fieldName + " не знайдено!");

			DirectoryPointer directoryPointer = new DirectoryPointer(Kernel, Table);

			Query querySelect = new Query(Table);
			querySelect.Where.Add(new Where(Alias[fieldName], Comparison.EQ, fieldValue));

			bool isFind = Kernel.DataBase.FindDirectoryPointer(querySelect, ref directoryPointer);

			return directoryPointer;
		}

		protected List<DirectoryPointer> BaseFindListByField(string fieldName, object fieldValue, int limit = 0, int offset = 0)
		{
			if (!Alias.ContainsKey(fieldName))
				throw new Exception("Поле " + fieldName + " не знайдено!");

			List<DirectoryPointer> directoryPointerList = new List<DirectoryPointer>();

			Query querySelect = new Query(Table);
			querySelect.Limit = limit;
			querySelect.Offset = offset;
			querySelect.Where.Add(new Where(Alias[fieldName], Comparison.EQ, fieldValue));

			Kernel.DataBase.SelectDirectoryPointers(querySelect, directoryPointerList);

			return directoryPointerList;
		}

		public void Dispose()
		{
			QuerySelect = null;
			Kernel = null;
			DirectoryPointerPosition = null;
			BaseSelectList = null;
			Position = 0;
		}
	}
}
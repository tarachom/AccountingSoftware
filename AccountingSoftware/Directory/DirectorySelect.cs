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
		public DirectorySelect(Kernel kernel, string table)
		{
			QuerySelect = new Query(table);
			Kernel = kernel;

			BaseSelectList = new List<DirectoryPointer>();
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

		protected Kernel Kernel { get; private set; }

		protected int Position { get; private set; }

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

			Kernel.DataBase.SelectDirectoryPointers(this, BaseSelectList);

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
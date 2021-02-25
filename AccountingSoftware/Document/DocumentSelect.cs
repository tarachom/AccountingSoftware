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
	public abstract class DocumentSelect
	{
		public DocumentSelect(Kernel kernel, string table)
		{
			QuerySelect = new Query(table);
			Kernel = kernel;

			BaseSelectList = new List<DocumentPointer>();
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

		protected DocumentPointer DocumentPointerPosition { get; private set; }

		protected List<DocumentPointer> BaseSelectList { get; private set; }

		protected bool MoveToPosition()
		{
			if (Position < BaseSelectList.Count)
			{
				DocumentPointerPosition = BaseSelectList[Position];
				Position++;
				return true;
			}
			else
			{
				DocumentPointerPosition = null;
				return false;
			}
		}

		protected bool BaseSelect()
		{
			Position = 0;
			DocumentPointerPosition = null;
			BaseSelectList.Clear();

			Kernel.DataBase.SelectDocumentPointer(this, BaseSelectList);

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
			DocumentPointerPosition = null;
			BaseSelectList = null;
			Position = 0;
		}
	}
}
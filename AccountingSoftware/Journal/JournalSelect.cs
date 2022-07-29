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
	/// Журнал Вибірка Документів
	/// </summary>
	public abstract class JournalSelect
	{
		public JournalSelect(Kernel kernel, string[] table, string[] typeDocument)
		{
			Kernel = kernel;
			TableArray = table;
			TypeDocumentArray = typeDocument;

			WhereSelect = new List<Where>();
			BaseSelectList = new List<DocumentPointer>();
		}

		/// <summary>
		/// Масив таблиць
		/// </summary>
		private string[] TableArray { get; set; }

		/// <summary>
		/// Масив типів документів
		/// </summary>
		private string[] TypeDocumentArray { get; set; }


		public List<Where> WhereSelect { get; set; }

		/// <summary>
		/// Переміститися в початок вибірки
		/// </summary>
		public void MoveToFirst()
		{
			Position = 0;
			MoveToPosition();
		}

		/// <summary>
		/// Кількість елементів вибірки
		/// </summary>
		/// <returns></returns>
		public int Count()
		{
			return BaseSelectList.Count;
		}

		/// <summary>
		/// Ядро
		/// </summary>
		protected Kernel Kernel { get; private set; }

		/// <summary>
		/// Поточна позиція
		/// </summary>
		protected int Position { get; private set; }

		/// <summary>
		/// Поточний вказівник
		/// </summary>
		protected DocumentPointer DocumentPointerPosition { get; private set; }

		/// <summary>
		/// Список вибраних вказівників
		/// </summary>
		protected List<DocumentPointer> BaseSelectList { get; private set; }

		/// <summary>
		/// Переміститися на наступну позицію
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// Зчитати
		/// </summary>
		/// <returns></returns>
		protected bool BaseSelect()
		{
			Position = 0;
			DocumentPointerPosition = null;
			BaseSelectList.Clear();

			Kernel.DataBase.SelectDocumentPointer(QuerySelect, BaseSelectList);

			return Count() > 0;
		}

		public void Dispose()
		{
			WhereSelect = null;
			Kernel = null;
			DocumentPointerPosition = null;
			BaseSelectList = null;
			Position = 0;
		}
	}
}
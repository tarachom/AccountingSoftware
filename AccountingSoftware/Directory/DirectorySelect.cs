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

			Kernel.DataBase.SelectDirectoryPointer(this, BaseSelectList);

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
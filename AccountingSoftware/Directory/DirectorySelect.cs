using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccountingSoftware
{
	//Довідник Вибірка
	//
	public abstract class DirectorySelect
	{
		public DirectorySelect(Kernel kernel, string table)
		{
			QuerySelect = new Query(table);
			Kernel = kernel;
		}

		public Query QuerySelect { get; set; }

		public void MoveToFirst()
		{
			Position = 0;

			MoveToPosition();
		}

		public int Count()
		{
			return (BaseSelectList != null) ? BaseSelectList.Count : 0;
		}

		protected Kernel Kernel { get; private set; }

		protected int Position { get; private set; }

		protected DirectoryPointer DirectoryPointerPosition { get; private set; }

		protected bool MoveToPosition()
		{
			if (BaseSelectList == null)
				return false;

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

		protected List<DirectoryPointer> BaseSelectList { get; private set; }

		protected void BaseSelect()
		{
			Position = 0;
			DirectoryPointerPosition = null;
			BaseSelectList = new List<DirectoryPointer>();

			Kernel.DataBase.SelectDirectoryPointer(this, BaseSelectList);
		}

		protected void BaseSelectSingle()
		{
			int oldLimitValue = QuerySelect.Limit;
			QuerySelect.Limit = 1;

			BaseSelect();

			QuerySelect.Limit = oldLimitValue;
		}
	}
}
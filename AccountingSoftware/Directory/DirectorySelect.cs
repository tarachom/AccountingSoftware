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

		protected Kernel Kernel { get; private set; }

		protected int Position { get; private set; }

		protected DirectoryPointer DirectoryPointerPosition { get; private set; }

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

		protected List<DirectoryPointer> BaseSelectList { get; private set; }

		protected void BaseSelect()
		{
			BaseSelectList = new List<DirectoryPointer>();
			Kernel.DataBase.SelectDirectoryPointer(this, BaseSelectList);
		}
	}
}
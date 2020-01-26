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

		protected Kernel Kernel { get; set; }

		protected List<DirectoryPointer> BaseSelectList { get; private set; }

		protected void BaseSelect()
		{
			BaseSelectList = new List<DirectoryPointer>();

			Kernel.DataBase.SelectDirectoryPointer(this, BaseSelectList);
		}
	}
}
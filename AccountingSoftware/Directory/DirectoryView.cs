using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	public abstract class DirectoryView
	{
		public DirectoryView(Kernel kernel, string table, string[] fieldsArray)
		{
			Table = table;
			Kernel = kernel;
			FieldArray = fieldsArray;

			QuerySelect = new Query(table);

			foreach (string field in fieldsArray)
				QuerySelect.Field.Add(field);
		}

		private Kernel Kernel { get; set; }

		private string Table { get; set; }

		private string[] FieldArray { get; set; }

		public Query QuerySelect { get; set; }

		public string Read()
		{
			return Kernel.DataBase.SelectDirectoryView(this);
		}
	}
}

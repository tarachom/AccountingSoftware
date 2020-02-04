using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	public abstract class DirectoryView
	{
		public DirectoryView(Kernel kernel, string table, string[] fieldsArray, string name)
		{
			Table = table;
			Kernel = kernel;
			FieldArray = fieldsArray;
			Name = name;

			QuerySelect = new Query(table);

			foreach (string field in fieldsArray)
				QuerySelect.Field.Add(field);
		}

		private Kernel Kernel { get; set; }

		private string Table { get; set; }

		private string[] FieldArray { get; set; }

		public string Name { get; } 

		public Query QuerySelect { get; set; }

		public string Read()
		{
			return Kernel.DataBase.SelectDirectoryView(this);
		}
	}
}

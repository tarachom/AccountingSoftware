using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	/// <summary>
	/// Довідник Візуалізація для вибірки даних
	/// </summary>
	public abstract class DirectoryView
	{
		public DirectoryView(Kernel kernel, string table, string[] fieldsNameInTableArray, string[] fieldsNameArray, string name)
		{
			Table = table;
			Kernel = kernel;
			//FieldsNameInTableArray = fieldsNameInTableArray;
			//FieldsNameArray = fieldsNameArray;
			Name = name;

			Alias = new Dictionary<string, string>();
			AliasRevers = new Dictionary<string, string>();

			QuerySelect = new Query(table);

			for (int i = 0; i < fieldsNameInTableArray.Length; i++)
			{
				Alias.Add(fieldsNameArray[i], fieldsNameInTableArray[i]);
				AliasRevers.Add(fieldsNameInTableArray[i], fieldsNameArray[i]);
				
				QuerySelect.Field.Add(fieldsNameInTableArray[i]);
			}				
		}

		private Kernel Kernel { get; set; }

		private string Table { get; set; }

		//private string[] FieldsNameInTableArray { get; set; }
		//private string[] FieldsNameArray { get; set; }

		public Dictionary<string, string> Alias { get; }
		public Dictionary<string, string> AliasRevers { get; }

		public string Name { get; } 

		public Query QuerySelect { get; set; }

		public string Read()
		{
			return Kernel.DataBase.SelectDirectoryView(this);
		}
	}
}

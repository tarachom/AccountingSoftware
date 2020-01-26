using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware
{
	//Довідник Обєкт
	//
	public abstract class DirectoryObject
	{
		public DirectoryObject(Kernel kernel, string table, string[] fields)
		{
			Table = table;
			Kernel = kernel;
			FieldList = fields;

			Fields = new Dictionary<string, object>();

			foreach (string field in fields)
				Fields.Add(field, null);
		}

		protected Kernel Kernel { get; set; }

		public string Table { get; set; }

		public UnigueID UID { get; private set; }

		protected void BaseInit(UnigueID uid)
		{
			UID = uid;

			Kernel.DataBase.SelectDirectoryObject(this, Fields);
		}

		protected void BaseSave()
		{
			Kernel.DataBase.SaveDirectoryObject(this, Fields);
		}

		public string[] FieldList { get; set; }

		protected Dictionary<string, object> Fields { get; set; }
	}
}

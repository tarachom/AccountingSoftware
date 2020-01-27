using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

		protected Kernel Kernel { get; private set; }

		public string Table { get; private set; }

		public UnigueID UnigueID { get; private set; }

		protected bool IsNew { get; private set; }

		public void New()
		{
			UnigueID = new UnigueID(Guid.NewGuid());
			IsNew = true;
		}

		protected void BaseInit(UnigueID uid)
		{
			UnigueID = uid;
			Kernel.DataBase.SelectDirectoryObject(this, Fields);
		}

		protected void BaseSave()
		{
			if (IsNew)
			{
				Kernel.DataBase.InsertDirectoryObject(this, Fields);
			}
			else
			{
				Kernel.DataBase.SaveDirectoryObject(this, Fields);
			}
		}

		public string[] FieldList { get; set; }

		protected Dictionary<string, object> Fields { get; set; }
	}
}
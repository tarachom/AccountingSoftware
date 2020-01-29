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
		public DirectoryObject(Kernel kernel, string table, string[] fieldsArray)
		{
			Kernel = kernel;
			Table = table;
			FieldArray = fieldsArray;

			FieldValue = new Dictionary<string, object>();

			foreach (string field in FieldArray)
				FieldValue.Add(field, null);
		}

		private Kernel Kernel { get; set; }

		private string Table { get; set; }

		private string[] FieldArray { get; set; }

		protected Dictionary<string, object> FieldValue { get; set; }

		public UnigueID UnigueID { get; private set; }

		public bool IsNew { get; private set; }

		public void New()
		{
			UnigueID = new UnigueID(Guid.NewGuid());
			IsNew = true;
		}

		protected void BaseRead(UnigueID uid)
		{
			UnigueID = uid;
			Kernel.DataBase.SelectDirectoryObject(this, Table, FieldArray, FieldValue);
		}

		protected void BaseSave()
		{
			if (IsNew)
			{
				Kernel.DataBase.InsertDirectoryObject(this, Table, FieldArray, FieldValue);
			}
			else
			{
				Kernel.DataBase.SaveDirectoryObject(this, Table, FieldArray, FieldValue);
			}
		}

		protected void BaseDelete()
		{
			Kernel.DataBase.DeleteDirectoryObject(this, Table);
		}
	}
}
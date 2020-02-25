using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	/// <summary>
	/// Документ Об'єкт
	/// </summary>
	public abstract class DocumentObject
	{
		public DocumentObject(Kernel kernel, string table, string[] fieldsArray)
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

		protected void BaseClear()
		{
			foreach (string field in FieldArray)
				FieldValue[field] = null;
		}

		protected bool BaseRead(UnigueID uid)
		{
			if (uid == null || uid.UGuid == Guid.Empty)
				return false;

			BaseClear();

			if (Kernel.DataBase.SelectDocumentObject(this, uid, Table, FieldArray, FieldValue))
			{
				UnigueID = uid;
				return true;
			}
			else
				return false;
		}

		protected void BaseSave()
		{
			if (IsNew)
			{
				Kernel.DataBase.InsertDocumentObject(this, Table, FieldArray, FieldValue);
			}
			else
			{
				Kernel.DataBase.UpdateDocumentObject(this, Table, FieldArray, FieldValue);
			}

			BaseClear();
		}

		protected void BaseDelete()
		{
			Kernel.DataBase.DeleteDocumentObject(UnigueID, Table);

			BaseClear();
		}
	}
}

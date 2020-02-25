using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	/// <summary>
	/// Документ Таблична частина
	/// </summary>
	public abstract class DocumentTablePart
	{
		public DocumentTablePart(Kernel kernel, string table, string[] fieldsArray)
		{
			Kernel = kernel;
			Table = table;
			FieldArray = fieldsArray;

			FieldValueList = new List<Dictionary<string, object>>();
		}

		private Kernel Kernel { get; set; }

		private string Table { get; set; }

		private string[] FieldArray { get; set; }

		protected List<Dictionary<string, object>> FieldValueList { get; private set; }

		protected void BaseClear()
		{
			FieldValueList.Clear();
		}

		protected void BaseRead(UnigueID ownerUnigueID)
		{
			BaseClear();

			Kernel.DataBase.SelectDocumentTablePartRecords(ownerUnigueID, Table, FieldArray, FieldValueList);
		}

		protected void BaseBeginTransaction()
		{
			Kernel.DataBase.BeginTransaction();
		}

		protected void BaseCommitTransaction()
		{
			Kernel.DataBase.CommitTransaction();
		}

		protected void BaseRollbackTransaction()
		{
			Kernel.DataBase.RollbackTransaction();
		}

		protected void BaseDelete(UnigueID ownerUnigueID)
		{
			Kernel.DataBase.DeleteDocumentTablePartRecords(ownerUnigueID, Table);
		}

		protected void BaseSave(UnigueID ownerUnigueID, Dictionary<string, object> fieldValue)
		{
			Kernel.DataBase.InsertDocumentTablePartRecords(ownerUnigueID, Table, FieldArray, fieldValue);
		}
	}
}

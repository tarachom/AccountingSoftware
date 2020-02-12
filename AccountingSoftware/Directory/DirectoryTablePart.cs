using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	/// <summary>
	/// Довідник Таблична частина
	/// </summary>
	public abstract class DirectoryTablePart
	{
		public DirectoryTablePart(Kernel kernel, string table, string[] fieldsArray)
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

			Kernel.DataBase.SelectDirectoryTablePartRecords(ownerUnigueID, Table, FieldArray, FieldValueList);
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
			Kernel.DataBase.DeleteDirectoryTablePartRecords(ownerUnigueID, Table);
		}

		protected void BaseSave(UnigueID ownerUnigueID, Dictionary<string, object> fieldValue)
		{
			Kernel.DataBase.InsertDirectoryTablePartRecords(ownerUnigueID, Table, FieldArray, fieldValue);
		}
	}
}

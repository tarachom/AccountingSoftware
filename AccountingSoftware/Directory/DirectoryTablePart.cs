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

		protected void BaseRead(UnigueID ownerUnigueID)
		{
			Kernel.DataBase.SelectDirectoryTablePartRecords(ownerUnigueID, Table, FieldArray, FieldValueList);
		}

		protected void BaseBeginTransaction()
		{
			Console.WriteLine("Begin");
			Kernel.DataBase.BeginTransaction();
		}

		protected void BaseCommitTransaction()
		{
			Console.WriteLine("Commit");
			Kernel.DataBase.CommitTransaction();
		}

		protected void BaseRollbackTransaction()
		{
			Console.WriteLine("Rollback");
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware
{
	//Довідник
	//
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

		protected void BaseSave(UnigueID ownerUnigueID, Dictionary<string, object> fieldValue)
		{
			Console.WriteLine("BaseSave " + ownerUnigueID.UGuid);

			Kernel.DataBase.InsertDirectoryTablePartRecords(ownerUnigueID, Table, FieldArray, fieldValue);
		}
	}
}

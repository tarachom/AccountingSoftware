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

			//FieldValue = new Dictionary<string, object>();

			
		}

		private Kernel Kernel { get; set; }

		private string Table { get; set; }

		protected string[] FieldArray { get; private set; }

		protected List<Dictionary<string, object>> FieldValueList { get; private set; }

		protected void BaseRead(UnigueID OwnerUnigueID)
		{
			//Kernel.DataBase.SelectDirectoryPointer(this, BaseSelectList);
		}
	}
}

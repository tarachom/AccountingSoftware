using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	public class DocumentPointer
	{
		public DocumentPointer()
		{
			UnigueID = new UnigueID(Guid.Empty);
		}

		public DocumentPointer(Kernel kernel, string table) : this()
		{
			Table = table;
			Kernel = kernel;
		}

		public void Init(UnigueID uid, Dictionary<string, object> fields = null)
		{
			UnigueID = uid;
			Fields = fields;
		}

		private Kernel Kernel { get; set; }

		private string Table { get; set; }

		public UnigueID UnigueID { get; private set; }

		public Dictionary<string, object> Fields { get; private set; }

		public void Delete()
		{
			Kernel.DataBase.DeleteDocumentObject(UnigueID, Table);
		}
	}
}

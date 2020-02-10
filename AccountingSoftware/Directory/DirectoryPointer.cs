using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	/// <summary>
	/// Довідник Вказівник - Вказівник на елемент довідника 
	/// </summary>
	public class DirectoryPointer
	{
		public DirectoryPointer()
		{
			UnigueID = new UnigueID(Guid.Empty);
		}

		public DirectoryPointer(Kernel kernel, string table)
		{
			Table = table;
			Kernel = kernel;

			UnigueID = new UnigueID(Guid.Empty);
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
			Kernel.DataBase.DeleteDirectoryObject(UnigueID, Table);
		}
	}
}

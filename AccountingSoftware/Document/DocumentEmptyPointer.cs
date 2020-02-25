using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	/// <summary>
	/// Пустий вказівник
	/// </summary>
	public class DocumentEmptyPointer
	{
		public DocumentEmptyPointer()
		{
			UnigueID = new UnigueID(Guid.Empty);
		}

		public UnigueID UnigueID { get; private set; }

		public override string ToString()
		{
			return UnigueID.UGuid.ToString();
		}
	}
}
